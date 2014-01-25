using Microsoft.Xna.Framework;
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

        public void Update(GameTime gameTime)
        {
            Level.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Level.Draw(gameTime, spriteBatch);
        }
    }
}