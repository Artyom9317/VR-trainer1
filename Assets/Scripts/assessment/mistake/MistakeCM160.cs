namespace assessment.mistake
{
    public class MistakeCM160 : IMistake
    {
        public MistakeCM160(MistakeType type, string message, double weight)
        {
            this.weight = weight;
            this.message = message;

            this.title = StringConstants.CM160_MISTAKE_TITLE;
        }

        public MistakeType type { get; }
        public string title { get; }
        public double weight { get; }
        public string message { get; }
    }
}