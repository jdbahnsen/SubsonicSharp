using Subsonic.Common.Enums;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(PodcastEpisode))]
    [XmlInclude(typeof(NowPlayingEntry))]
    public class Child : IEqualityComparer<Child>
    {
        [XmlIgnore]
        private double? _averageRating;

        [XmlIgnore]
        private int? _bitRate;

        [XmlIgnore]
        private long? _bookmarkPosition;

        [XmlIgnore]
        private DateTime? _created;

        [XmlIgnore]
        private int? _discNumber;

        [XmlIgnore]
        private int? _duration;

        [XmlIgnore]
        private bool? _isVideo;

        [XmlIgnore]
        private int? _originalHeight;

        [XmlIgnore]
        private int? _originalWidth;

        [XmlIgnore]
        private long? _playCount;

        [XmlIgnore]
        private long? _size;

        [XmlIgnore]
        private DateTime? _starred;

        [XmlIgnore]
        private int? _track;

        [XmlIgnore]
        private MediaType? _type;

        [XmlIgnore]
        private int? _userRating;

        [XmlIgnore]
        private int? _year;

        [XmlAttribute("album")]
        public string Album { get; set; }

        [XmlAttribute("albumId")]
        public string AlbumId { get; set; }

        [XmlAttribute("artist")]
        public string Artist { get; set; }

        [XmlAttribute("artistId")]
        public string ArtistId { get; set; }

        [XmlAttribute("averageRating")]
        public double AverageRating
        {
            get => _averageRating.GetValueOrDefault();
            set => _averageRating = value;
        }

        [XmlAttribute("bitRate")]
        public int BitRate
        {
            get => _bitRate.GetValueOrDefault();
            set => _bitRate = value;
        }

        [XmlAttribute("bookmarkPosition")]
        public long BookmarkPosition
        {
            get => _bookmarkPosition.GetValueOrDefault();
            set => _bookmarkPosition = value;
        }

        [XmlAttribute("contentType")]
        public string ContentType { get; set; }

        [XmlAttribute("coverArt")]
        public string CoverArt { get; set; }

        [XmlAttribute("created")]
        public DateTime Created
        {
            get => _created.GetValueOrDefault();
            set => _created = value;
        }

        [XmlAttribute("discNumber")]
        public int DiscNumber
        {
            get => _discNumber.GetValueOrDefault();
            set => _discNumber = value;
        }

        [XmlAttribute("duration")]
        public int Duration
        {
            get => _duration.GetValueOrDefault();
            set => _duration = value;
        }

        [XmlAttribute("genre")]
        public string Genre { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("isDir")]
        public bool IsDir { get; set; }

        [XmlAttribute("isVideo")]
        public bool IsVideo
        {
            get => _isVideo.GetValueOrDefault();
            set => _isVideo = value;
        }

        [XmlAttribute("originalHeight")]
        public int OriginalHeight
        {
            get => _originalHeight.GetValueOrDefault();
            set => _originalHeight = value;
        }

        [XmlAttribute("originalWidth")]
        public int OriginalWidth
        {
            get => _originalWidth.GetValueOrDefault();
            set => _originalWidth = value;
        }

        [XmlAttribute("parent")]
        public string Parent { get; set; }

        [XmlAttribute("path")]
        public string Path { get; set; }

        [XmlAttribute("playCount")]
        public long PlayCount
        {
            get => _playCount.GetValueOrDefault();
            set => _playCount = value;
        }

        [XmlAttribute("size")]
        public long Size
        {
            get => _size.GetValueOrDefault();
            set => _size = value;
        }

        [XmlAttribute("starred")]
        public DateTime Starred
        {
            get => _starred.GetValueOrDefault();
            set => _starred = value;
        }

        [XmlAttribute("suffix")]
        public string Suffix { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("track")]
        public int Track
        {
            get => _track.GetValueOrDefault();
            set => _track = value;
        }

        [XmlAttribute("transcodedContentType")]
        public string TranscodedContentType { get; set; }

        [XmlAttribute("transcodedSuffix")]
        public string TranscodedSuffix { get; set; }

        [XmlAttribute("type")]
        public MediaType Type
        {
            get => _type.GetValueOrDefault();
            set => _type = value;
        }

        [XmlAttribute("userRating")]
        public int UserRating
        {
            get => _userRating.GetValueOrDefault();
            set => _userRating = value;
        }

        [XmlAttribute("year")]
        public int Year
        {
            get => _year.GetValueOrDefault();
            set => _year = value;
        }

        public static bool operator !=(Child left, Child right)
        {
            return !(left == right);
        }

        public static bool operator ==(Child left, Child right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            return string.Equals(left.Id, right.Id);
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as Child);
        }

        public bool Equals(Child x, Child y)
        {
            return x?.Equals(y) == true;
        }

        public override int GetHashCode()
        {
            const int hash = 13;
            const int hashFactor = 7;

            return (hash * hashFactor) + Id.GetHashCode();
        }

        public int GetHashCode(Child obj)
        {
            return obj.GetHashCode();
        }

        public bool ShouldSerializeAverageRating()
        {
            return _averageRating.HasValue;
        }

        public bool ShouldSerializeBitRate()
        {
            return _bitRate.HasValue;
        }

        public bool ShouldSerializeBookmarkPosition()
        {
            return _bookmarkPosition.HasValue;
        }

        public bool ShouldSerializeCreated()
        {
            return _created.HasValue;
        }

        public bool ShouldSerializeDiscNumber()
        {
            return _discNumber.HasValue;
        }

        public bool ShouldSerializeDuration()
        {
            return _duration.HasValue;
        }

        public bool ShouldSerializeIsVideo()
        {
            return _isVideo.HasValue;
        }

        public bool ShouldSerializeOriginalHeight()
        {
            return _originalHeight.HasValue;
        }

        public bool ShouldSerializeOriginalWidth()
        {
            return _originalWidth.HasValue;
        }

        public bool ShouldSerializePlayCount()
        {
            return _playCount.HasValue;
        }

        public bool ShouldSerializeSize()
        {
            return _size.HasValue;
        }

        public bool ShouldSerializeStarred()
        {
            return _starred.HasValue;
        }

        public bool ShouldSerializeTrack()
        {
            return _track.HasValue;
        }

        public bool ShouldSerializeType()
        {
            return _type.HasValue;
        }

        public bool ShouldSerializeUserRating()
        {
            return _userRating.HasValue;
        }

        public bool ShouldSerializeYear()
        {
            return _year.HasValue;
        }

        private bool Equals(Child item)
        {
            return item != null && this == item;
        }
    }
}