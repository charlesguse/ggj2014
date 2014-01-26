#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using QuantumTrap.GameLogic;
using QuantumTrap.GameLogic.Managers;
using QuantumTrap.ScreenManagers;
#endregion

namespace QuantumTrap.Screens
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        private readonly string _levelFile;
        private readonly int _currentLevel;
        private readonly List<PlayerColor> _colorsAvailable;
        private Song _music; 

        #region Fields
        ContentManager _content;

        float _pauseAlpha;
        private readonly GameplayManager _gameplayManager;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen(string levelFile, int currentLevel, List<PlayerColor> colorsAvailable)
        {
            _levelFile = levelFile;
            _currentLevel = currentLevel;
            _colorsAvailable = colorsAvailable;
            _gameplayManager = new GameplayManager(_levelFile, _colorsAvailable);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameplayManager.LoadContent(_content);
            //_music = _content.Load<Song>("music/Bozon Game Music"); 
            ScreenManager.Game.ResetElapsedTime();

            //if (MediaPlayer.Queue.ActiveSong != _music)
            //{
            //    MediaPlayer.IsRepeating = true;
            //    MediaPlayer.Play(_music);
            //}
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            _content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                _gameplayManager.Update(gameTime);

                if (_gameplayManager.WinManager.GameWon)
                {
                    LoadingScreen.Load(ScreenManager, true, null,
                               new StoryScreen(_currentLevel, true));
                }
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = ControllingPlayer != null ? (int)ControllingPlayer.Value : 0;

            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(_levelFile, _currentLevel, _colorsAvailable), ControllingPlayer);
            }
            else
            {
                _gameplayManager.HandleInput(input, (PlayerIndex)playerIndex);
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            //ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
            //                                   Color.CornflowerBlue, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            _gameplayManager.Draw(gameTime, spriteBatch);

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }


        #endregion
    }
}
