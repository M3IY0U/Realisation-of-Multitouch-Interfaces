using System.Collections.Generic;
using System.Text;
using OSC.NET;
using video_processing.Processing;

namespace video_processing.Util
{
    public class TuioSender
    {
        private int _fseq;
        private OSCTransmitter _sender;
        private int _maxWidth, _maxHeight;

        public TuioSender(string host, int port, int maxWidth, int maxHeight)
        {
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _sender = new OSCTransmitter(host, port);
            _sender.Connect();
        }

        public void SendTouches(List<Touch> touches)
        {
            var bundle = new OSCBundle();
            AddAliveMessage(touches, ref bundle);
            AddSetMessages(touches, ref bundle);
            AddFSeqMessage(ref bundle);
            _sender.Send(bundle);
        }

        private static void AddAliveMessage(List<Touch> touches, ref OSCBundle bundle)
        {
            var msg = new OSCMessage("/tuio/2Dcur");
            msg.Append("alive");
            touches.ForEach(touch => msg.Append(touch.Id));
            bundle.Append(msg);
        }

        private void AddSetMessages(IEnumerable<Touch> touches, ref OSCBundle bundle)
        {
            foreach (var touch in touches)
            {
                var msg = new OSCMessage("/tuio/2Dcur");
                msg.Append("set");
                // session id
                msg.Append(touch.Id);
                // (normalized) position
                msg.Append(touch.Position.X / _maxWidth);
                msg.Append(touch.Position.Y / _maxHeight);
                // movement vector
                msg.Append(0.0f);
                msg.Append(0.0f);
                // motion acceleration
                msg.Append(0.0f);

                bundle.Append(msg);
            }
        }

        private void AddFSeqMessage(ref OSCBundle bundle)
        {
            var msg = new OSCMessage("/tuio/2Dcur");
            msg.Append("fseq");
            msg.Append(_fseq++);
            bundle.Append(msg);
        }
    }
}