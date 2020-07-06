using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MediaControlGUI.Recognizer;
using MediaControlGUI.Recognizer.GeometricRecognizer;
using MediaControlGUI.Recognizer.Types;
using TUIO;

namespace MediaControlGUI
{
    public partial class MediaControlForm : Form
    {
        private GeometricRecognizer _recognizer;
        private string _pauseGesture, _prevGesture, _nextGesture;
        private bool _recordingMode;
        private List<string> _gestureList;
        private GestureTemplate _holdingTemplate;
        private List<GestureTemplate> _displayGestures;

        public MediaControlForm()
        {
            // used to stop errors in debug mode
            CheckForIllegalCrossThreadCalls = false;
            // initialize members and tuio functionality
            _displayGestures = new List<GestureTemplate>();
            _recordingMode = false;
            _recognizer = new GeometricRecognizer();
            _recognizer.Templates.ForEach(t => _displayGestures.Add(new GestureTemplate(t.Name, FixPath(t.Points))));
            _gestureList = _recognizer.Templates.Select(x => x.Name).ToList();

            var tuioClient = new TuioClient();
            var listener = new Listener();
            tuioClient.addTuioListener(listener);
            // subscribe to the gesturecomplete event (whenever a cursor is removed)
            // initialize winforms components
            InitializeComponent();
            UpdateComboBoxes();
            // event subscriptions
            listener.GestureComplete += GestureComplete;
            comboBoxPrev.SelectedIndexChanged += PrevComboBoxIndexChanged;
            comboBoxPause.SelectedIndexChanged += PauseComboBoxIndexChanged;
            comboBoxNext.SelectedIndexChanged += NextComboBoxIndexChanged;
            // arbitrary gestures to display
            comboBoxPrev.SelectedIndex = 5;
            comboBoxPause.SelectedIndex = 3;
            comboBoxNext.SelectedIndex = 2;
            // start receiving tuio messages
            tuioClient.connect();
        }

        private void PrevComboBoxIndexChanged(object sender, EventArgs e)
        {
            // set the gesture (name) used for going back to the previous track
            _prevGesture = (string) ((ComboBox) sender).SelectedItem;
            if (string.IsNullOrEmpty(_prevGesture))
                return;
            // update the example of how the gesture has to look like
            panelPrevPreview.BackgroundImage = PathToImage(panelPrevPreview.Width, panelPrevPreview.Height,
                _displayGestures.Find(t => t.Name == _prevGesture).Points, Color.Blue);
        }

        private void PauseComboBoxIndexChanged(object sender, EventArgs e)
        {
            // set the gesture (name) used for pausing media
            _pauseGesture = (string) ((ComboBox) sender).SelectedItem;
            if (string.IsNullOrEmpty(_pauseGesture))
                return;
            // update the example of how the gesture has to look like
            panelPausePreview.BackgroundImage =
                PathToImage(panelPausePreview.Width, panelPausePreview.Height,
                    _displayGestures.Find(t => t.Name == _pauseGesture).Points, Color.Blue);
        }

        private void NextComboBoxIndexChanged(object sender, EventArgs e)
        {
            // set the gesture (name) used skipping the current track
            _nextGesture = (string) ((ComboBox) sender).SelectedItem;
            if (string.IsNullOrEmpty(_nextGesture))
                return;
            // update the example of how the gesture has to look like
            panelNextPreview.BackgroundImage =
                PathToImage(panelNextPreview.Width, panelNextPreview.Height,
                    _displayGestures.Find(t => t.Name == _nextGesture).Points, Color.Blue);
        }

        private void GestureComplete(object sender, Path2D completedPath)
        {
            if (_recordingMode)
            {
                // use the panel for displaying what the user just drew
                UpdateGesturePanel(new RecognitionResult(@"New gesture", 0d), completedPath);
                // temporarily hold the gesture
                _holdingTemplate = new GestureTemplate(@"New gesture", new Path2D(completedPath));
                // let the user know to confirm their gesture
                buttonRecord.BackColor = Color.Orange;
                buttonRecord.Text = @"Confirm gesture name";
                buttonRecord.Enabled = true;
            }
            else
            {
                // find out what the user drew
                var result = _recognizer.Recognize(completedPath);
                // check for a match
                CheckForGestureMatch(result);
                // display what the user drew (+ confidence)
                UpdateGesturePanel(result, completedPath);
            }
        }

        private void CheckForGestureMatch(RecognitionResult result)
        {
            if (result.Name == _prevGesture)
                MediaControlGUI.Controls.Previous();
            if (result.Name == _pauseGesture)
                MediaControlGUI.Controls.Pause();
            if (result.Name == _nextGesture)
                MediaControlGUI.Controls.Next();
        }

        private void UpdateGesturePanel(RecognitionResult result, Path2D completedPath)
        {
            // create the image to display (using red in recording mode and green normally)
            var image = PathToImage(gestureDisplay.Width, gestureDisplay.Height, completedPath,
                _recordingMode ? Color.Firebrick : Color.ForestGreen);
            // update text and panel
            labelGestureText.Text = $@"{result.Name} ({Math.Round(result.Score, 2)})";
            gestureDisplay.BackgroundImage = image;
        }

        private void UpdateComboBoxes()
        {
            // fuck this method
            // store previously selected indices and update them each with a new context
            int c1 = comboBoxPrev.SelectedIndex, c2 = comboBoxPause.SelectedIndex, c3 = comboBoxNext.SelectedIndex;
            comboBoxPrev.DataSource = null;
            comboBoxPause.DataSource = null;
            comboBoxNext.DataSource = null;
            comboBoxPrev.BindingContext = new BindingContext();
            comboBoxPrev.DataSource = _gestureList;
            comboBoxPrev.SelectedIndex = c1;
            comboBoxPause.BindingContext = new BindingContext();
            comboBoxPause.DataSource = _gestureList;
            comboBoxPause.SelectedIndex = c2;
            comboBoxNext.BindingContext = new BindingContext();
            comboBoxNext.DataSource = _gestureList;
            comboBoxNext.SelectedIndex = c3;
        }

        private static Path2D FixPath(Path2D path)
        {
            // normalizes a path to 0 - 1
            var newPath = new Path2D();
            var minX = path.Min(p => p.X);
            var maxX = path.Max(p => p.X);
            var minY = path.Min(p => p.Y);
            var maxY = path.Max(p => p.Y);
            newPath.AddRange(path.Select(p => new Point2D((p.X - minX) / (maxX - minX), (p.Y - minY) / (maxY - minY))));
            return newPath;
        }

        private static Bitmap PathToImage(int width, int height, Path2D path, Color color)
        {
            // create image
            var image = new Bitmap(width, height);
            var graphics = Graphics.FromImage(image);
            // convert Point2D objects to PointFs which are needed to draw a curve
            var points = new List<PointF>();
            path.ForEach(p =>
                points.Add(new PointF(Convert.ToSingle(p.X * width), Convert.ToSingle(p.Y * height))));
            // draw on and return the image
            graphics.FillEllipse(new SolidBrush(color), points.First().X, points.First().Y, 10, 10);
            graphics.DrawCurve(new Pen(color, 3), points.ToArray());
            return image;
        }

        private void buttonRecord_Click(object sender, EventArgs e)
        {
            var button = (Button) sender;
            if (_recordingMode)
            {
                // handle new gesture
                if (_holdingTemplate != null)
                {
                    var name = textNewGestureName.Text;
                    _holdingTemplate.Name = name;
                    Console.WriteLine(_holdingTemplate.Name);
                    _gestureList.Add(name);
                    _displayGestures.Add(_holdingTemplate);
                    _recognizer.Templates.Add(new GestureTemplate(name,
                        new Path2D(_recognizer.NormalizePath(_holdingTemplate.Points))));
                    UpdateComboBoxes();
                }

                // reset controls
                button.Text = @"Record new gesture";
                textNewGestureName.Text = null;
                button.BackColor = DefaultBackColor;
                textNewGestureName.Enabled = false;
                _holdingTemplate = null;
                _recordingMode = false;
            }
            else
            {
                // let the user know that we're waiting for input
                button.Text = @"Waiting for gesture completion...";
                button.BackColor = Color.IndianRed;
                button.Enabled = false;
                textNewGestureName.Enabled = true;
                _recordingMode = true;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // minimize to tray
            if (WindowState != FormWindowState.Minimized) return;
            Hide();
            notifyIcon.Visible = true;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // restore from tray
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
    }
}