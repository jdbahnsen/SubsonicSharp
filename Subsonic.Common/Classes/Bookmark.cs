using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Bookmark
    {
        [XmlAttribute("changed")]
        public DateTime Changed { get; set; }

        [XmlElement("entry")]
        public List<Child> Children { get; set; }

        [XmlAttribute("comment")]
        public string Comment { get; set; }

        [XmlAttribute("created")]
        public DateTime Created { get; set; }

        [XmlAttribute("position")]
        public long Position { get; set; }

        [XmlAttribute("username")]
        public string Username { get; set; }
    }
}