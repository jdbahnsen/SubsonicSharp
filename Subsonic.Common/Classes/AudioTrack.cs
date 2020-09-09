using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class AudioTrack
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("languageCode")]
        public string LanguageCode { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}