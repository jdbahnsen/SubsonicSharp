using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(JukeboxPlaylist))]
    public class JukeboxStatus
    {
        [XmlIgnore]
        private int? _position;

        [XmlAttribute("currentIndex")]
        public int CurrentIndex { get; set; }

        [XmlAttribute("gain")]
        public float Gain { get; set; }

        [XmlAttribute("playing")]
        public bool Playing { get; set; }

        [XmlAttribute("position")]
        public int Position
        {
            get => _position.GetValueOrDefault();
            set => _position = value;
        }

        public bool ShouldSerializePosition()
        {
            return _position.HasValue;
        }
    }
}