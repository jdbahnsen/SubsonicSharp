﻿using Subsonic.Client.Enums;
using Subsonic.Client.Exceptions;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Common;

namespace Subsonic.Client
{
    public class SubsonicClient<T> : ISubsonicClient<T>
    {
        protected ISubsonicResponse<T> SubsonicResponse { get; set; }
        protected ISubsonicRequest<T> SubsonicRequest { private get; set; }
        public Uri ServerUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ProxyServerUrl { get; set; }
        public int ProxyPort { get; set; }
        public string ProxyUserName { get; set; }
        public string ProxyPassword { get; set; }
        public Version ServerApiVersion { get; set; }
        private bool EncodePasswords { get; set; }

        protected SubsonicClient(Uri serverUrl, string userName, string password, string name)
        {
            ServerUrl = serverUrl;
            UserName = userName;
            Password = password;
            Name = name;
        }

        public virtual async Task<bool> PingAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync(Methods.Ping, Versions.Version100, null, cancelToken);
        }

        public virtual async Task<License> GetLicenseAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<License>(Methods.GetLicense, Versions.Version100, null, cancelToken);
        }

        public virtual async Task<MusicFolders> GetMusicFoldersAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<MusicFolders>(Methods.GetMusicFolders, Versions.Version100, null, cancelToken);
        }

        public virtual async Task<NowPlaying> GetNowPlayingAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<NowPlaying>(Methods.GetNowPlaying, Versions.Version100, null, cancelToken);
        }

        public virtual async Task<Starred> GetStarredAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Starred>(Methods.GetStarred, Versions.Version180, null, cancelToken);
        }

        public virtual async Task<Starred2> GetStarred2Async(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Starred2>(Methods.GetStarred2, Versions.Version180, null, cancelToken);
        }

        public virtual async Task<Indexes> GetIndexesAsync(string musicFolderId = null, long? ifModifiedSince = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.MusicFolderId, musicFolderId);
            parameters.Add(Constants.IfModifiedSince, ifModifiedSince);

            return await SubsonicResponse.GetResponseAsync<Indexes>(Methods.GetIndexes, Versions.Version100, parameters, cancelToken);
        }

        public virtual async Task<Directory> GetMusicDirectoryAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<Directory>(Methods.GetMusicDirectory, Versions.Version100, parameters, cancelToken);
        }

        public virtual async Task<ArtistWithAlbumsID3> GetArtistAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<ArtistWithAlbumsID3>(Methods.GetArtist, Versions.Version180, parameters, cancelToken);
        }

        public virtual async Task<ArtistsID3> GetArtistsAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<ArtistsID3>(Methods.GetArtists, Versions.Version180, null, cancelToken);
        }

        public virtual async Task<AlbumID3> GetAlbumAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<AlbumID3>(Methods.GetAlbum, Versions.Version180, parameters, cancelToken);
        }

        public virtual async Task<Child> GetSongAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<Child>(Methods.GetSong, Versions.Version180, parameters, cancelToken);
        }

        public virtual async Task<Videos> GetVideosAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Videos>(Methods.GetVideos, Versions.Version180, null, cancelToken);
        }

        public virtual async Task<SearchResult> SearchAsync(string artist = null, string album = null, string title = null, string any = null, int? count = null, int? offset = null, long? newerThan = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Artist, artist);
            parameters.Add(Constants.Album, album);
            parameters.Add(Constants.Title, title);
            parameters.Add(Constants.Any, any);
            parameters.Add(Constants.Count, count);
            parameters.Add(Constants.Offset, offset);
            parameters.Add(Constants.NewerThan, newerThan);

            return await SubsonicResponse.GetResponseAsync<SearchResult>(Methods.Search, Versions.Version100, parameters, cancelToken);
        }

        public virtual async Task<SearchResult2> Search2Async(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Query, query, true);
            parameters.Add(Constants.ArtistCount, artistCount);
            parameters.Add(Constants.ArtistOffset, artistOffset);
            parameters.Add(Constants.AlbumCount, albumCount);
            parameters.Add(Constants.AlbumOffset, albumOffset);
            parameters.Add(Constants.SongCount, songCount);
            parameters.Add(Constants.SongOffset, songOffset);

            return await SubsonicResponse.GetResponseAsync<SearchResult2>(Methods.Search2, Versions.Version140, parameters, cancelToken);
        }

        public virtual async Task<SearchResult3> Search3Async(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Query, query, true);
            parameters.Add(Constants.ArtistCount, artistCount);
            parameters.Add(Constants.ArtistOffset, artistOffset);
            parameters.Add(Constants.AlbumCount, albumCount);
            parameters.Add(Constants.AlbumOffset, albumOffset);
            parameters.Add(Constants.SongCount, songCount);
            parameters.Add(Constants.SongOffset, songOffset);

            return await SubsonicResponse.GetResponseAsync<SearchResult3>(Methods.Search3, Versions.Version180, parameters, cancelToken);
        }

        public virtual async Task<Playlists> GetPlaylistsAsync(string username = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = Versions.Version100;
            var parameters = SubsonicParameters.Create();

            if (!string.IsNullOrWhiteSpace(username))
            {
                parameters.Add(Constants.Username, username);
                methodApiVersion = Versions.Version180;
            }

            return await SubsonicResponse.GetResponseAsync<Playlists>(Methods.GetPlaylists, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<PlaylistWithSongs> GetPlaylistAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<PlaylistWithSongs>(Methods.GetPlaylist, Versions.Version100, parameters, cancelToken);
        }

        public virtual async Task<bool> CreatePlaylistAsync(string playlistId = null, string name = null, IEnumerable<string> songId = null, CancellationToken? cancelToken = null)
        {
            if (!string.IsNullOrWhiteSpace(playlistId) && !string.IsNullOrWhiteSpace(name))
                throw new SubsonicApiException("Only one of playlist ID and name can be specified.");

            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);

            if (!string.IsNullOrWhiteSpace(playlistId))
                parameters.Add(Constants.PlaylistId, playlistId);
            else if (!string.IsNullOrWhiteSpace(name))
                parameters.Add(Constants.Name, name);
            else
                throw new SubsonicApiException("One of playlist ID and name must be specified.");

            parameters.Add(Constants.SongId, songId);

            return await SubsonicResponse.GetResponseAsync(Methods.CreatePlaylist, Versions.Version120, parameters, cancelToken);
        }

        public virtual async Task<bool> UpdatePlaylistAsync(string playlistId, string name = null, string comment = null, IEnumerable<string> songIdToAdd = null, IEnumerable<string> songIndexToRemove = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);
            parameters.Add(Constants.Name, name);
            parameters.Add(Constants.Comment, comment);
            parameters.Add(Constants.SongIdToAdd, songIdToAdd);
            parameters.Add(Constants.SongIndexToRemove, songIndexToRemove);

            return await SubsonicResponse.GetResponseAsync(Methods.UpdatePlaylist, Versions.Version180, parameters);
        }

        public virtual async Task<bool> DeletePlaylistAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeletePlaylist, Versions.Version120, parameters, cancelToken);
        }

        /// <summary>
        /// Returns the size of a cover art image.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the cover art file to download. Obtained by calls to getMusicDirectory.</param>
        /// <param name="size">If specified, scale image to this size.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>long</returns>
        public virtual async Task<long> GetCoverArtSizeAsync(string id, int? size = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Size, size);

            return await SubsonicResponse.GetImageSizeAsync(Methods.GetCoverArt, Versions.Version100, parameters, cancelToken);
        }

        public virtual async Task<IImageFormat<T>> GetCoverArtAsync(string id, int? size = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Size, size);

            return await SubsonicResponse.GetImageResponseAsync(Methods.GetCoverArt, Versions.Version100, parameters, cancelToken);
        }

        public virtual async Task<bool> ScrobbleAsync(string id, bool? submission = null, long? time = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = Versions.Version150;

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Submission, submission);

            if (time != null)
            {
                parameters.Add(Constants.Time, time);
                methodApiVersion = Versions.Version180;
            }

            return await SubsonicResponse.GetResponseAsync(Methods.Scrobble, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<Shares> GetSharesAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Shares>(Methods.GetShares, Versions.Version160, null, cancelToken);
        }

        public virtual async Task<bool> ChangePasswordAsync(string username, string password, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            if (EncodePasswords)
                password = string.Format(CultureInfo.InvariantCulture, "enc:{0}", password.ToHex());

            parameters.Add("password", password, true);

            return await SubsonicResponse.GetResponseAsync(Methods.ChangePassword, Versions.Version110, parameters, cancelToken);
        }

        public virtual async Task<User> GetUserAsync(string username, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            return await SubsonicResponse.GetResponseAsync<User>(Methods.GetUser, Versions.Version130, parameters, cancelToken);
        }

        public virtual async Task<IImageFormat<T>> GetAvatarAsync(string username, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            return await SubsonicResponse.GetImageResponseAsync(Methods.GetAvatar, Versions.Version180, parameters, cancelToken);
        }

        public virtual async Task<bool> StarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null, CancellationToken? cancelToken = null)
        {
            if (id == null && albumId == null && artistId == null)
                throw new SubsonicApiException("You must provide one of id, albumId or artistId");

            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);
            parameters.Add(Constants.Id, id);
            parameters.Add(Constants.AlbumId, albumId);
            parameters.Add(Constants.ArtistId, artistId);

            return await SubsonicResponse.GetResponseAsync(Methods.Star, Versions.Version180, parameters, cancelToken);
        }

        public virtual async Task<bool> UnStarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null, CancellationToken? cancelToken = null)
        {
            if (id == null && albumId == null && artistId == null)
                throw new SubsonicApiException("You must provide one of id, albumId or artistId");

            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);
            parameters.Add(Constants.Id, id);
            parameters.Add(Constants.AlbumId, albumId);
            parameters.Add(Constants.ArtistId, artistId);

            return await SubsonicResponse.GetResponseAsync(Methods.Unstar, Versions.Version180, parameters, cancelToken);
        }

        public virtual async Task<bool> SetRatingAsync(string id, int rating, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Rating, rating, true);

            return await SubsonicResponse.GetResponseAsync(Methods.SetRating, Versions.Version160, parameters, cancelToken);
        }

        public virtual async Task<bool> CreateUserAsync(string username, string password, string email, bool? ldapAuthenticated = null, bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null, bool? jukeboxRole = null, bool? downloadRole = null, bool? uploadRole = null, bool? playlistRole = null, bool? coverArtRole = null, bool? commentRole = null, bool? podcastRole = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            if (EncodePasswords)
                password = string.Format(CultureInfo.InvariantCulture, "enc:{0}", password.ToHex());

            parameters.Add(Constants.Password, password, true);
            parameters.Add(Constants.Email, email, true);
            parameters.Add(Constants.LdapAuthenticated, ldapAuthenticated);
            parameters.Add(Constants.AdminRole, adminRole);
            parameters.Add(Constants.SettingsRole, settingsRole);
            parameters.Add(Constants.StreamRole, streamRole);
            parameters.Add(Constants.JukeboxRole, jukeboxRole);
            parameters.Add(Constants.DownloadRole, downloadRole);
            parameters.Add(Constants.UploadRole, uploadRole);
            parameters.Add(Constants.PlaylistRole, playlistRole);
            parameters.Add(Constants.CoverArtRole, coverArtRole);
            parameters.Add(Constants.CommentRole, commentRole);
            parameters.Add(Constants.PodcastRole, podcastRole);

            return await SubsonicResponse.GetResponseAsync(Methods.CreateUser, Versions.Version130, parameters, cancelToken);
        }

        public virtual async Task<bool> DeleteUserAsync(string username, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeleteUser, Versions.Version130, parameters, cancelToken);
        }

        public virtual async Task<ChatMessages> GetChatMessagesAsync(double? since = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Since, since);

            return await SubsonicResponse.GetResponseAsync<ChatMessages>(Methods.GetChatMessages, Versions.Version120, parameters, cancelToken);
        }

        public virtual async Task<bool> AddChatMessageAsync(string message, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Message, message);

            return await SubsonicResponse.GetResponseAsync(Methods.AddChatMessage, Versions.Version120, parameters, cancelToken);
        }

        public virtual async Task<AlbumList> GetAlbumListAsync(AlbumListType type, int? size = null, int? offset = null, int? fromYear = null, int? toYear = null, string genre = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = Versions.Version120;

            if (type == AlbumListType.AlphabeticalByArtist || type == AlbumListType.AlphabeticalByName || type == AlbumListType.Starred)
                methodApiVersion = Versions.Version180;

            var albumListTypeName = type.GetXmlEnumAttribute();

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Type, albumListTypeName, true);
            parameters.Add(Constants.Size, size);
            parameters.Add(Constants.Offset, offset);

            if (type == AlbumListType.ByYear)
            {
                parameters.Add(Constants.FromYear, fromYear, true);
                parameters.Add(Constants.ToYear, toYear, true);

                methodApiVersion = Versions.Version1101;
            }

            if (type == AlbumListType.ByGenre)
            {
                parameters.Add(Constants.Genre, genre, true);

                methodApiVersion = Versions.Version1101;
            }

            return await SubsonicResponse.GetResponseAsync<AlbumList>(Methods.GetAlbumList, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<AlbumList2> GetAlbumList2Async(AlbumListType type, int? size = null, int? offset = null, int? fromYear = null, int? toYear = null, string genre = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = Versions.Version180;

            var albumListTypeName = type.GetXmlEnumAttribute();

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Type, albumListTypeName, true);
            parameters.Add(Constants.Size, size);
            parameters.Add(Constants.Offset, offset);

            if (type == AlbumListType.ByYear)
            {
                parameters.Add(Constants.FromYear, fromYear, true);
                parameters.Add(Constants.ToYear, toYear, true);

                methodApiVersion = Versions.Version1101;
            }

            if (type == AlbumListType.ByGenre)
            {
                parameters.Add(Constants.Genre, genre, true);

                methodApiVersion = Versions.Version1101;

            }

            return await SubsonicResponse.GetResponseAsync<AlbumList2>(Methods.GetAlbumList2, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<RandomSongs> GetRandomSongsAsync(int? size = null, string genre = null, int? fromYear = null, int? toYear = null, string musicFolderId = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Size, size);
            parameters.Add(Constants.Genre, genre);
            parameters.Add(Constants.FromYear, fromYear);
            parameters.Add(Constants.ToYear, toYear);
            parameters.Add(Constants.MusicFolderId, musicFolderId);

            return await SubsonicResponse.GetResponseAsync<RandomSongs>(Methods.GetRandomSongs, Versions.Version120, parameters, cancelToken);
        }

        public virtual async Task<Lyrics> GetLyricsAsync(string artist = null, string title = null, CancellationToken? cancelToken = null)
        {
            if (string.IsNullOrWhiteSpace(artist) && string.IsNullOrWhiteSpace(title))
                throw new SubsonicApiException("You must specify an artist and/or a title");

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Artist, artist);
            parameters.Add(Constants.Title, title);

            return await SubsonicResponse.GetResponseAsync<Lyrics>(Methods.GetLyrics, Versions.Version120, parameters, cancelToken);
        }

        public virtual async Task<JukeboxPlaylist> JukeboxControlAsync(CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Action, Constants.Get);

            return await SubsonicResponse.GetResponseAsync<JukeboxPlaylist>(Methods.JukeboxControl, Versions.Version120, parameters, cancelToken);
        }

        public virtual async Task<bool> JukeboxControlAsync(JukeboxControlAction action, int? index = null, float? gain = null, IEnumerable<string> id = null, CancellationToken? cancelToken = null)
        {
            var actionName = action.GetXmlEnumAttribute();

            if (string.IsNullOrWhiteSpace(actionName))
                throw new SubsonicApiException("You must provide valid action");

            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);

            parameters.Add(Constants.Action, actionName);

            if ((action == JukeboxControlAction.Skip || action == JukeboxControlAction.Remove) && index != null)
                parameters.Add(Constants.Index, index.ToString());

            if (action == JukeboxControlAction.Add)
            {
                if (id == null)
                    throw new SubsonicApiException("You must provide at least 1 ID.");

                parameters.Add(Constants.Id, id);
            }

            if (action == JukeboxControlAction.SetGain)
            {
                if (gain == null || (gain < 0 || gain > 1))
                    throw new SubsonicApiException("Gain value must be >= 0.0 and <= 1.0");

                parameters.Add(Constants.SetGain, gain.ToString());
            }

            return await SubsonicResponse.GetResponseAsync(Methods.JukeboxControl, Versions.Version120, parameters, cancelToken);
        }

        public virtual async Task<Podcasts> GetPodcastsAsync(string id = null, bool? includeEpisodes = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = Versions.Version160;

            var parameters = SubsonicParameters.Create();

            if (!string.IsNullOrWhiteSpace(id))
            {
                parameters.Add(Constants.Id, id);
                methodApiVersion = Versions.Version190;
            }

            if (includeEpisodes != null)
            {
                parameters.Add(Constants.IncludeEpisodes, includeEpisodes);
                methodApiVersion = Versions.Version190;
            }

            return await SubsonicResponse.GetResponseAsync<Podcasts>(Methods.GetPodcasts, methodApiVersion, parameters.Parameters.Count > 0 ? parameters : null, cancelToken);
        }

        public virtual async Task<Genres> GetGenresAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Genres>(Methods.GetGenres, Versions.Version190, null, cancelToken);
        }

        public virtual async Task<Songs> GetSongsByGenreAsync(string genre, int? count = null, int? offset = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Genre, genre, true);
            parameters.Add(Constants.Count, count);
            parameters.Add(Constants.Offset, offset);

            return await SubsonicResponse.GetResponseAsync<Songs>(Methods.GetSongsByGenre, Versions.Version190, parameters, cancelToken);
        }

        public virtual Task<long> StreamAsync(string id, string path, int? maxBitRate = null, StreamFormat? format = null, int? timeOffset = null, string size = null, bool? estimateContentLength = null, CancellationToken? cancelToken = null, bool noResponse = false)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> DownloadAsync(string id, string path, bool pathOverride = false, CancellationToken? cancelToken = null)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<string> HlsAsync(string id, int? bitRate = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = Versions.Version180;

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            if (bitRate != null)
            {
                parameters.Add(Constants.BitRate, bitRate);

                methodApiVersion = Versions.Version190;
            }

            return await SubsonicResponse.GetStringResponseAsync(Methods.Hls, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<bool> RefreshPodcastsAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync(Methods.RefreshPodcasts, Versions.Version190, null, cancelToken);
        }

        public virtual async Task<bool> CreatePodcastChannelAsync(string url, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Url, url, true);

            return await SubsonicResponse.GetResponseAsync(Methods.CreatePodcastChannel, Versions.Version190, parameters, cancelToken);
        }

        public virtual async Task<bool> DeletePodcastChannelAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeletePodcastChannel, Versions.Version190, parameters, cancelToken);
        }

        public virtual async Task<bool> DeletePodcastEpisodeAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeletePodcastEpisode, Versions.Version190, parameters, cancelToken);
        }

        public virtual Task<long> DownloadPodcastEpisodeAsync(string id, string path, bool pathOverride = false, CancellationToken? cancelToken = null)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<InternetRadioStations> GetInternetRadioStationsAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<InternetRadioStations>(Methods.GetInternetRadioStations, Versions.Version190, null, cancelToken);
        }

        public virtual async Task<Bookmarks> GetBookmarksAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Bookmarks>(Methods.GetBookmarks, Versions.Version190, null, cancelToken);
        }

        public virtual async Task<bool> CreateBookmarkAsync(string id, long position, string comment = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Position, position, true);
            parameters.Add(Constants.Comment, comment);

            return await SubsonicResponse.GetResponseAsync(Methods.CreateBookmark, Versions.Version190, parameters, cancelToken);
        }

        public virtual async Task<bool> DeleteBookmarkAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeleteBookmark, Versions.Version190, parameters, cancelToken);
        }

        public virtual async Task<bool> UpdateUserAsync(string username, string password = null, string email = null, bool? ldapAuthenticated = null, bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null, bool? jukeboxRole = null, bool? downloadRole = null, bool? uploadRole = null, bool? coverArtRole = null, bool? commentRole = null, bool? podcastRole = null, bool? shareRole = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);
            parameters.Add(Constants.Password, password);
            parameters.Add(Constants.Email, email);
            parameters.Add(Constants.LdapAuthenticated, ldapAuthenticated);
            parameters.Add(Constants.AdminRole, adminRole);
            parameters.Add(Constants.SettingsRole, settingsRole);
            parameters.Add(Constants.StreamRole, streamRole);
            parameters.Add(Constants.JukeboxRole, jukeboxRole);
            parameters.Add(Constants.DownloadRole, downloadRole);
            parameters.Add(Constants.UploadRole, uploadRole);
            parameters.Add(Constants.CoverArtRole, coverArtRole);
            parameters.Add(Constants.CommentRole, commentRole);
            parameters.Add(Constants.PodcastRole, podcastRole);
            parameters.Add(Constants.ShareRole, shareRole);

            return await SubsonicResponse.GetResponseAsync(Methods.UpdateUser, Versions.Version1101, parameters, cancelToken);
        }

        public virtual async Task<Shares> CreateShareAsync(IEnumerable<string> id, string description = null, long? expires = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Description, description);
            parameters.Add(Constants.Expires, expires);

            return await SubsonicResponse.GetResponseAsync<Shares>(Methods.CreateShare, Versions.Version160, parameters, cancelToken);
        }

        public virtual async Task<bool> UpdateShareAsync(string id, string description = null, long? expires = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Description, description);
            parameters.Add(Constants.Expires, expires);

            return await SubsonicResponse.GetResponseAsync(Methods.UpdateShare, Versions.Version160, parameters, cancelToken);
        }

        public virtual async Task<bool> DeleteShareAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeleteShare, Versions.Version160, parameters, cancelToken);
        }

        public virtual async Task<Users> GetUsersAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Users>(Methods.GetUsers, Versions.Version180, null, cancelToken);
        }

        /// <summary>
        /// Downloads a given media file. Similar to stream, but this method returns the original media data without transcoding or downsampling.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to download. Obtained by calls to GetMusicDirectory.</param>W
        /// <returns>long</returns>
        public Uri BuildDownloadUrl(string id)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return SubsonicRequest.BuildRequestUriUser(Methods.Download, Versions.Version100, parameters);
        }

        /// <summary>
        /// Streams a given media file.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to stream. Obtained by calls to getMusicDirectory.</param>
        /// <param name="maxBitRate">(Since 1.2.0) If specified, the server will attempt to limit the bitrate to this value, in kilobits per second. If set to zero, no limit is imposed.</param>
        /// <param name="format">(Since 1.6.0) Specifies the preferred target format (e.g., "mp3" or "flv") in case there are multiple applicable transcodings</param>
        /// <param name="timeOffset">Only applicable to video streaming. If specified, start streaming at the given offset (in seconds) into the video. Typically used to implement video skipping.</param>
        /// <param name="size">(Since 1.6.0) Only applicable to video streaming. Requested video size specified as WxH, for instance "640x480".</param>
        /// <param name="estimateContentLength">(Since 1.8.0). If set to "true", the Content-Length HTTP header will be set to an estimated value for transcoded or downsampled media.</param>
        /// <returns>long</returns>
        public Uri BuildStreamUrl(string id, int? maxBitRate = null, StreamFormat? format = null, int? timeOffset = null, string size = null, bool? estimateContentLength = null)
        {
            var methodApiVersion = Versions.Version120;

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            if (maxBitRate != null && maxBitRate != 0)
                parameters.Add(Constants.MaxBitRate, maxBitRate);

            if (format != null)
            {
                var streamFormatName = format.GetXmlEnumAttribute();

                if (streamFormatName != null)
                {
                    parameters.Add(Constants.StreamFormat, streamFormatName);
                    methodApiVersion = Versions.Version160;
                }
            }

            if (timeOffset != null)
            {
                parameters.Add(Constants.TimeOffset, timeOffset);
                methodApiVersion = Versions.Version160;
            }

            if (!string.IsNullOrWhiteSpace(size))
            {
                parameters.Add(Constants.Size, size);
                methodApiVersion = Versions.Version160;
            }

            if (estimateContentLength != null)
            {
                parameters.Add(Constants.EstimateContentLength, estimateContentLength);
                methodApiVersion = Versions.Version180;
            }

            return SubsonicRequest.BuildRequestUriUser(Methods.Stream, methodApiVersion, parameters);
        }
    }
}