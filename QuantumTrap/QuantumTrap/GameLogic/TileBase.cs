namespace QuantumTrap.GameLogic
{
    public class TileBase
    {
        public static Position2 TILE_SIZE { get { return new Position2 { X = 64, Y = 64 }; } }
        public Position2 TileSize { get { return TILE_SIZE; } }

        public static Position2 ConvertToDrawablePosition(Position2 position, Position2 tileSize)
        {
            var drawablePosition = position;
            drawablePosition.X *= TILE_SIZE.X;
            drawablePosition.Y *= TILE_SIZE.Y;

            return drawablePosition;
        }
    }
}