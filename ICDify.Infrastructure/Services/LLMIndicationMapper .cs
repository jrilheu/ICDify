using ICDify.Application.DTOs;
using ICDify.Application.Interfaces;
using ICDify.Infrastructure.ExternalModels.OpenAI;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ICDify.Infrastructure.Services;

public class LLMIndicationMapper : IIndicationMapper
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public LLMIndicationMapper(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<List<IndicationDto>> ExtractAndMapAsync(string drugName, CancellationToken cancellationToken = default)
    {
        var prompt = $"""
        Extract the medical indications for the drug "{drugName}" and map them to ICD-10 codes. 
        Return a JSON array of objects with fields: condition, icd10Code, confidence, notes.
        """;

        using var request = new HttpRequestMessage(HttpMethod.Post, @"https://api.openai.com/v1/chat/completions");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config["OpenAI:ApiKey"]);

        var body = new
        {
            model = "gpt-4",
            messages = new[] { new { role = "user", content = prompt } },
            temperature = 0.2
        };

        request.Content = JsonContent.Create(body);

        using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>(cancellationToken: cancellationToken);
        var json = result?.Choices?.FirstOrDefault()?.Message.Content;

        return JsonSerializer.Deserialize<List<IndicationDto>>(json ?? "[]", new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new();
    }
}
