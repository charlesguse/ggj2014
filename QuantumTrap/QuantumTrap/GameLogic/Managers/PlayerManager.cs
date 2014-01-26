using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using QuantumTrap.ScreenManagers;

namespace QuantumTrap.GameLogic.Managers
{
    public class PlayerManager
    {
        public Player Player { get; set; }

        public PlayerManager()
        {
            Player = new Player(Position2.Zero); 
        }

        public void LoadContent(ContentManager content)
        {
            Player.LoadContent(content);
        }

        public void Update(GameTime gameTime, LevelManager levelManager)
        {
            Player.Update(gameTime, levelManager);
        }

        public void HandleInput(InputState input, PlayerIndex playerIndex)
        {
            Player.HandleInput(input, playerIndex);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Player.Draw(gameTime, spriteBatch);
        }
    }
}