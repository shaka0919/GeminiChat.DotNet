using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeminiChat.DotNet;

internal class GeminiClient
{
    private readonly string _apiKey;
    private readonly string _model;
    private readonly HttpClient _client;
    private static string _apiBaseUrl = "generativelanguage.googleapis.com";

    public GeminiClient(string apiKey, string model, string? apiBaseUrl)
    {
        _apiKey = apiKey;
        _model = model;
        _client = new HttpClient();
        _apiBaseUrl = apiBaseUrl ?? _apiBaseUrl;
    }

    public async Task<string> Send(GeminiRequest request)
    {
        if (string.IsNullOrEmpty(_apiBaseUrl)) _apiBaseUrl = "generativelanguage.googleapis.com";
        var _apiUrl = $"https://{_apiBaseUrl}/v1beta/models/{_model}:generateContent?key={_apiKey}";
        var jsonOption = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        };
        var resp = await _client.PostAsJsonAsync<GeminiRequest>(_apiUrl, request, jsonOption);
        if (resp.IsSuccessStatusCode)
        {
            var result = await resp.Content.ReadFromJsonAsync<GeminiResponse>(jsonOption);
            return result?.Candidates?.First().Content?.Parts?.First().Text ?? throw new NullReferenceException();
        }
        var error = await resp.Content.ReadFromJsonAsync<GeminiError>(jsonOption)?? throw new NullReferenceException();
        return $"[Error] Code - {error.Error?.Code} : {error.Error?.Message}";
    }
}
