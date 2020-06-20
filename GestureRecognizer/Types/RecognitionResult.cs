namespace GestureRecognizer.Types
{
    public class RecognitionResult
    {
        public string Name;
        public double Score;

        public RecognitionResult(string name, double score)
        {
            Name = name;
            Score = score;
        }
    }
}