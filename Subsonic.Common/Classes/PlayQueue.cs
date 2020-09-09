using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class PlayQueue
    {
        [XmlIgnore]
        private long? _position;

        [XmlAttribute("changed")]
        public DateTime Changed { get; set; }

        [XmlAttribute("changedBy")]
        public string ChangedBy { get; set; }

        [XmlAttribute("current")]
        public string Current { get; set; }

        [XmlElement("entry")]
        public List<Child> Entries { get; set; }

        [XmlAttribute("position")]
        public long Position
        {
            get => _position.GetValueOrDefault();
            set => _position = value;
        }

        [XmlAttribute("username")]
        public string Username { get; set; }

        public bool ShouldSerializePosition()
        {
            return _position.HasValue;
        }
    }
}