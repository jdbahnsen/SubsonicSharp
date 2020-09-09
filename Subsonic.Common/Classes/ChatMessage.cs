using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class ChatMessage
    {
        [XmlAttribute("message")]
        public string Message { get; set; }

        [XmlAttribute("time")]
        public long Time { get; set; }

        [XmlAttribute("username")]
        public string Username { get; set; }
    }
}