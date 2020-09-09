using Subsonic.Common.Enums;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Error
    {
        [XmlAttribute("code")]
        public ErrorCode Code { get; set; }

        [XmlAttribute("message")]
        public string Message { get; set; }
    }
}