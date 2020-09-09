using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Indexes
    {
        [XmlElement("child")]
        public List<Child> Children { get; set; }

        [XmlAttribute("ignoredArticles")]
        public string IgnoredArticles { get; set; }

        [XmlElement("index")]
        public List<Index> Items { get; set; }

        [XmlAttribute("lastModified")]
        public long LastModified { get; set; }

        [XmlElement("shortcut")]
        public List<Artist> Shortcuts { get; set; }
    }
}