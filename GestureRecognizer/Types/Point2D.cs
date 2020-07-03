namespace GestureRecognizer.Types
{
    public class Point2D
    {
        public double X, Y;

        public Point2D()
        {
            X = 0;
            Y = 0;
        }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X} | {Y})";
        }
    }
}