using System.Collections.Generic;

namespace video_processing
{
    public class Tracker
    {
        private static List<Touch> _currentTouches = new List<Touch>();
        private int _idCounter;
        private const double MaxDistance = 35;

        public List<Touch> Track(List<Blob> blobs)
        {
            // are there more blobs in the new frame than in the old?
            var add = blobs.Count > _currentTouches.Count;

            // update/move all old touches
            for (var i = 0; i < _currentTouches.Count; i++)
            {
                UpdateTouch(_currentTouches[i], ref blobs);
            }

            // add the remaining blobs as new touches
            if (add)
                blobs.ForEach(b => _currentTouches.Add(new Touch(_idCounter++, b.Position)));

            return _currentTouches;
        }
        
        private static void UpdateTouch(Touch touch, ref List<Blob> blobs)
        {
            var closestBlob = new KeyValuePair<Blob, double>(null, double.PositiveInfinity);
            foreach (var blob in blobs)
            {
                var distance = touch.Position.DistanceTo(blob.Position);
                if (distance < MaxDistance && distance < closestBlob.Value)
                {
                    closestBlob = new KeyValuePair<Blob, double>(blob, distance);
                }
            }

            // if no close blob was found for the touch, remove it
            if (closestBlob.Key == null)
            {
                _currentTouches.Remove(touch);
                return;
            }

            // if a blob was found, move the touch to its position and remove it from the remaining blobs
            _currentTouches.Find(t => t.Id == touch.Id)?.MoveTouch(closestBlob.Key.Position);
            blobs.Remove(closestBlob.Key);
        }
    }
}