using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(ArtistWithAlbumsID3))]
    public class ArtistID3
    {
        [XmlIgnore]
        private DateTime? _starred;

        [XmlAttribute("albumCount")]
        public int AlbumCount { get; set; }

        [XmlAttribute("artistImageUrl")]
        public string ArtistImageUrl { get; set; }

        [XmlAttribute("coverArt")]
        public string CoverArt { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("starred")]
        public DateTime Starred
        {
            get => _starred.GetValueOrDefault();
            set => _starred = value;
        }

        public bool ShouldSerializeStarred()
        {
            return _starred.HasValue;
        }
    }
}