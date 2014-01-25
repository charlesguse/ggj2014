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
            TileMap = new Tile[20][];

            for (int x = 0; x < TileMap.Length; x++)
            {
                TileMap[x] = new Tile[20];
                for (int y = 0; y < TileMap[x].Length; y++)
                {
                    TileMap[x][y] = new Tile();
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
    }
}
