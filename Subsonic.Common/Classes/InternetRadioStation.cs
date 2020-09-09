using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class InternetRadioStation
    {
        [XmlAttribute("homePageUrl")]
        public string HomePageUrl { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("streamUrl")]
        public string StreamUrl { get; set; }
    }
}