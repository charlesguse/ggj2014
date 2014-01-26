﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using QuantumTrap.GameLogic.Managers;

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
            Direction = player.Direction * -1;

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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_shadowTexture, DrawablePosition, Color.White);
        }
    }
}
