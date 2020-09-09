using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(ArtistInfo))]
    [XmlInclude(typeof(ArtistInfo2))]
    public class ArtistInfoBase
    {
        [XmlElement("biography")]
        public string Biography { get; set; }

        [XmlElement("largeImageUrl")]
        public string LargeImageUrl { get; set; }

        [XmlElement("lastFmUrl")]
        public string LastFmUrl { get; set; }

        [XmlElement("mediumImageUrl")]
        public string MediumImageUrl { get; set; }

        [XmlElement("musicBrainzId")]
        public string MusicBrainzId { get; set; }

        [XmlElement("smallImageUrl")]
        public string SmallImageUrl { get; set; }
    }
}