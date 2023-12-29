namespace GeminiChat.DotNet;

internal class GeminiResponse
{
    public IList<Candidate>? Candidates { get; set; }
    public Promptfeedback? PromptFeedback { get; set; }

}
internal class Promptfeedback
{
    public IList<Safetyrating>? SafetyRatings { get; set; }
}

internal class Safetyrating
{
    public string? Category { get; set; }
    public string? Probability { get; set; }
}

internal class Candidate
{
    public Content? Content { get; set; }
    public string? FinishReason { get; set; }
    public int Index { get; set; }
    public IList<Safetyrating>? SafetyRatings { get; set; }
}

internal class Safetyrating1
{
    public string? Category { get; set; }
    public string? Probability { get; set; }
}
