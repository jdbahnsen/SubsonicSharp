using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class User
    {
        [XmlIgnore]
        private DateTime? _avatarLastChanged;

        [XmlIgnore]
        private int? _maxBitrate;

        [XmlAttribute("adminRole")]
        public bool AdminRole { get; set; }

        [XmlAttribute("avatarLastChanged")]
        public DateTime AvatarLastChanged
        {
            get => _avatarLastChanged.GetValueOrDefault();
            set => _avatarLastChanged = value;
        }

        [XmlAttribute("commentRole")]
        public bool CommentRole { get; set; }

        [XmlAttribute("coverArtRole")]
        public bool CoverArtRole { get; set; }

        [XmlAttribute("downloadRole")]
        public bool DownloadRole { get; set; }

        [XmlAttribute("email")]
        public string Email { get; set; }

        [XmlElement("folder")]
        public List<int> Folder { get; set; }

        [XmlAttribute("jukeboxRole")]
        public bool JukeboxRole { get; set; }

        [XmlAttribute("maxBitrate")]
        public int MaxBitrate
        {
            get => _maxBitrate.GetValueOrDefault();
            set => _maxBitrate = value;
        }

        [XmlAttribute("playlistRole")]
        public bool PlaylistRole { get; set; }

        [XmlAttribute("podcastRole")]
        public bool PodcastRole { get; set; }

        [XmlAttribute("scrobblingEnabled")]
        public bool ScrobblingEnabled { get; set; }

        [XmlAttribute("settingsRole")]
        public bool SettingsRole { get; set; }

        [XmlAttribute("shareRole")]
        public bool ShareRole { get; set; }

        [XmlAttribute("streamRole")]
        public bool StreamRole { get; set; }

        [XmlAttribute("uploadRole")]
        public bool UploadRole { get; set; }

        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlAttribute("videoConversionRole")]
        public bool VideoConversionRole { get; set; }

        public bool ShouldSerializeAvatarLastChanged()
        {
            return _avatarLastChanged.HasValue;
        }

        public bool ShouldSerializeMaxBitrate()
        {
            return _maxBitrate.HasValue;
        }
    }
}