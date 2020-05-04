using System.Collections.Generic;
using OpenCvSharp;

namespace video_processing
{
    public class Touch
    {
        public int Id { get; }
        public Point2f Position { get; set; }
        public int Age { get; set; }
        public List<Point2f> Path = new List<Point2f>();
        
        public Touch(int id, Point2f position)
        {
            Id = id;
            Position = position;
            Age = 0;
            Program.FrameComplete += (s, a) => Age++;
        }

        public void MoveTouch(Point2f newPosition)
        {
            Path.Add(newPosition);
            Position = newPosition;
        }
    }
}