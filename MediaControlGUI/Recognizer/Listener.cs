using System;
using MediaControlGUI.Recognizer.Types;
using TUIO;

namespace MediaControlGUI.Recognizer
{
    public sealed class Listener : TuioListener
    {
        private readonly Path2D _currentPath;
        private int _cursorCount;
        private int _frameCount;
        public event EventHandler<Path2D> GestureComplete;

        public Listener()
        {
            _cursorCount = 0;
            _currentPath = new Path2D();
        }
        
        public void addTuioCursor(TuioCursor tcur)
        {
            _cursorCount++;
            _currentPath.Add(new Point2D(tcur.X, tcur.Y));
            Console.WriteLine($@"[{tcur.TuioTime.TotalMilliseconds}] Cursor #{tcur.CursorID} added");
        }

        public void updateTuioCursor(TuioCursor tcur)
        {
            _frameCount++; // frames are counted so the instruction is only executed every 10 frames to make changing the volume smoother for the user
            if (_cursorCount > 1 && _cursorCount < 3 && _frameCount % 10 == 0) // two fingers for continuous gesture volume control 
            {
                if(tcur.YSpeed < 0)
                    Controls.VolumeUp();
                else if(tcur.YSpeed > 0)
                    Controls.VolumeDown();
            }
            _currentPath.Add(new Point2D(tcur.X, tcur.Y));
            Console.WriteLine($@"[{tcur.TuioTime.TotalMilliseconds}] Cursor #{tcur.CursorID} updated");
        }

        public void removeTuioCursor(TuioCursor tcur)
        {
            _frameCount = 0;
            if (_cursorCount > 1) // for 2 finger (continuous) gestures
            {
                _currentPath.Clear();
                _cursorCount--;
                return;   
            }           
            _cursorCount--;
            Console.WriteLine($@"[{tcur.TuioTime.TotalMilliseconds}] Cursor #{tcur.CursorID} removed");
            if(_currentPath.Count > 1)
                OnGestureComplete(_currentPath);
            _currentPath.Clear();
        }
        
        // objects and blobs don't need to be implemented as our input only uses cursors
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

        private void OnGestureComplete(Path2D completedPath)
        {
            GestureComplete?.Invoke(this, completedPath);
        }
    }
}