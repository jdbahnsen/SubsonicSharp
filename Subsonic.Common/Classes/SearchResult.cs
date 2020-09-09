using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class SearchResult
    {
        [XmlElement("match")]
        public List<Child> Matches { get; set; }

        [XmlAttribute("offset")]
        public int Offset { get; set; }

        [XmlAttribute("totalHits")]
        public int TotalHits { get; set; }
    }
}