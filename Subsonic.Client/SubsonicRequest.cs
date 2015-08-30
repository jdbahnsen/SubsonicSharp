﻿using Subsonic.Client.Constants;
using Subsonic.Client.Exceptions;
using Subsonic.Client.Extensions;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Subsonic.Client
{
    public class SubsonicRequest<T> : ISubsonicRequest<T>
    {
        protected ISubsonicServer SubsonicServer { get; private set; }
        IImageFormatFactory<T> ImageFormatFactory { get; set; }

        protected SubsonicRequest(ISubsonicServer subsonicServer, IImageFormatFactory<T> imageFormatFactory)
        {
            SubsonicServer = subsonicServer;
            ImageFormatFactory = imageFormatFactory;
        }

        public virtual async Task<Response> RequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            Response result;
            Uri requestUri = SubsonicServer.BuildRequestUri(method, methodApiVersion, parameters);
            HttpClientHandler clientHandler = GetClientHandler();
            HttpClient client = GetClient(clientHandler);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (HttpResponseMessage response = cancelToken.HasValue ? await client.PostAsync(requestUri, null, cancelToken.Value) : await client.PostAsync(requestUri, null))
                {
                    string stringResponse = await response.Content.ReadAsStringAsync();

                    if (stringResponse == null) throw new SubsonicErrorException("HTTP response contains no content");

                    if (response.Content.Headers.ContentType.MediaType.Contains(HttpContentTypes.TextXml))
                        result = await DeserializeResponseAsync(stringResponse);
                    else
                        throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "HTTP response does not contain XML, content type is: {0}", response.Content.Headers.ContentType.MediaType));
                }
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            return result;
        }

        public virtual Task<long> RequestAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            throw new SubsonicApiException("Unsupported method");
        }

        public virtual async Task RequestWithoutResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            Uri requestUri = SubsonicServer.BuildRequestUri(method, methodApiVersion, parameters);
            HttpClientHandler clientHandler = GetClientHandler();
            HttpClient client = GetClient(clientHandler);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (HttpRequestMessage message = new HttpRequestMessage(System.Net.Http.HttpMethod.Head, requestUri))
                using (HttpResponseMessage ignored = cancelToken.HasValue ? await client.SendAsync(message, cancelToken.Value) : await client.SendAsync(message)) { }
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();
        }

        public virtual async Task<string> StringRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            Uri requestUri = SubsonicServer.BuildRequestUri(method, methodApiVersion, parameters);
            HttpClientHandler clientHandler = GetClientHandler();
            HttpClient client = GetClient(clientHandler);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (HttpResponseMessage response = cancelToken.HasValue ? await client.PostAsync(requestUri, null, cancelToken.Value) : await client.PostAsync(requestUri, null))
                {
                    string stringResponse = await response.Content.ReadAsStringAsync();

                    if (stringResponse == null) throw new SubsonicErrorException("HTTP response contains no content");

                    if (!response.Content.Headers.ContentType.MediaType.Contains(HttpContentTypes.TextXml))
                        return stringResponse;

                    Response result = await DeserializeResponseAsync(stringResponse);

                    if (result.ItemElementName == ItemChoiceType.Error)
                        throw new SubsonicErrorException("Error occurred during request.", result.Item as Error);

                    throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                }
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }
        }

        public virtual async Task<IImageFormat<T>> ImageRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            Uri requestUri = SubsonicServer.BuildRequestUri(method, methodApiVersion, parameters);
            HttpClientHandler clientHandler = GetClientHandler();
            HttpClient client = GetClient(clientHandler);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            MemoryStream content = new MemoryStream();

            try
            {
                using (HttpResponseMessage response = cancelToken.HasValue ? await client.GetAsync(requestUri, cancelToken.Value) : await client.GetAsync(requestUri))
                {
                    if (response.Content.Headers.ContentType != null && response.Content.Headers.ContentType.MediaType.Contains(HttpContentTypes.TextXml))
                    {
                        string stringResponse = await response.Content.ReadAsStringAsync();

                        if (stringResponse == null) throw new SubsonicErrorException("HTTP response contains no content");

                        Response result = await DeserializeResponseAsync(stringResponse);

                        if (result.ItemElementName == ItemChoiceType.Error)
                            throw new SubsonicErrorException("Error occurred during request.", result.Item as Error);

                        throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                    }

                    await response.Content.CopyToAsync(content);
                }
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            content.Position = 0;

            IImageFormat<T> image = ImageFormatFactory.Create();

            image.SetImageFromStream(content);

            return image;
        }

        public virtual async Task<long> ContentLengthRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            Uri requestUri = SubsonicServer.BuildRequestUri(method, methodApiVersion, parameters);
            HttpClientHandler clientHandler = GetClientHandler();
            HttpClient client = GetClient(clientHandler);

            long length = -1;

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (HttpRequestMessage message = new HttpRequestMessage(System.Net.Http.HttpMethod.Head, requestUri))
                using (HttpResponseMessage response = cancelToken.HasValue ? await client.SendAsync(message, cancelToken.Value) : await client.SendAsync(message))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response status code: {0}", Enum.GetName(typeof(ItemChoiceType), response.StatusCode)));

                    if (response.Content.Headers.ContentLength != null)
                        length = response.Content.Headers.ContentLength.GetValueOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            return length;
        }

        public virtual async Task<bool> SettingChangeRequestAsync(SettingMethods method, CancellationToken? cancelToken = null)
        {
            Uri requestUri = SubsonicServer.BuildSettingsRequestUri(method);
            HttpClientHandler clientHandler = GetClientHandler();
            HttpClient client = GetClient(clientHandler);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            bool success;

            try
            {
                using (HttpResponseMessage response = cancelToken.HasValue ? await client.GetAsync(requestUri, cancelToken.Value) : await client.GetAsync(requestUri))
                    success = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            return success;
        }

        bool UseOldAuthenticationMethod()
        {
            return SubsonicServer.GetApiVersion() == null || SubsonicServer.GetApiVersion() < Subsonic.Common.SubsonicApiVersions.Version1_13_0;
        }

        public virtual HttpClientHandler GetClientHandler()
        {
            NetworkCredential networkCredential = UseOldAuthenticationMethod() ? new NetworkCredential(SubsonicServer.GetUserName(), SubsonicServer.GetPassword()) : null;
            bool preAuthenticate = UseOldAuthenticationMethod();

            return new HttpClientHandler
            {
                Credentials = networkCredential,
                PreAuthenticate = preAuthenticate,
                UseCookies = false,
                AllowAutoRedirect = true,
                Proxy = SubsonicServer.GetProxy(),
                UseProxy = SubsonicServer.GetProxy() != null
            };
        }

        public virtual HttpClient GetClient(HttpMessageHandler handler, bool addAuthentication = true)
        {
            HttpClient httpClient = new HttpClient(handler);

            if (addAuthentication && UseOldAuthenticationMethod())
                httpClient.DefaultRequestHeaders.Add(HttpHeaderField.Authorization, GetAuthorizationHeader());

            httpClient.DefaultRequestHeaders.Add(HttpHeaderField.UserAgent, SubsonicServer.GetClientName());

            return httpClient;
        }

        string GetAuthorizationHeader()
        {
            string authInfo = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", SubsonicServer.GetUserName(), SubsonicServer.GetPassword());
            authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
            return string.Format(CultureInfo.InvariantCulture, "Basic {0}", authInfo);
        }

        protected async Task<Response> DeserializeResponseAsync(string response)
        {
            Response result;

            if (!string.IsNullOrWhiteSpace(response))
            {
                result = await response.DeserializeFromXmlAsync<Response>();

                Version serverVersion = Version.Parse(result.Version);

                // Store the subsonic server version if we don't already have it
                if (SubsonicServer.GetApiVersion() == null || serverVersion != SubsonicServer.GetApiVersion())
                    SubsonicServer.SetApiVersion(serverVersion);
            }
            else
            {
                throw new SubsonicApiException("Empty HTTP response returned.");
            }

            return result;
        }
    }
}
