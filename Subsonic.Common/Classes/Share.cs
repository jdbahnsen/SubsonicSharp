using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Share
    {
        [XmlIgnore]
        private DateTime? _expires;

        [XmlIgnore]
        private DateTime? _lastVisited;

        [XmlAttribute("created")]
        public DateTime Created { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlElement("entry")]
        public List<Child> Entries { get; set; }

        [XmlAttribute("expires")]
        public DateTime Expires
        {
            get => _expires.GetValueOrDefault();
            set => _expires = value;
        }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("lastVisited")]
        public DateTime LastVisited
        {
            get => _lastVisited.GetValueOrDefault();
            set => _lastVisited = value;
        }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlAttribute("visitCount")]
        public int VisitCount { get; set; }

        public bool ShouldSerializeExpires()
        {
            return _expires.HasValue;
        }

        public bool ShouldSerializeLastVisited()
        {
            return _lastVisited.HasValue;
        }
    }
}