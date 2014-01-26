using System.Collections.Generic;
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

        public PlayerManager(List<PlayerColor> colorsAvailable)
        {
            Player = new Player(colorsAvailable); 
            Shadow = new Shadow();
        }

        public void LoadContent(ContentManager content)
        {
            Player.LoadContent(content);
            Shadow.LoadContent(content);
        }

        public void Update(GameTime gameTime, LevelManager levelManager)
        {
            Player.Update(gameTime, levelManager);
            Shadow.Update(gameTime, Player, levelManager);
        }

        public void HandleInput(InputState input, PlayerIndex playerIndex)
        {
            Player.HandleInput(input, playerIndex);
            Shadow.HandleInput(input, playerIndex);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Shadow.Draw(gameTime, spriteBatch);
            Player.Draw(gameTime, spriteBatch);
        }
    }
}