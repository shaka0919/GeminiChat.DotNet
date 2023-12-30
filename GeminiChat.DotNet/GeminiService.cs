using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeminiChat.DotNet;

public class GeminiService
{
    private readonly GeminiClient _client;
    private readonly GeminiRequest _conversation;

    public GeminiService(string apiKey, string model = "gemini-pro", string? apiBaseUrl = null)
    {
        _client = new GeminiClient(apiKey, model, apiBaseUrl);
        _conversation = new()
        {
            Contents = new List<Content>()
        };
    }

    public async Task<string> GetResponseAsync()
    {
        var result = await _client.PostAsync(_conversation);
        AppendMessage(result, MessageRole.Model);
        return result;
    }

    public async Task StreamResponseAsync(Action<string> resultHandler)
    {
        var result = new StringBuilder();
        await foreach (var data in _client.StreamingRequest(_conversation))
        {
            result.Append(data);
            resultHandler(data);
        }
        AppendMessage(result.ToString());
    }

    public void AppendMessage(string text, MessageRole role = MessageRole.User)
    {
        _conversation.Contents?.Add(new Content
        {
            Role = role,
            Parts = new List<Part>() { new() { Text = text } }
        });
    }

    public void ClearHistory()
    {
        _conversation.Contents?.Clear();
    }
}




