using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;

namespace video_processing
{
    class Program
    {
        static void Main()
        {
            var capture = new VideoCapture("mt_camera_raw.AVI");
            var background = Cv2.ImRead("1.jpg");

            var blobList = new List<Blob>();
            var frameCounter = 0;
            
            var sleepTime = (int) Math.Round(1000 / capture.Fps);
            using (var window = new Window("Video Processing"))
            {
                var original = new Mat();
                while (true)
                {
                    capture.Read(original);
                    frameCounter++;

                    if (original.Empty())
                        break;

                    var result = new Mat();

                    var blurred = new Mat();

                    Cv2.Absdiff(original, background, result);
                    Cv2.Blur(result, blurred, new Size(15, 15));
                    Cv2.Absdiff(result, blurred, result);
                    Cv2.Blur(result, result, new Size(6, 6));
                    Cv2.CvtColor(result, result, ColorConversionCodes.BGR2GRAY);
                    Cv2.Threshold(result, result, 10, 255, ThresholdTypes.Binary);

                    // var erodeElement = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(6, 6));
                    // var dilateElement = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(10, 10));

                    Cv2.Erode(result, result, new Mat());
                    Cv2.Dilate(result, result, new Mat(), iterations: 3);

                    Cv2.FindContours(result, out var contours, out var hierarchy, RetrievalModes.CComp,
                        ContourApproximationModes.ApproxSimple);
                    
                    
                    var x = hierarchy.Select(hierarchyIndex => hierarchyIndex.ToVec4i()).ToList();

                    if (x.Count > 0)
                    {
                        for (var idx = 0; idx >= 0; idx = x[idx][0])
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
                    if (Cv2.WaitKey(sleepTime) == 27)
                        break;
                }
            }
            Console.WriteLine($"{blobList.Count} entries in the list");
        }
    }
}