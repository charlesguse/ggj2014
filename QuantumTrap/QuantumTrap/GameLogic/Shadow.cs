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

        public Shadow(Position2 startingLocation)
        {
            Position = startingLocation;
            SetDrawablePosition(Position);
        }

        public void LoadContent(ContentManager content)
        {
            _shadowTexture = content.Load<Texture2D>("img/lepton");            
        }

        public void Update(GameTime gameTime, Player player, LevelManager levelManager)
        {
            if (player.CanMove)
            {
                if (DistanceLeftToTravel > 0)
                {
                    var potentialPostion = Position + Direction;
                    if (levelManager.CanMoveTo(potentialPostion, player.PlayerColor))
                    {
                        Move();
                    }
                    else
                    {
                        Direction = Position2.Zero;
                    }
                }
                else if (DistanceLeftToTravel == 0 && Direction.Sum() != 0)
                {
                    var potentialPostion = Position + Direction;
                    if (levelManager.CanMoveTo(potentialPostion, player.PlayerColor))
                    {
                        DistanceLeftToTravel = DistanceToTravel;
                        Move();
                    }
                    else
                    {
                        Direction = Position2.Zero;
                    }
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
