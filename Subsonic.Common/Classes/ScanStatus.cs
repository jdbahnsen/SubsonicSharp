using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class ScanStatus
    {
        [XmlIgnore]
        private long? _count;

        [XmlAttribute("count")]
        public long Count
        {
            get => _count.GetValueOrDefault();
            set => _count = value;
        }

        [XmlAttribute("scanning")]
        public bool Scanning { get; set; }

        public bool ShouldSerializeCount()
        {
            return _count.HasValue;
        }
    }
}