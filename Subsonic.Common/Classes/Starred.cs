using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Starred
    {
        [XmlElement("album")]
        public List<Child> Albums { get; set; }

        [XmlElement("artist")]
        public List<Artist> Artists { get; set; }

        [XmlElement("song")]
        public List<Child> Songs { get; set; }
    }
}