namespace Ants
{
    public struct AntPoint
    {
        public float X;
        public float Y;
        public int Id;

        public AntPoint(int id, float x, float y)
        {
            X = x;
            Y = y;
            Id = id;
        }

        public double DistanceTo(AntPoint anotherPoint)
        {
            return Distance.Euclidean(X, Y, anotherPoint.X, anotherPoint.Y);
        }
    }
}
