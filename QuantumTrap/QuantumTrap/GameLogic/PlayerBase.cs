using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumTrap.GameLogic
{
    public class PlayerBase : TileBase
    {
        public Position2 Direction { get; set; }
        public Position2 DrawablePosition { get; set; }
        public Position2 Position { get; set; }
        public int DistanceToTravel { get { return 64; } }
        public int DistanceLeftToTravel { get; set; }

        public void Move()
        {
            DrawablePosition += Direction;

            DistanceLeftToTravel -= 1;

            if (DistanceLeftToTravel <= 0)
            {
                DistanceLeftToTravel = 0;
                Position += Direction;
                DrawablePosition = ConvertToDrawablePosition(Position, TileSize);
                Direction = Position2.Zero;
            }
        }
    }
}
