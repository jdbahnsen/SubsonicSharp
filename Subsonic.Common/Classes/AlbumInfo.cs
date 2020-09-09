using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class AlbumInfo
    {
        [XmlElement("largeImageUrl")]
        public string LargeImageUrl { get; set; }

        [XmlElement("lastFmUrl")]
        public string LastFmUrl { get; set; }

        [XmlElement("mediumImageUrl")]
        public string MediumImageUrl { get; set; }

        [XmlElement("musicBrainzId")]
        public string MusicBrainzId { get; set; }

        [XmlElement("notes")]
        public string Notes { get; set; }

        [XmlElement("smallImageUrl")]
        public string SmallImageUrl { get; set; }
    }
}