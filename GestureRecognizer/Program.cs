using System;
using System.Text;
using TUIO;

namespace GestureRecognizer
{
    class Program
    {
        static void Main()
        {
            var gr = new GeometricRecognizer.GeometricRecognizer();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var listener = new Listener();
            var client = new TuioClient();
            client.addTuioListener(listener);
            client.connect();
            
            /*
            var gestures = new SampleGestures();
            var result = gr.Recognize(gestures.Pigtail());
            Console.WriteLine($"{result.Name} was recognized with a score of {result.Score}");*/
        }
    }
}