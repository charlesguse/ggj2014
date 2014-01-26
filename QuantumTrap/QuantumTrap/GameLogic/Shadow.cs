using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using QuantumTrap.GameLogic.Managers;

namespace QuantumTrap.GameLogic
{
    class Shadow : PlayerBase
    {
        private Texture2D _shadowTexture;

        public Shadow(Position2 startingLocation)
        {
            Position = startingLocation;
            DrawablePosition = ConvertToDrawablePosition(Position, TileSize);
        }

        public void LoadContent(ContentManager content)
        {
            _shadowTexture = content.Load<Texture2D>("img/lepton");            
        }

        public void Update(GameTime gameTime, Player player, LevelManager levelManager)
        {
            Direction = player.Direction * -1;

            var potentialPostion = Position + Direction;
            if (levelManager.CanMoveTo(potentialPostion, player.PlayerColor))
            {
                Move();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_shadowTexture, DrawablePosition, Color.White);
        }
    }
}
