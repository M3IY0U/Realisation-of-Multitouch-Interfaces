using System;
using MediaControlGUI.Recognizer.Types;
using TUIO;

namespace MediaControlGUI.Recognizer
{
    public class Listener : TuioListener
    {
        private Path2D _currentPath;
        public event EventHandler<Path2D> GestureComplete;
        
        
        public Listener()
        {
            _currentPath = new Path2D();
        }


        public void addTuioCursor(TuioCursor tcur)
        {
            _currentPath.Add(new Point2D(tcur.X, tcur.Y));
            Console.WriteLine($@"[{tcur.TuioTime.TotalMilliseconds}] Cursor #{tcur.CursorID} added");
        }

        public void updateTuioCursor(TuioCursor tcur)
        {
            _currentPath.Add(new Point2D(tcur.X, tcur.Y));
            Console.WriteLine($@"[{tcur.TuioTime.TotalMilliseconds}] Cursor #{tcur.CursorID} updated");
        }

        public void removeTuioCursor(TuioCursor tcur)
        {
            Console.WriteLine($@"[{tcur.TuioTime.TotalMilliseconds}] Cursor #{tcur.CursorID} removed");
            OnGestureComplete(_currentPath);
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

        protected virtual void OnGestureComplete(Path2D completedPath)
        {
            GestureComplete?.Invoke(this, completedPath);
        }
    }
}