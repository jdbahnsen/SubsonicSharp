﻿using Subsonic.Common.Enums;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlRoot("subsonic-response", Namespace = null, IsNullable = false)]
    public class Response
    {
        [XmlElement("album", typeof(AlbumWithSongsID3))]
        [XmlElement("albumInfo", typeof(AlbumInfo))]
        [XmlElement("albumInfo2", typeof(AlbumInfo))]
        [XmlElement("albumList", typeof(AlbumList))]
        [XmlElement("albumList2", typeof(AlbumList2))]
        [XmlElement("artist", typeof(ArtistWithAlbumsID3))]
        [XmlElement("artistInfo", typeof(ArtistInfo))]
        [XmlElement("artistInfo2", typeof(ArtistInfo2))]
        [XmlElement("artists", typeof(ArtistsID3))]
        [XmlElement("bookmarks", typeof(Bookmarks))]
        [XmlElement("chatMessages", typeof(ChatMessages))]
        [XmlElement("directory", typeof(Directory))]
        [XmlElement("error", typeof(Error))]
        [XmlElement("genres", typeof(Genres))]
        [XmlElement("indexes", typeof(Indexes))]
        [XmlElement("internetRadioStations", typeof(InternetRadioStations))]
        [XmlElement("jukeboxPlaylist", typeof(JukeboxPlaylist))]
        [XmlElement("jukeboxStatus", typeof(JukeboxStatus))]
        [XmlElement("license", typeof(License))]
        [XmlElement("lyrics", typeof(Lyrics))]
        [XmlElement("musicFolders", typeof(MusicFolders))]
        [XmlElement("newestPodcasts", typeof(NewestPodcasts))]
        [XmlElement("nowPlaying", typeof(NowPlaying))]
        [XmlElement("playlist", typeof(PlaylistWithSongs))]
        [XmlElement("playlists", typeof(Playlists))]
        [XmlElement("playQueue", typeof(PlayQueue))]
        [XmlElement("podcasts", typeof(Podcasts))]
        [XmlElement("randomSongs", typeof(RandomSongs))]
        [XmlElement("scanStatus", typeof(ScanStatus))]
        [XmlElement("searchResult", typeof(SearchResult))]
        [XmlElement("searchResult2", typeof(SearchResult2))]
        [XmlElement("searchResult3", typeof(SearchResult3))]
        [XmlElement("shares", typeof(Shares))]
        [XmlElement("similarSongs", typeof(SimilarSongs))]
        [XmlElement("similarSongs2", typeof(SimilarSongs2))]
        [XmlElement("song", typeof(Child))]
        [XmlElement("songsByGenre", typeof(Songs))]
        [XmlElement("starred", typeof(Starred))]
        [XmlElement("starred2", typeof(Starred2))]
        [XmlElement("topSongs", typeof(TopSongs))]
        [XmlElement("user", typeof(User))]
        [XmlElement("users", typeof(Users))]
        [XmlElement("videos", typeof(Videos))]
        [XmlElement("videoInfo", typeof(VideoInfo))]
        [XmlChoiceIdentifier(nameof(ItemElementName))]
        public dynamic Item { get; set; }

        [XmlIgnore]
        public ItemChoiceType ItemElementName { get; set; }

        [XmlAttribute("status")]
        public ResponseStatus Status { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }
    }
}