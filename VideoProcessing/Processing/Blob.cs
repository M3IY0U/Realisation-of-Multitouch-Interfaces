using OpenCvSharp;

namespace video_processing.Processing
{
    public class Blob
    {
        public Point2f Position { get; }
        public Blob(Point2f pos)
        {
            Position = pos;
        }
    }
}