using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(AlbumWithSongsID3))]
    public class AlbumID3
    {
        [XmlIgnore]
        private long? _playCount;

        [XmlIgnore]
        private DateTime? _starred;

        [XmlIgnore]
        private int? _year;

        [XmlAttribute("artist")]
        public string Artist { get; set; }

        [XmlAttribute("artistId")]
        public string ArtistId { get; set; }

        [XmlAttribute("coverArt")]
        public string CoverArt { get; set; }

        [XmlAttribute("created")]
        public DateTime Created { get; set; }

        [XmlAttribute("duration")]
        public int Duration { get; set; }

        [XmlAttribute("genre")]
        public string Genre { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("playCount")]
        public long PlayCount
        {
            get => _playCount.GetValueOrDefault();
            set => _playCount = value;
        }

        [XmlAttribute("songCount")]
        public int SongCount { get; set; }

        [XmlAttribute("starred")]
        public DateTime Starred
        {
            get => _starred.GetValueOrDefault();
            set => _starred = value;
        }

        [XmlAttribute("year")]
        public int Year
        {
            get => _year.GetValueOrDefault();
            set => _year = value;
        }

        public bool ShouldSerializePlayCount()
        {
            return _playCount.HasValue;
        }

        public bool ShouldSerializeStarred()
        {
            return _starred.HasValue;
        }

        public bool ShouldSerializeYear()
        {
            return _year.HasValue;
        }
    }
}