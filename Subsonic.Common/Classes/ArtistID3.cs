﻿using System;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [XmlInclude(typeof(ArtistWithAlbumsID3))]
    [Serializable]
    public class ArtistID3
    {
        [XmlAttribute("albumCount")]
        public int AlbumCount;

        [XmlAttribute("coverArt")]
        public string CoverArt;

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("starred")]
        public DateTime Starred;
    }
}