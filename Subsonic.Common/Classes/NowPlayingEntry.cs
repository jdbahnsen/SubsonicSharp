using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class NowPlayingEntry : Child
    {
        [XmlAttribute("minutesAgo")]
        public int MinutesAgo { get; set; }

        [XmlAttribute("playerId")]
        public int PlayerId { get; set; }

        [XmlAttribute("playerName")]
        public string PlayerName { get; set; }

        [XmlAttribute("username")]
        public string Username { get; set; }
    }
}