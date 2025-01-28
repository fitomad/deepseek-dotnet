using System.Net;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using Fitomad.DeepSeek.Entities.Chat;

namespace Fitomad.DeepSeek.Endpoints.Chat;

public interface IChatEndpoint
{
    public Task<ChatResponse> CreateChatAsync(ChatRequest chatRequest);
}

public sealed class ChatEndpoint: Endpoint, IChatEndpoint
{
    private HttpClient _httpClient;

    internal ChatEndpoint(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ChatResponse> CreateChatAsync(ChatRequest chatRequest)
    {
        var payload = JsonSerializer.Serialize(chatRequest);
        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Create, httpContent);
        var chatResponse = await response.Content.ReadFromJsonAsync<ChatResponse>();

        if(response.StatusCode != HttpStatusCode.OK)
        {
            var responseFailure = ProcessHttpStatus(responseStatus: response.StatusCode);
            throw new DeepSeekException(message: "", failure: responseFailure);
        }

        return chatResponse;
    }

    private static class Endpoint
    {
        internal const string Create = "chat/completions";
    }
}