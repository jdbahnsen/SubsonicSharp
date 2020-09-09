using Subsonic.Common.Enums;
using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class PodcastEpisode : Child
    {
        [XmlIgnore]
        private DateTime? _publishDate;

        [XmlAttribute("channelId")]
        public string ChannelId { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlAttribute("publishDate")]
        public DateTime PublishDate
        {
            get => _publishDate.GetValueOrDefault();
            set => _publishDate = value;
        }

        [XmlAttribute("status")]
        public PodcastStatus Status { get; set; }

        [XmlAttribute("streamId")]
        public string StreamId { get; set; }

        public bool ShouldSerializePublishDate()
        {
            return _publishDate.HasValue;
        }
    }
}