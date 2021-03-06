﻿using Subsonic.Client.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Subsonic.Client.Windows
{
    public class StreamProxy : IDisposable
    {
        private static readonly Lazy<StreamProxy> StreamProxyInstance = new Lazy<StreamProxy>(() => new StreamProxy());
        private bool _isRunning;
        private int _port;
        private TcpListener _socket;
        private StreamToMediaPlayerTask _task;
        private Thread _thread;
        private TrackModel _trackItem;

        private StreamProxy()
        { }

        public static StreamProxy Instance => StreamProxyInstance.Value;

        public void Dispose()
        {
            if (_isRunning)
                Stop();

            _task?.Dispose();
        }

        public int GetPort()
        {
            return _port;
        }

        public void SetTrackItem(TrackModel trackItem)
        {
            _trackItem = trackItem;
        }

        public void Start()
        {
            // Create listening socket
            try
            {
                _socket = new TcpListener(IPAddress.Loopback, 0);
                _socket.Start();
                _port = ((IPEndPoint)_socket.LocalEndpoint).Port;

                _thread = new Thread(Run);
                _thread.Start();
            }
            catch
            {
            }
        }

        public void Stop()
        {
            _isRunning = false;

            if (_thread == null) return;

            _socket.Stop();
            _thread.Abort();
        }

        private bool CheckForWork()
        {
            try
            {
                var client = _socket.AcceptTcpClient();

                if (!client.Connected)
                    return !_isRunning;

                _task.SetClient(client);

                if (_task.ProcessRequest())
                    _task.Run();
                else
                    _task.Dispose();
            }
            catch
            {
            }

            return !_isRunning;
        }

        private void Run()
        {
            _isRunning = true;

            _task = new StreamToMediaPlayerTask();

            SpinWait.SpinUntil(CheckForWork);
        }

        private class StreamToMediaPlayerTask : IDisposable
        {
            private const string Headers = "HTTP/1.1 200 OK\r\nContent-Type: application/octet-stream\r\nConnection: close\r\n\r\n";
            private int _cbSkip;
            private TcpClient _client;
            private NetworkStream _inputStream;
            private string _localPath;
            private StreamReader _streamReader;

            public void Dispose()
            {
                if (_streamReader != null)
                {
                    _streamReader.Close();
                    _streamReader.Dispose();
                    _streamReader = null;
                }

                if (_inputStream != null)
                {
                    _inputStream.Close();
                    _inputStream.Dispose();
                    _inputStream = null;
                }
            }

            internal bool ProcessRequest()
            {
                var request = ReadRequest();

                if (string.IsNullOrWhiteSpace(request))
                    return false;

                _localPath = Uri.UnescapeDataString(request);

                const int timeToWaitForFile = 5000;

                if (_localPath == null)
                    return false;

                var file = new FileInfo(_localPath);

                if (file.Exists)
                    return true;

                var waitForFile = true;
                var sw = new Stopwatch();
                sw.Start();

                while (waitForFile)
                {
                    file.Refresh();

                    if (file.Exists)
                        return true;

                    Thread.Sleep(2);

                    waitForFile &= sw.ElapsedMilliseconds <= timeToWaitForFile;
                }

                sw.Stop();

                return false;
            }

            internal void Run()
            {
                var headerBytes = Encoding.ASCII.GetBytes(Headers);
                var buff = new byte[4096];

                try
                {
                    using (var output = _client.GetStream())
                    {
                        output.Write(headerBytes, 0, headerBytes.Length);

                        // See if there's more to send
                        var file = new FileInfo(_localPath);

                        // Loop as long as there's stuff to send
                        while (Instance._isRunning && _client.Connected)
                        {
                            file.Refresh();

                            var cbSentThisBatch = 0;

                            if (file.Exists)
                            {
                                using (var input = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                                {
                                    input.Seek(_cbSkip, SeekOrigin.Current);
                                    var cbToSendThisBatch = input.Length;

                                    while (cbToSendThisBatch > 0)
                                    {
                                        var cbToRead = (int)Math.Min(cbToSendThisBatch, buff.Length);
                                        var cbRead = input.Read(buff, 0, cbToRead);

                                        if (cbRead == 0)
                                            break;

                                        cbToSendThisBatch -= cbRead;

                                        if (output.CanWrite)
                                            output.Write(buff, 0, cbRead);

                                        _cbSkip += cbRead;
                                        cbSentThisBatch += cbRead;
                                    }
                                }
                            }

                            // Done regardless of whether or not it thinks it is
                            if (Instance._trackItem.Cached)
                            {
                                // Make sure we have the latest file information
                                file.Refresh();

                                if (_cbSkip >= file.Length)
                                    break;
                            }

                            // If we did nothing this batch, block for a second
                            if (cbSentThisBatch == 0)
                                Thread.Sleep(100);
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    Dispose();
                    _client.Close();
                }
            }

            internal void SetClient(TcpClient client)
            {
                _client?.Close();

                _client = client;
                _client.NoDelay = true;
                _client.ReceiveBufferSize = 4096;
                _cbSkip = 0;
                _localPath = null;
            }

            private string ReadRequest()
            {
                String firstLine;

                try
                {
                    _inputStream = _client.GetStream();
                    _streamReader = new StreamReader(_inputStream);
                    firstLine = _streamReader.ReadLine();
                }
                catch (Exception)
                {
                    return null;
                }

                if (string.IsNullOrWhiteSpace(firstLine))
                    return null;

                var st = firstLine.Split(' ');
                return st.ElementAt(1).Substring(1);
            }
        }
    }
}