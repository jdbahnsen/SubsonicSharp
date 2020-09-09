using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Directory
    {
        [XmlIgnore]
        private double? _averageRating;

        [XmlIgnore]
        private long? _playCount;

        [XmlIgnore]
        private DateTime? _starred;

        [XmlIgnore]
        private int? _userRating;

        [XmlAttribute("averageRating")]
        public double AverageRating
        {
            get => _averageRating.GetValueOrDefault();
            set => _averageRating = value;
        }

        [XmlElement("child")]
        public List<Child> Children { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parent")]
        public string Parent { get; set; }

        [XmlAttribute("playCount")]
        public long PlayCount
        {
            get => _playCount.GetValueOrDefault();
            set => _playCount = value;
        }

        [XmlAttribute("starred")]
        public DateTime Starred
        {
            get => _starred.GetValueOrDefault();
            set => _starred = value;
        }

        [XmlAttribute("userRating")]
        public int UserRating
        {
            get => _userRating.GetValueOrDefault();
            set => _userRating = value;
        }

        public bool ShouldSerializeAverageRating()
        {
            return _averageRating.HasValue;
        }

        public bool ShouldSerializePlayCount()
        {
            return _playCount.HasValue;
        }

        public bool ShouldSerializeStarred()
        {
            return _starred.HasValue;
        }

        public bool ShouldSerializeUserRating()
        {
            return _userRating.HasValue;
        }
    }
}