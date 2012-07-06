﻿using System;
using System.Collections.Generic;
using System.Linq;
using Espera.Core.Audio;
using Espera.Core.Library;
using Moq;
using NUnit.Framework;

namespace Espera.Core.Tests
{
    [TestFixture]
    public class PlaylistTest
    {
        [Test]
        public void AddSongsTest()
        {
            Song[] songs = SetupSimpleSongMock(4);
            Playlist playlist = SetupPlaylist(songs);

            Assert.AreEqual(4, playlist.Count());
            Assert.AreEqual(songs[0], playlist[0]);
            Assert.AreEqual(songs[1], playlist[1]);
            Assert.AreEqual(songs[2], playlist[2]);
            Assert.AreEqual(songs[3], playlist[3]);
        }

        [Test]
        public void CanPlayNextSong_CurrentSongIndexIsNull_ReturnsFalse()
        {
            Song[] songs = SetupSimpleSongMock(4);
            Playlist playlist = SetupPlaylist(songs);

            playlist.CurrentSongIndex = null;

            Assert.IsFalse(playlist.CanPlayNextSong);
        }

        [Test]
        public void CanPlayNextSong_CurrentSongIndexIsPlaylistCount_ReturnsFalse()
        {
            Song[] songs = SetupSimpleSongMock(4);
            Playlist playlist = SetupPlaylist(songs);

            playlist.CurrentSongIndex = playlist.Count();

            Assert.IsFalse(playlist.CanPlayNextSong);
        }

        [Test]
        public void CanPlayNextSong_CurrentSongIndexIsZero_ReturnsTrue()
        {
            Song[] songs = SetupSimpleSongMock(4);
            Playlist playlist = SetupPlaylist(songs);

            playlist.CurrentSongIndex = 0;

            Assert.IsTrue(playlist.CanPlayNextSong);
        }

        [Test]
        public void CanPlayPreviousSong_CurrentSongIndexIsNull_ReturnsFalse()
        {
            Song[] songs = SetupSimpleSongMock(4);
            Playlist playlist = SetupPlaylist(songs);

            playlist.CurrentSongIndex = null;

            Assert.IsFalse(playlist.CanPlayPreviousSong);
        }

        [Test]
        public void CanPlayPreviousSong_CurrentSongIndexIsPlaylistCount_ReturnsTrue()
        {
            Song[] songs = SetupSimpleSongMock(4);
            Playlist playlist = SetupPlaylist(songs);

            playlist.CurrentSongIndex = playlist.Count();

            Assert.IsTrue(playlist.CanPlayPreviousSong);
        }

        [Test]
        public void CanPlayPreviousSong_CurrentSongIndexIsZero_ReturnsFalse()
        {
            Song[] songs = SetupSimpleSongMock(4);
            Playlist playlist = SetupPlaylist(songs);

            playlist.CurrentSongIndex = 0;

            Assert.IsFalse(playlist.CanPlayPreviousSong);
        }

        [Test]
        public void InsertMove_InsertSongToPlaylist_OrderIsCorrent()
        {
            Song[] songs = SetupSimpleSongMock(5);

            Playlist playlist = SetupPlaylist(songs);

            playlist.InsertMove(3, 1);

            Assert.AreEqual(songs[0], playlist[0]);
            Assert.AreEqual(songs[3], playlist[1]);
            Assert.AreEqual(songs[1], playlist[2]);
            Assert.AreEqual(songs[2], playlist[3]);
        }

        [Test]
        public void RemoveSongs_RemoveMultipleSongs_OrderIsCorrect()
        {
            Song[] songs = SetupSimpleSongMock(7);
            Playlist playlist = SetupPlaylist(songs);

            playlist.RemoveSongs(new[] { 1, 3, 4 });

            Assert.AreEqual(4, playlist.Count());
            Assert.AreEqual(songs[0], playlist[0]);
            Assert.AreEqual(songs[2], playlist[1]);
            Assert.AreEqual(songs[5], playlist[2]);
            Assert.AreEqual(songs[6], playlist[3]);
        }

        [Test]
        public void RemoveSongs_RemoveOneSong_OrderIsCorrect()
        {
            Song[] songs = SetupSimpleSongMock(4);
            Playlist playlist = SetupPlaylist(songs);

            playlist.RemoveSongs(new[] { 1 });

            Assert.AreEqual(3, playlist.Count());
            Assert.AreEqual(songs[0], playlist[0]);
            Assert.AreEqual(songs[2], playlist[1]);
            Assert.AreEqual(songs[3], playlist[2]);
        }

        private static Playlist SetupPlaylist(IEnumerable<Song> songs)
        {
            var playlist = new Playlist("Test Playlist");

            playlist.AddSongs(songs);

            return playlist;
        }

        private static Song[] SetupSimpleSongMock(int count)
        {
            var songs = new Song[count];

            for (int i = 0; i < count; i++)
            {
                songs[i] = new Mock<Song>("Song" + i, AudioType.Mp3, TimeSpan.Zero).Object;
            }

            return songs;
        }
    }
}