using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class IndexID3
    {
        [XmlElement("artist")]
        public List<ArtistID3> Artists { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}