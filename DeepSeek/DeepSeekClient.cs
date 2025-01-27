using Fitomad.DeepSeek.Endpoints.Chat;
using Fitomad.DeepSeek.Endpoints.Models;

namespace Fitomad.DeepSeek;

public interface IDeepSeekClient
{
    public ChatEndpoint ChatCompletion { get; }
    public ModelEndpoint Models { get; }
}

public class DeepSeekClient: IDeepSeekClient
{
    private HttpClient _httpClient;

    public ChatEndpoint ChatCompletion
    {
        get => new ChatEndpoint(_httpClient);
    }

    public ModelEndpoint Models
    {
        get => new ModelEndpoint(_httpClient);
    }

    public DeepSeekClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}