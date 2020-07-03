using System;
using GestureRecognizer.Types;
using TUIO;

namespace GestureRecognizer
{
    public class Listener : TuioListener
    {
        private GeometricRecognizer.GeometricRecognizer _recognizer;
        private Path2D _currentPath;

        public Listener()
        {
            _recognizer = new GeometricRecognizer.GeometricRecognizer();
            _currentPath = new Path2D();
        }


        public void addTuioCursor(TuioCursor tcur)
        {
            _currentPath.Add(new Point2D(tcur.X, tcur.Y));
            Console.WriteLine($"[{tcur.TuioTime.TotalMilliseconds}] Cursor #{tcur.CursorID} added");
        }

        public void updateTuioCursor(TuioCursor tcur)
        {
            _currentPath.Add(new Point2D(tcur.X, tcur.Y));
            Console.WriteLine($"[{tcur.TuioTime.TotalMilliseconds}] Cursor #{tcur.CursorID} updated");
        }

        public void removeTuioCursor(TuioCursor tcur)
        {
            Console.WriteLine($"[{tcur.TuioTime.TotalMilliseconds}] Cursor #{tcur.CursorID} removed");

            var result = _recognizer.Recognize(_currentPath);
            Console.WriteLine($"{result.Name} was recognized with a {result.Score} score!");
            _currentPath.Clear();
            if(result.Name.ToLower() == "v")
                Program.keybd_event(0xB3, 0, 1, IntPtr.Zero);
        }

        public void addTuioObject(TuioObject tobj)
        {
            throw new NotImplementedException();
        }

        public void updateTuioObject(TuioObject tobj)
        {
            throw new NotImplementedException();
        }

        public void removeTuioObject(TuioObject tobj)
        {
            throw new NotImplementedException();
        }

        public void addTuioBlob(TuioBlob tblb)
        {
            throw new NotImplementedException();
        }

        public void updateTuioBlob(TuioBlob tblb)
        {
            throw new NotImplementedException();
        }

        public void removeTuioBlob(TuioBlob tblb)
        {
            throw new NotImplementedException();
        }

        public void refresh(TuioTime ftime)
        {
            //Console.WriteLine("Refreshed");
        }
    }
}