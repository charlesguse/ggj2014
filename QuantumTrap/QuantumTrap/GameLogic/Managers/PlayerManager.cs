using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using QuantumTrap.ScreenManagers;

namespace QuantumTrap.GameLogic.Managers
{
    public class PlayerManager
    {
        public Player Player { get; set; }
        public Shadow Shadow { get; set; }

        public PlayerManager()
        {
            Player = new Player(Position2.Zero); 
            Shadow = new Shadow(new Position2{X = 10, Y = 10});
        }

        public void LoadContent(ContentManager content)
        {
            Player.LoadContent(content);
            Shadow.LoadContent(content);
        }

        public void Update(GameTime gameTime, LevelManager levelManager)
        {
            Shadow.Update(gameTime, Player, levelManager);
            Player.Update(gameTime, levelManager);
        }

        public void HandleInput(InputState input, PlayerIndex playerIndex)
        {
            Player.HandleInput(input, playerIndex);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Shadow.Draw(gameTime, spriteBatch);
            Player.Draw(gameTime, spriteBatch);
        }
    }
}