using Subsonic.Common.Enums;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class PodcastChannel
    {
        [XmlAttribute("coverArt")]
        public string CoverArt { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlElement("episode")]
        public List<PodcastEpisode> Episodes { get; set; }

        [XmlAttribute("errorMessage")]
        public string ErrorMessage { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("originalImageUrl")]
        public string OriginalImageUrl { get; set; }

        [XmlAttribute("status")]
        public PodcastStatus Status { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}