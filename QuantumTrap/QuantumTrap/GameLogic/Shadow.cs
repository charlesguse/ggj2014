using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using QuantumTrap.GameLogic.Managers;
using QuantumTrap.ScreenManagers;

namespace QuantumTrap.GameLogic
{
    public class Shadow : PlayerBase
    {
        private Texture2D _shadowTexture;

        public void LoadContent(ContentManager content)
        {
            _shadowTexture = content.Load<Texture2D>("img/lepton");            
        }

        public void Update(GameTime gameTime, Player player, LevelManager levelManager)
        {
            if (player.CanMove)
            {
                var potentialPostion = Position + Direction;
                if (levelManager.CanMoveTo(potentialPostion, player.PlayerColor))
                {
                    if (DistanceLeftToTravel == 0 && Direction.Sum() != 0)
                    {
                        DistanceLeftToTravel = DistanceToTravel;
                    }
                    Move();
                }
                else
                {
                    Direction = Position2.Zero;
                }
            }
        }

        public void HandleInput(InputState input, PlayerIndex playerIndex)
        {
            if (DistanceLeftToTravel == 0)
            {
                Direction = GetMovementVector(input, playerIndex) * -1;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_shadowTexture, DrawablePosition, Color.White);
        }
    }
}
