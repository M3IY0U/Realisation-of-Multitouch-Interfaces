using MediaControlGUI.Recognizer.Types;

namespace MediaControlGUI.Recognizer
{
    public class GestureTemplate
    {
        public string Name;
        public Path2D Points;

        public GestureTemplate(string name, Path2D points)
        {
            Name = name;
            Points = points;
        }
    }
}