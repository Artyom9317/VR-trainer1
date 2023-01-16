namespace assessment.mistake
{
    public class MistakeFurnace : IMistake
    {
        public MistakeFurnace(MistakeType type, string message, double weight)
        {
            this.type = type;
            this.message = message;
            this.weight = weight;

            title = StringConstants.FURNACE_MISTAKE_TITTLE;
        }
        
        public MistakeType type { get; }
        public string title { get; }
        public double weight { get; }
        public string message { get; }
    }
}