using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace QuantumTrap.GameLogic.Managers
{
    public class WinManager
    {
        private int _framesTogether;
        private DelayedSoundEffect _wonSfx;
        public bool GameWon { get; set; }

        public void LoadContent(ContentManager content)
        {
            _wonSfx = new DelayedSoundEffect(content, "sfx/Can't move there sound", 5000); ;
        }

        public void Update(GameTime gameTime,PlayerManager playerManager)
        {
            if (playerManager.Player.Position == playerManager.Shadow.Position)
                _framesTogether++;
            else
                _framesTogether = 0;

            if (_framesTogether >= 3)
            {
                GameWon = playerManager.Player.Position == playerManager.Shadow.Position;
                _wonSfx.Play(gameTime);
            }
        }
    }
}