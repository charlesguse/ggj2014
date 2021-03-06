#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using QuantumTrap.GameLogic;

#endregion

namespace QuantumTrap.Screens
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class StoryScreen : MenuScreen
    {
        // Configurable
        private readonly string[] _levelFiles = { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5" };
        private readonly string[] _storyBackgroundsFiles = { "img/story-level-1", "img/story-level-2", "img/story-level-3", "img/story-level-4", "img/story-level-5", "img/story-level-end" };

        private List<PlayerColor> GetColorsAvailable(int currentLevel)
        {
            var colorsAvailable = new List<PlayerColor> { PlayerColor.Grey };

            if (currentLevel >= 1)
            {
                colorsAvailable.Add(PlayerColor.Blue);
            }
            if (currentLevel >= 2)
            {
                colorsAvailable.Add(PlayerColor.Red);   
            }
            if (currentLevel >= 3)
            {
                colorsAvailable.Add(PlayerColor.Yellow);
                colorsAvailable.Add(PlayerColor.Green);
            }
            return colorsAvailable;
        }
        // End Configurable
        
        #region Initialization
        private readonly int _maxLevel;
        public int CurrentLevel { get; private set; }

        public int PreviousLevel
        {
            get { return CurrentLevel == 0 ? CurrentLevel : CurrentLevel - 1; }
        }
        private Texture2D[] _storyBackgroundsTextures;
        private ContentManager _content;
        public string CurrentLevelFile { get { return _levelFiles[CurrentLevel]; } }
        public string PreviousLevelFile { get { return _levelFiles[PreviousLevel]; } }

        public Texture2D CurrentLevelTexture { get { return _storyBackgroundsTextures[CurrentLevel]; } }

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public StoryScreen(int currentLevel, bool nextLevel)
            : base("Story")
        {
            if (_levelFiles.Length + 1 != _storyBackgroundsFiles.Length)
            {
                throw new ArgumentOutOfRangeException("There should be one less level than story backgrounds.");
            }

            CurrentLevel = currentLevel;
            _maxLevel = _levelFiles.Length;

            if (nextLevel)
                NextLevel();

            // Create our menu entries.
            UpdateMenuEntries();
        }

        public override void LoadContent()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _storyBackgroundsTextures = new Texture2D[_storyBackgroundsFiles.Length];
            for (int i = 0; i < _storyBackgroundsTextures.Length; i++)
            {
                _storyBackgroundsTextures[i] = _content.Load<Texture2D>(_storyBackgroundsFiles[i]);
            }
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            _content.Unload();
        }

        private void UpdateMenuEntries()
        {
            MenuEntry startLevelMenuEntry = new MenuEntry("Start Level");
            MenuEntry retryLevelMenuEntry = new MenuEntry("Retry Last Level");
            MenuEntry backToMenuEntry = new MenuEntry("Back to Main Menu");
            // Hook up menu event handlers.
            startLevelMenuEntry.Selected += StartLevelEntrySelected;
            retryLevelMenuEntry.Selected += RetryLevelEntrySelected;
            backToMenuEntry.Selected += BackToMenuEntrySelected;

            // Add entries to the menu.
            if (CurrentLevel < _maxLevel)
            {
                MenuEntries.Add(startLevelMenuEntry);
            }
            if (CurrentLevel > 0)
            {
                MenuEntries.Add(retryLevelMenuEntry);
            }
            MenuEntries.Add(backToMenuEntry);
        }
        #endregion

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            StupidMusicPlayer.LoopMusic(Songs.Gameplay);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        #region Handle Input

        public void NextLevel()
        {
            if (CurrentLevel + 1 <= _maxLevel)
            {
                CurrentLevel++;
            }
        }

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void StartLevelEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadLevel(e, CurrentLevelFile, CurrentLevel);
        }

        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void RetryLevelEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadLevel(e, PreviousLevelFile,PreviousLevel);
        }

        private void BackToMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
            ScreenManager.AddScreen(new BackgroundScreen(), null);
            ScreenManager.AddScreen(new MainMenuScreen(), null);
        }

        private void LoadLevel(PlayerIndexEventArgs e, string levelFile, int level)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                new GameplayScreen(levelFile, level, GetColorsAvailable(level)));
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(CurrentLevelTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}
