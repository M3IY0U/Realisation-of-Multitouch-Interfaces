using System.Collections.Generic;

namespace MediaControlGUI.Recognizer.Types
{
    public class Path2D : List<Point2D>
    {
        public Path2D(){}
        public Path2D(Path2D other) // copy constructor
        {
            other.ForEach(p => Add(new Point2D(p.X, p.Y)));
        }
    }
}