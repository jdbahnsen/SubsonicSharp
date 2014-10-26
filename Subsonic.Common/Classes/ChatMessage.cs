﻿using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class ChatMessage
    {
        [XmlAttribute("message")]
        public string Message;

        [XmlAttribute("time")]
        public long Time;

        [XmlAttribute("username")]
        public string Username;
    }
}
