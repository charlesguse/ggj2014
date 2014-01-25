using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuantumTrap.ScreenManagers;

namespace QuantumTrap.GameLogic.Managers
{
    public class GameplayManager
    {
        public PlayerManager PlayerManager { get; set; }
        public ShadowManager ShadowManager { get; set; }
        public LevelManager LevelManager { get; set; }
        public WinManager WinManager { get; set; }

        public GameplayManager()
        {
            PlayerManager = new PlayerManager();
            ShadowManager = new ShadowManager();
            LevelManager = new LevelManager();
            WinManager = new WinManager();
        }


        public void Update(GameTime gameTime)
        {
            PlayerManager.Update(gameTime);
            ShadowManager.Update(gameTime);
            LevelManager.Update(gameTime);
            WinManager.Update(PlayerManager, ShadowManager);
        }

        public void HandleInput(GameTime gameTime, InputState input)
        {
            PlayerManager.HandleInput(gameTime, input, LevelManager);
            ShadowManager.HandleInput(gameTime, input, LevelManager);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            LevelManager.Draw(gameTime, spriteBatch);
            ShadowManager.Draw(gameTime, spriteBatch);
            PlayerManager.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
