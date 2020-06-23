using System;
using GestureRecognizer.Types;
using TUIO;

namespace GestureRecognizer
{
    public class Listener : TuioListener
    {
        private GeometricRecognizer.GeometricRecognizer _recognizer;
        private Path2D _currentPath;
        private Path2D _backupPath;

        public Listener()
        {
            _recognizer = new GeometricRecognizer.GeometricRecognizer();
            _currentPath = new Path2D();
            _backupPath = new Path2D();
        }
        
        
        public void addTuioCursor(TuioCursor tcur)
        {
            Console.WriteLine($"Cursor #{tcur.CursorID} added");
            _currentPath.Add(new Point2D(tcur.X, tcur.Y));
        }

        public void updateTuioCursor(TuioCursor tcur)
        {
            _currentPath.Add(new Point2D(tcur.X, tcur.Y));
            Console.WriteLine($"Cursor #{tcur.CursorID} updated");
        }

        public void removeTuioCursor(TuioCursor tcur)
        {
            Console.WriteLine($"Cursor #{tcur.CursorID} removed");
            
            var result = _recognizer.Recognize(_currentPath);
            Console.WriteLine($"{result.Name} was recognized with a {result.Score} score!");
            _currentPath.Clear();
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