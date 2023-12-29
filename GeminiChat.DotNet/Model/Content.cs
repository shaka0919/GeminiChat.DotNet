namespace GeminiChat.DotNet;

internal class Content
{
    public MessageRole Role { get; set; }
    public IList<Part>? Parts { get; set; }
}

internal class Part
{
    public string? Text { get; set; }
}