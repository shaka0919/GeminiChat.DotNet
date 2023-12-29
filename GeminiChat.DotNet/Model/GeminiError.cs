namespace GeminiChat.DotNet;

internal class GeminiError
{
    public Error? Error { get; set; }

}

internal class Error
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public string? Status { get; set; }
}