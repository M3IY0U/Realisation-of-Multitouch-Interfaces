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
    public partial class Form1 : Form
    {
        private GeometricRecognizer _recognizer;
        private string _pauseGesture, _prevGesture, _nextGesture;

        public Form1()
        {
            var tuioClient = new TuioClient();
            var listener = new Listener();
            tuioClient.addTuioListener(listener);
            listener.GestureComplete += GestureComplete;
            InitializeComponent();
            labelGestureText.Text = @"moino";
            _recognizer = new GeometricRecognizer();

            var list = _recognizer.Templates.Select(x => x.Name).ToList();
            comboBoxPrev.DataSource = list;
            comboBoxPrev.SelectedIndexChanged += PrevComboBoxIndexChanged;
            comboBoxPrev.SelectedIndex = 5;
            comboBoxPause.BindingContext = new BindingContext();
            comboBoxPause.DataSource = list;
            comboBoxPause.SelectedIndexChanged += PauseComboBoxIndexChanged;
            comboBoxPause.SelectedIndex = 3;
            comboBoxNext.BindingContext = new BindingContext();
            comboBoxNext.DataSource = list;
            comboBoxNext.SelectedIndexChanged += NextComboBoxIndexChanged;
            comboBoxNext.SelectedIndex = 2;

            tuioClient.connect();
        }

        private void PrevComboBoxIndexChanged(object sender, EventArgs e)
        {
            _prevGesture = (string) ((ComboBox) sender).SelectedItem;
            var path = FixPath(_recognizer.Templates.Find(t => t.Name == _prevGesture).Points);
            panelPrevPreview.BackgroundImage = PathToImage(panelPrevPreview.Width, panelPrevPreview.Height,
                path, Color.Blue);
        }

        private void PauseComboBoxIndexChanged(object sender, EventArgs e)
        {
            _pauseGesture = (string) ((ComboBox) sender).SelectedItem;
            var path = FixPath(_recognizer.Templates.Find(t => t.Name == _pauseGesture).Points);
            panelPausePreview.BackgroundImage =
                PathToImage(panelPausePreview.Width, panelPausePreview.Height, path, Color.Blue);
        }

        private void NextComboBoxIndexChanged(object sender, EventArgs e)
        {
            _nextGesture = (string) ((ComboBox) sender).SelectedItem;
            var path = FixPath(_recognizer.Templates.Find(t => t.Name == _nextGesture).Points);
            panelNextPreview.BackgroundImage =
                PathToImage(panelNextPreview.Width, panelNextPreview.Height, path, Color.Blue);
        }

        private void GestureComplete(object sender, Path2D completedPath)
        {
            var result = _recognizer.Recognize(completedPath);

            CheckForGestureMatch(result);
            UpdateGesturePanel(result, completedPath);
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
            var image = PathToImage(gestureDisplay.Width, gestureDisplay.Height, completedPath, Color.Firebrick);
            labelGestureText.Text = $@"{result.Name} ({Math.Round(result.Score, 2)})";
            gestureDisplay.BackgroundImage = image;
        }

        private static Path2D FixPath(Path2D path)
        {
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
            var image = new Bitmap(width, height);
            var graphics = Graphics.FromImage(image);
            var points = new List<PointF>();
            path.ForEach(p =>
                points.Add(new PointF(Convert.ToSingle(p.X * width), Convert.ToSingle(p.Y * height))));
            graphics.FillEllipse(new SolidBrush(color), points.First().X, points.First().Y, 10, 10);
            graphics.DrawCurve(new Pen(color, 3), points.ToArray());
            return image;
        }
    }
}