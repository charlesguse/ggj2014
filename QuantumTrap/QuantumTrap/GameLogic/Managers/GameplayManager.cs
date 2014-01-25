using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public void LoadContent(ContentManager content)
        {
            PlayerManager.LoadContent(content);
            LevelManager.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            PlayerManager.Update(gameTime, LevelManager);
            ShadowManager.Update(gameTime, LevelManager);
            LevelManager.Update(gameTime);
            WinManager.Update(PlayerManager, ShadowManager);
        }

        public void HandleInput(InputState input)
        {
            PlayerManager.HandleInput(input);
            ShadowManager.HandleInput(input);

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
