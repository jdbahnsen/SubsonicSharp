﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class Lyrics
    {
        [XmlAttribute("artist")]
        public string Artist;

        [XmlText]
        public List<string> Text;

        [XmlAttribute("title")]
        public string Title;
    }
}
