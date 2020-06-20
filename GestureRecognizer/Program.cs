using System;

namespace GestureRecognizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var gr = new GeometricRecognizer.GeometricRecognizer();
            var gestures = new SampleGestures();
            var result = gr.Recognize(gestures.Caret());
            Console.WriteLine($"{result.Name} was recognized with a score of {result.Score}");
        }
    }
}