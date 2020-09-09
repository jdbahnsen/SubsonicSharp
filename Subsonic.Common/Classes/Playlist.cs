using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(PlaylistWithSongs))]
    public class Playlist
    {
        [XmlIgnore]
        private bool? _public;

        [XmlElement("allowedUser")]
        public List<string> AllowedUsers { get; set; }

        [XmlAttribute("changed")]
        public DateTime Changed { get; set; }

        [XmlAttribute("comment")]
        public string Comment { get; set; }

        [XmlAttribute("coverArt")]
        public string CoverArt { get; set; }

        [XmlAttribute("created")]
        public DateTime Created { get; set; }

        [XmlAttribute("duration")]
        public int Duration { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("owner")]
        public string Owner { get; set; }

        [XmlAttribute("public")]
        public bool Public
        {
            get => _public.GetValueOrDefault();
            set => _public = value;
        }

        [XmlAttribute("songCount")]
        public int SongCount { get; set; }

        public bool ShouldSerializePublic()
        {
            return _public.HasValue;
        }
    }
}