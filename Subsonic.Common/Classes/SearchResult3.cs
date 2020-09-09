using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class SearchResult3
    {
        [XmlElement("album")]
        public List<AlbumID3> Albums { get; set; }

        [XmlElement("artist")]
        public List<ArtistID3> Artists { get; set; }

        [XmlElement("song")]
        public List<Child> Songs { get; set; }
    }
}