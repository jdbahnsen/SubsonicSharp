using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Genre
    {
        [XmlAttribute("albumCount")]
        public int AlbumCount { get; set; }

        [XmlText]
        public string Name { get; set; }

        [XmlAttribute("songCount")]
        public int SongCount { get; set; }
    }
}