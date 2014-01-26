using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace QuantumTrap.GameLogic
{
    public class Level
    {
        public Tile[][] TileMap { get; set; }

        public Level()
        {
            var random = new Random();
            TileMap = new Tile[20][];

            for (int x = 0; x < TileMap.Length; x++)
            {
                TileMap[x] = new Tile[20];
                for (int y = 0; y < TileMap[x].Length; y++)
                {
                    TileMap[x][y] = new Tile(new Position2 { X = x, Y = y });
                    TileMap[x][y].TileType = (TileType)random.Next((int)TileType.Black);
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            for (int x = 0; x < TileMap.Length; x++)
            {
                for (int y = 0; y < TileMap[x].Length; y++)
                {
                    TileMap[x][y].LoadContent(content);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int x = 0; x < TileMap.Length; x++)
            {
                for (int y = 0; y < TileMap[x].Length; y++)
                {
                    TileMap[x][y].Update(gameTime);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < TileMap.Length; x++)
            {
                for (int y = 0; y < TileMap[x].Length; y++)
                {
                    TileMap[x][y].Draw(gameTime, spriteBatch);
                }
            }
        }

        public bool CanMoveTo(Position2 potentialPostion, PlayerColor playerColor)
        {
            int x = potentialPostion.X;
            int y = potentialPostion.Y;
            return IsPositionInBounds(TileMap, potentialPostion) && (TileMap[x][y].TileType == TileType.White
                    || TileAndPlayerColorMatch(TileMap[x][y].TileType, playerColor));
        }

        private static bool TileAndPlayerColorMatch(TileType tile, PlayerColor player)
        {
            return (int)tile == (int)player;
        }

        private static bool IsPositionInBounds(Tile[][] tileMap, Position2 position)
        {
            int x = position.X;
            int y = position.Y;

            return x >= 0 && y >= 0 && x <= tileMap.Length && y <= tileMap[0].Length;
        }
    }
}
