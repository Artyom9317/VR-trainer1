namespace assessment.mistake
{
    public class MistakeGranulator : IMistake
    {
        public MistakeGranulator(MistakeType type, string message, double weight)
        {
            this.type = type;
            this.message = message;
            this.weight = weight;

            title = "Формование";
        }
        
        public MistakeType type { get; }
        public string title { get; }
        public double weight { get; }
        public string message { get; }
    }
}