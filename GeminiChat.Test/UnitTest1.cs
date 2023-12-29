using System.Diagnostics;
using GeminiChat.DotNet;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace GeminiChat.Test;

public class UnitTest1
{
    [Fact]
    public async Task Test1Async()
    {
        var gemini = new GeminiService("apikey");
        var input = "Write long a story about a magic backpack.";
        gemini.AppendMessage(input);
        var result = string.Empty;
        await gemini.StreamResponseAsync(str =>
        {
            Debug.WriteLine(str);
            result += str;
        });
        Assert.False(string.IsNullOrEmpty(result));
    }
}