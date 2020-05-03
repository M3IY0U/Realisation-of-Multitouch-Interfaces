using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;

namespace video_processing
{
    internal static class Program
    {
        public static event EventHandler FrameComplete; // event used for aging all touches at once
        
        private const int BlurHighPass = 15;
        private const int BlurSmoothing = 6;
        private const int Threshold = 10;
        private static int _frameCounter;

        private static void Main()
        {
            var capture = new VideoCapture("mt_camera_raw.AVI");
            var sleepTime = (int) Math.Round(1000 / capture.Fps);
            var blobList = new List<Touch>();
            var background = new Mat();
            var tracker = new Tracker();

            using (var window = new Window("Video Processing"))
            {
                var original = new Mat();
                while (true)
                {
                    capture.Read(original);

                    if (_frameCounter == 0)
                        background = original.Clone();

                    if (original.Empty())
                        break;

                    var result = new Mat();
                    var blurred = new Mat();

                    Cv2.Absdiff(original, background, result);
                    Cv2.Blur(result, blurred, new Size(BlurHighPass, BlurHighPass));
                    Cv2.Absdiff(result, blurred, result);
                    Cv2.Blur(result, result, new Size(BlurSmoothing, BlurSmoothing));
                    Cv2.CvtColor(result, result, ColorConversionCodes.BGR2GRAY);
                    Cv2.Threshold(result, result, Threshold, 255, ThresholdTypes.Binary);

                    Cv2.FindContours(result, out var contours, out var hierarchy, RetrievalModes.CComp,
                        ContourApproximationModes.ApproxSimple);

                    var vectorList = hierarchy.Select(hierarchyIndex => hierarchyIndex.ToVec4i()).ToList();
                    var foundBlobs = new List<Blob>();
                    if (vectorList.Count > 0)
                    {
                        for (var idx = 0; idx >= 0; idx = vectorList[idx][0])
                        {
                            if (Cv2.ContourArea(contours[idx]) < 25 || contours[idx].Length <= 4) continue;
                            Cv2.Ellipse(original, Cv2.FitEllipse(contours[idx]), Scalar.Blue);
                            Cv2.DrawContours(original, contours, idx, Scalar.Red, hierarchy: hierarchy);
                            var moments = Cv2.Moments(contours[idx]);
                            var p = new Point2f((float) (moments.M10 / moments.M00),
                                (float) (moments.M01 / moments.M00));

                            foundBlobs.Add(new Blob(p));
                        }
                    }

                    blobList = tracker.Track(foundBlobs);

                    blobList.ForEach(b => Cv2.PutText(original, $"{b.Id}", (Point) b.Position,
                        HersheyFonts.HersheyPlain, 1, Scalar.White));

                    window.ShowImage(original);
                    OnFrameComplete();
                    if (Cv2.WaitKey(sleepTime / 4) == 27)
                        break;
                    if (_frameCounter > 48)
                        Cv2.WaitKey();
                }
            }

            Console.WriteLine($"{blobList.Count} entries in the list");
        }

        private static void OnFrameComplete()
        {
            _frameCounter++;
            FrameComplete?.Invoke(null, EventArgs.Empty);
        }
    }
}