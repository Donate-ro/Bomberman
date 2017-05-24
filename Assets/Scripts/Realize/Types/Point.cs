namespace Assets.Scripts
{
    class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Point a, Point b)
        {
            return ((a.X == b.X) && (a.Y == b.Y));
        }

        public static bool operator !=(Point a, Point b)
        {
            return ((a.X != b.X) || (a.Y != b.Y));
        }

        public override bool Equals(object obj)
        {
            if (obj is Point)
                return (this == (Point)obj);
            return false;
        }

        public override string ToString()
        {
            return "X +" + X + " Y= " + Y;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}