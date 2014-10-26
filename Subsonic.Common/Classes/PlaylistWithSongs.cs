﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class PlaylistWithSongs : Playlist
    {
        [XmlElement("entry")]
        public List<Child> Entry;
    }
}
