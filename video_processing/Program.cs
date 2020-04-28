using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;

namespace video_processing
{
    internal static class Program
    {
        private const int BlurHighPass = 15;
        private const int BlurSmoothing = 6;
        private const int Threshold = 10;

        private static void Main()
        {
            var capture = new VideoCapture("mt_camera_raw.AVI");
            var sleepTime = (int) Math.Round(1000 / capture.Fps);
            var blobList = new List<Blob>();
            var background = new Mat();
            var frameCounter = 0;

            using (var window = new Window("Video Processing"))
            {
                var original = new Mat();
                while (true)
                {
                    capture.Read(original);

                    if (frameCounter == 0)
                        background = original.Clone();

                    frameCounter++;

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

                    if (vectorList.Count > 0)
                    {
                        for (var idx = 0; idx >= 0; idx = vectorList[idx][0])
                        {
                            if (Cv2.ContourArea(contours[idx]) < 30 || contours[idx].Length <= 4) continue;
                            Cv2.Ellipse(original, Cv2.FitEllipse(contours[idx]), Scalar.Blue);
                            Cv2.DrawContours(original, contours, idx, Scalar.Red, hierarchy: hierarchy);
                            var moments = Cv2.Moments(contours[idx]);
                            var p = new Point((moments.M10 / moments.M00), (moments.M01 / moments.M00));
                            blobList.Add(new Blob(frameCounter, Cv2.ContourArea(contours[idx]), p));
                        }
                    }

                    window.ShowImage(original);
                    if (Cv2.WaitKey(sleepTime / 4) == 27)
                        break;
                }
            }

            Console.WriteLine($"{blobList.Count} entries in the list");
        }
    }
}