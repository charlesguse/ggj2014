﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace QuantumTrap.GameLogic.Managers
{
    public class LevelManager
    {
        public Level Level { get; set; }

        public LevelManager()
        {
            Level = new Level();
        }

        public void LoadContent(ContentManager content, PlayerManager player, ShadowManager shadow)
        {
            LevelImporter importer = new LevelImporter();
            this.Level = importer.Import("debug");
            Level.LoadContent(content);
            player.Player.Position = Level.PlayerStart;
            //shadow.Position = Level.PlayerStart;
        }

        public void Update(GameTime gameTime)
        {
            Level.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Level.Draw(gameTime, spriteBatch);
        }

        internal bool CanMoveTo(Position2 potentialPostion, PlayerColor playerColor)
        {
            return Level.CanMoveTo(potentialPostion, playerColor);
        }
    }
}