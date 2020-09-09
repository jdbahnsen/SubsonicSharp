using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Captions
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}