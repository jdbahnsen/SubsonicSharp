using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class VideoConversion
    {
        [XmlIgnore]
        private int? _audioTrackId;

        [XmlIgnore]
        private int? _bitRate;

        [XmlAttribute("audioTrackId")]
        public int AudioTrackId
        {
            get => _audioTrackId.GetValueOrDefault();
            set => _audioTrackId = value;
        }

        [XmlAttribute("bitRate")]
        public int BitRate
        {
            get => _bitRate.GetValueOrDefault();
            set => _bitRate = value;
        }

        [XmlAttribute("id")]
        public string Id { get; set; }

        public bool ShouldSerializeAudioTrackId()
        {
            return _bitRate.HasValue;
        }

        public bool ShouldSerializeBitRate()
        {
            return _bitRate.HasValue;
        }
    }
}