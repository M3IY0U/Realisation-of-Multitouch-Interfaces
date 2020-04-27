using OpenCvSharp;

namespace video_processing
{
    public class Blob
    {
        public int Id { get; set; }
        public double Area { get; set; }
        public Point Position { get; set; }

        public Blob(int id, double area, Point position)
        {
            Id = id;
            Area = area;
            Position = position;
        }
    }
}