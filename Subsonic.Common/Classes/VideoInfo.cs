using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class VideoInfo
    {
        [XmlElement("audioTrack")]
        public List<AudioTrack> AudioTrack { get; set; }

        [XmlElement("captions")]
        public List<Captions> Captions { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("conversion")]
        public List<VideoConversion> VideoConversion { get; set; }
    }
}