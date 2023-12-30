using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace GeminiChat.DotNet;

internal class GeminiClient
{
    private readonly string _apiKey;
    private readonly string _model;
    private readonly HttpClient _client;
    private static string _apiBaseUrl = "generativelanguage.googleapis.com";
    private readonly JsonSerializerOptions _jsonOoption;

    public GeminiClient(string apiKey, string model, string? apiBaseUrl)
    {
        _apiKey = apiKey;
        _model = model;
        _client = new HttpClient();
        _apiBaseUrl = apiBaseUrl ?? _apiBaseUrl;
        _jsonOoption = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        };
    }

    public async Task<string> PostAsync(GeminiRequest request)
    {
        var _apiUrl = $"https://{_apiBaseUrl}/v1beta/models/{_model}:generateContent?key={_apiKey}";

        var resp = await _client.PostAsJsonAsync<GeminiRequest>(_apiUrl, request, _jsonOoption);

        if (resp.IsSuccessStatusCode)
        {
            var result = await resp.Content.ReadFromJsonAsync<GeminiResponse>(_jsonOoption);
            return result?.Candidates?.First().Content?.Parts?.First().Text ?? throw new NullReferenceException();
        }
        else
        {
            var error = await resp.Content.ReadFromJsonAsync<GeminiError>(_jsonOoption) ?? throw new NullReferenceException();
            throw new Exception($"[Error] Code - {error.Error?.Code} : {error.Error?.Message}");
        }
    }

    public async IAsyncEnumerable<string> StreamingRequest(GeminiRequest request)
    {
        var _apiUrl = $"https://{_apiBaseUrl}/v1beta/models/{_model}:streamGenerateContent?key={_apiKey}";

        var resp = await _client.PostAsJsonAsync<GeminiRequest>(_apiUrl, request, _jsonOoption);

        if (resp.IsSuccessStatusCode)
        {
            using var stream = await resp.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            var line = string.Empty;
            var pattern = @"""text""\s*:\s*""([^""]+)""";
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    yield return match.Groups[1].Value;
                }
            }
            
        }
        else
        {
            var errors = await resp.Content.ReadFromJsonAsync<GeminiError[]>(_jsonOoption) ?? throw new NullReferenceException();
            throw new Exception($"[Error] Code - {errors.First().Error?.Code} : {errors.First().Error?.Message}");
        }
    }
}
