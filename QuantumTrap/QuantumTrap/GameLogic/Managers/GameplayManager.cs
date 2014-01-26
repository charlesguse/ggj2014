using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using QuantumTrap.ScreenManagers;

namespace QuantumTrap.GameLogic.Managers
{
    public class GameplayManager
    {
        public PlayerManager PlayerManager { get; set; }
        public LevelManager LevelManager { get; set; }
        public WinManager WinManager { get; set; }

        public GameplayManager()
        {
            PlayerManager = new PlayerManager();
            LevelManager = new LevelManager();
            WinManager = new WinManager();
        }

        public void LoadContent(ContentManager content)
        {
            LevelManager.LoadContent(content, PlayerManager, ShadowManager);
            PlayerManager.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            PlayerManager.Update(gameTime, LevelManager);
            LevelManager.Update(gameTime);
            WinManager.Update(PlayerManager);
        }

        public void HandleInput(InputState input, PlayerIndex playerIndex)
        {
            PlayerManager.HandleInput(input, playerIndex);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            LevelManager.Draw(gameTime, spriteBatch);
            PlayerManager.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
