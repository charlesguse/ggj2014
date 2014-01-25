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

        public static Position2 operator -(Position2 position1)
        {
            position1.X = -position1.X;
            position1.Y = -position1.Y;
            return position1;
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
    }
}