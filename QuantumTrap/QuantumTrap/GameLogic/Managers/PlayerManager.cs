using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuantumTrap.ScreenManagers;

namespace QuantumTrap.GameLogic.Managers
{
    public class PlayerManager
    {
        Player Player { get; set; }

        public PlayerManager()
        {
            Player = new Player(); 
        }

        public void Update(GameTime gameTime, LevelManager levelManager)
        {
            Player.Update(gameTime, levelManager);
        }

        public void HandleInput(GameTime gameTime, InputState input)
        {
            Player.HandleInput(gameTime, input);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Player.Draw(gameTime, spriteBatch);
        }
    }
}