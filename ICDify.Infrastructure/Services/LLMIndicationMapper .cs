using ICDify.Application.DTOs;
using ICDify.Application.Interfaces;
using ICDify.Infrastructure.ExternalModels.OpenAI;
using ICDify.Infrastructure.Mappers;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;

namespace ICDify.Infrastructure.Services;

public class LLMIndicationMapper : IIndicationMapper
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    private readonly MockIndicationMapper _mock;

    public LLMIndicationMapper(HttpClient http, IConfiguration config, MockIndicationMapper mock)
    {
        _http = http;
        _config = config;
        _mock = mock;
    }

    public async Task<List<IndicationDto>> ExtractAndMapAsync(string drugName, CancellationToken cancellationToken = default)
    {
        var prompt = $"""
        Extract the medical indications for the drug "{drugName}" and map them to ICD-10 codes. 
        Return a JSON array of objects with fields: condition, icd10Code, confidence, notes.
    """;

        using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/responses");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config["OpenAI:ApiKey"]);

        var body = new
        {
            model = "gpt-4o",
            input = prompt
        };
        request.Content = JsonContent.Create(body);

        try
        {
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                // Fallback to local mock mapper
                return await _mock.ExtractAndMapAsync(drugName, cancellationToken);
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>(cancellationToken);
            var json = result?.Output;

            return JsonSerializer.Deserialize<List<IndicationDto>>(json ?? "[]") ?? new();
        }
        catch (Exception ex)
        {
            // Optional: log and return safe fallback
            return new List<IndicationDto>();
        }
    }
}
