using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace QuantumTrap.GameLogic
{
    public enum Songs
    {
        Gameplay,
        Title
    }

    public static class StupidMusicPlayer
    {
        static readonly TimeSpan _gameplaySongLength = new TimeSpan(0, 1, 37);
        static readonly TimeSpan _titleSongLength = new TimeSpan(0, 0, 11);
        static private DateTime _songStarted = DateTime.MinValue;
        static private Song _gamePlaySong;
        static private Song _titleSong;
        private static Songs _songPlaying;


        public static void Load(ContentManager content)
        {
            _gamePlaySong = content.Load<Song>("music/Bozon Game Music 16 bit.wav");
            _titleSong = content.Load<Song>("music/Bozon Title 16 bit.wav");
        }

        private static void StartMusic(Songs song)
        {
            StopMusic();

            if (song == Songs.Gameplay)
                MediaPlayer.Play(_gamePlaySong);
            else if (song == Songs.Title)
                MediaPlayer.Play(_titleSong);
            _songStarted = DateTime.Now;
            _songPlaying = song;
        }

        private static void StopMusic()
        {
            MediaPlayer.Stop();
            _songStarted = DateTime.MinValue;
        }

        public static void LoopMusic(Songs song)
        {
            if (song != _songPlaying)
            {
                StopMusic();
            }

            if (DateTime.Now.Subtract(_songStarted) >= GetSongLength(song))
            {
                StartMusic(song);
                _songStarted = DateTime.Now;
            }
        }

        private static TimeSpan GetSongLength(Songs song)
        {
            return song == Songs.Gameplay ? _gameplaySongLength : _titleSongLength;
        }
    }
}
