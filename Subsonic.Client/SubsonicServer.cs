﻿using System;
using Subsonic.Client.Interfaces;

namespace Subsonic.Client
{
    public class SubsonicServer : ISubsonicServer
    {
        Uri Url { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string ClientName { get; set; }
        Version ApiVersion { get; set; }

        protected SubsonicServer(Uri serverUrl, string userName, string password, string clientName)
        {
            Url = serverUrl;
            UserName = userName;
            Password = password;
            ClientName = clientName;
        }

        public Uri GetUrl()
        {
            return Url;
        }

        public void SetUrl(string url)
        {
            Url = new Uri(url);
        }

        public void SetUrl(Uri url)
        {
            Url = url;
        }

        public string GetUserName()
        {
            return UserName;
        }

        public void SetUserName(string username)
        {
            UserName = username;
        }

        public string GetPassword()
        {
            return Password;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public string GetClientName()
        {
            return ClientName;
        }

        public void SetClientName(string clientName)
        {
            ClientName = clientName;
        }

        public Version GetApiVersion()
        {
            return ApiVersion;
        }

        public void SetApiVersion(Version apiVersion)
        {
            ApiVersion = apiVersion;
        }
    }
}

