public interface IMistake
{
    MistakeType type { get; }
    string title { get; }
    double weight { get; }
    string message { get; }
}

public static class MistakeWeight
{
    public const double LIGHT = 0.1;
    public const double MEDIUM = 0.5;
    public const double HARD = 1;
}
