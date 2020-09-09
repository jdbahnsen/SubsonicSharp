using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Lyrics
    {
        [XmlAttribute("artist")]
        public string Artist { get; set; }

        [XmlText]
        public string Text { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }
    }
}