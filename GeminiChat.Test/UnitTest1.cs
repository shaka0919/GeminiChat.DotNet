using GeminiChat.DotNet;

namespace GeminiChat.Test;

public class UnitTest1
{
    [Fact]
    public async Task Test1Async()
    {
        var gemini = new GeminiService("INPUT YOUT API KEY");
        var input = "Hi!";
        gemini.AppendMessage(input);
        var result = await gemini.GetResponseAsync();
        Assert.False(string.IsNullOrEmpty(result));
    }
}