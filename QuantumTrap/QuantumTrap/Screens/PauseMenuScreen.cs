#region File Description
//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using QuantumTrap.GameLogic;

#endregion

namespace QuantumTrap.Screens
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class PauseMenuScreen : MenuScreen
    {
        private readonly string _levelFile;
        private readonly int _level;
        private readonly List<PlayerColor> _colorsAvailable;

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="currentLevel"></param>
        public PauseMenuScreen(string levelFile, int level, List<PlayerColor> colorsAvailable)
            : base("Paused")
        {
            _levelFile = levelFile;
            _level = level;
            _colorsAvailable = colorsAvailable;
            // Create our menu entries.
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
            MenuEntry restartLevelMenuEntry = new MenuEntry("Restart Level");
            MenuEntry quitMenuEntry = new MenuEntry("Quit To Menu");
            
            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += OnCancel;
            restartLevelMenuEntry.Selected += ConfirmRestartLevelMenuAccepted;
            quitMenuEntry.Selected += ConfirmQuitMessageBoxAccepted;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(restartLevelMenuEntry);
            MenuEntries.Add(quitMenuEntry);
        }

        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>

        private void ConfirmRestartLevelMenuAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                new GameplayScreen(_levelFile, _level, _colorsAvailable));
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }


        #endregion
    }
}
