using Microsoft.Xna.Framework;

namespace QuantumTrap.GameLogic
{
    public struct Position2
    {
        public static Position2 Zero
        {
            get { return new Position2 {X = 0, Y = 0}; }
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int Sum()
        {
            return X + Y;
        }

        public static Position2 operator +(Position2 position1, Position2 position2)
        {
            position1.X += position2.X;
            position1.Y += position2.Y;
            return position1;
        }

        public static Position2 operator -(Position2 position)
        {
            position.X = -position.X;
            position.Y = -position.Y;
            return position;
        }

        public static Position2 operator -(Position2 position1, Position2 position2)
        {
            return position1 + -position2;
        }

        public static Position2 operator *(Position2 position1, Position2 position2)
        {
            position1.X *= position2.X;
            position1.Y *= position2.Y;
            return position1;
        }

        public static Position2 operator *(Position2 position, int scalar)
        {
            position *= new Position2 {X = scalar, Y = scalar};
            return position;
        }

        public static implicit operator Vector2(Position2 position)  // explicit byte to digit conversion operator
        {
            var vector = new Vector2 {X = position.X, Y = position.Y};
            return vector;
        }
    }
}