﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace QuantumTrap.GameLogic
{
    public class Level
    {
        public Tile[,] TileMap { get; set; }

        public Level()
        {
            TileMap = new Tile[20, 20];
        }

        public void Update(GameTime gameTime)
        {
            foreach (var tile in TileMap)
            {
                tile.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var tile in TileMap)
            {
                tile.Draw(gameTime, spriteBatch);
            }
        }

        public void LoadContent(ContentManager content)
        {
            foreach (var tile in TileMap)
            {
                tile.LoadContent(content);
            }
        }
    }
}
