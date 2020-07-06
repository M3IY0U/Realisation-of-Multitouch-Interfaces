using System;
using System.Linq;
using MediaControlGUI.Recognizer.Types;

namespace MediaControlGUI.Recognizer.GeometricRecognizer
{
    // UTILITY FUNCTIONS
    // COMMENTS FROM ORIGINAL C++ VERSION UNCHANGED
    public partial class GeometricRecognizer
    {
        public Path2D NormalizePath(Path2D points)
        {
            points = Resample(points);

            if (RotationInvariance)
                RotateToZero(ref points);

            ScaleToSquare(ref points);

            TranslateToOrigin(ref points);
            return points;
        }

        private void TranslateToOrigin(ref Path2D points)
        {
            var c = Centroid(points);
            var newPoints = new Path2D();
            foreach (var point in points)
            {
                double qx = point.X - c.X;
                double qy = point.Y - c.Y;
                newPoints.Add(new Point2D(qx, qy));
            }
            points = newPoints;
        }

        private void ScaleToSquare(ref Path2D points)
        {
            //--- Figure out the smallest box that can contain the path
            Rectangle box = BoundingBox(points);
            Path2D newPoints = new Path2D();
            foreach (var point in points)
            {
                //--- Scale the points to fit the main box
                //--- So if we wanted everything 100x100 and this was 50x50,
                //---  we'd multiply every point by 2
                double scaledX = point.X * (SquareSize / box.Width);
                double scaledY = point.Y * (SquareSize / box.Height);
                //--- Why are we adding them to a new list rather than 
                //---  just scaling them in-place?
                // TODO: try scaling in place (once you know this way works)
                newPoints.Add(new Point2D(scaledX, scaledY));
            }

            points = newPoints;
        }

        private Rectangle BoundingBox(Path2D points)
        {
            var minX = double.MaxValue;
            var maxX = -double.MaxValue;
            var minY = double.MaxValue;
            var maxY = -double.MaxValue;

            foreach (var point in points)
            {
                if (point.X < minX)
                    minX = point.X;
                if (point.X > maxX)
                    maxX = point.X;
                if (point.Y < minY)
                    minY = point.Y;
                if (point.Y > maxY)
                    maxY = point.Y;
            }
            return new Rectangle(minX, minY, (maxX - minX), (maxY - minY));
        }

        private void RotateToZero(ref Path2D points)
        {
            var c = Centroid(points);
            var rotation = Math.Atan2(c.Y - points[0].Y, c.X - points[0].X);
            points = RotateBy(points, -rotation);
        }

        private Path2D RotateBy(Path2D points, double rotation)
        {
            Point2D centroid = Centroid(points);
            var cos = Math.Cos(rotation);
            var sin = Math.Sin(rotation);

            var newPoints = new Path2D();

            foreach (var point in points)
            {
                var qX = (point.X - centroid.X) * cos - (point.Y - centroid.Y) * sin + centroid.X;
                var qY = (point.X - centroid.X) * sin + (point.Y - centroid.Y) * cos + centroid.Y;
                newPoints.Add(new Point2D(qX, qY));
            }

            return newPoints;
        }

        private Point2D Centroid(Path2D points)
        {
            double x = 0.0, y = 0.0;
            foreach (var point in points)
            {
                x += point.X;
                y += point.Y;
            }

            x /= points.Count;
            y /= points.Count;
            return new Point2D(x, y);
        }

        private Path2D Resample(Path2D points)
        {
            var interval = PathLength(points) / (NumPointsInGesture - 1); // interval length
            var d = 0.0;
            var newPoints = new Path2D {points.First()};

            //--- Store first point since we'll never resample it out of existence
            for (var i = 1; i < points.Count; i++)
            {
                var currentPoint = points[i];
                var previousPoint = points[i - 1];
                var distance = GetDistance(previousPoint, currentPoint);
                if ((d + distance) >= interval)
                {
                    var qX = previousPoint.X + ((interval - d) / distance) * (currentPoint.X - previousPoint.X);
                    var qY = previousPoint.Y + ((interval - d) / distance) * (currentPoint.Y - previousPoint.Y);
                    var point = new Point2D(qX, qY);
                    newPoints.Add(point);
                    points.Insert(i, point);
                    d = 0.0;
                }
                else d += distance;
            }

            // sometimes we fall a rounding-error short of adding the last point, so add it if so
            if (newPoints.Count == (NumPointsInGesture - 1))
            {
                newPoints.Add(points.Last());
            }

            return newPoints;
        }

        private double PathLength(Path2D points)
        {
            var distance = 0d;
            for (var i = 1; i < points.Count; i++)
            {
                distance += GetDistance(points[i - 1], points[i]);
            }

            return distance;
        }

        private double GetDistance(Point2D p1, Point2D p2)
        {
            var dX = p2.X - p1.X;
            var dY = p2.Y - p1.Y;
            var distance = Math.Sqrt((dX * dX) + (dY * dY));
            return distance;
        }

        private double DistanceAtBestAngle(Path2D points, GestureTemplate template)
        {
            double startRange = -AngleRange;
            double endRange   =  AngleRange;
            double x1 = GoldenRatio * startRange + (1.0 - GoldenRatio) * endRange;
            double f1 = DistanceAtAngle(points, template, x1);
            double x2 = (1.0 - GoldenRatio) * startRange + GoldenRatio * endRange;
            double f2 = DistanceAtAngle(points, template, x2);
            while (Math.Abs(endRange - startRange) > AnglePrecision)
            {
                if (f1 < f2)
                {
                    endRange = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = GoldenRatio * startRange + (1.0 - GoldenRatio) * endRange;
                    f1 = DistanceAtAngle(points, template, x1);
                }
                else
                {
                    startRange = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = (1.0 - GoldenRatio) * startRange + GoldenRatio * endRange;
                    f2 = DistanceAtAngle(points, template, x2);
                }
            }
            return Math.Min(f1, f2);
        }

        private double DistanceAtAngle(Path2D points, GestureTemplate template, double rotation)
        {
            var newPoints = RotateBy(points, rotation);
            return PathDistance(newPoints, template.Points);
        }

        private double PathDistance(Path2D pts1, Path2D pts2)
        {
            if (pts1.Count != pts2.Count)
                return double.MaxValue;
            var distance = pts1.Select((t, i) => GetDistance(t, pts2[i])).Sum();
            return (distance / pts1.Count);
        }
    }
}