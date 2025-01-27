using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using Fitomad.DeepSeek.Entities.Models;

namespace Fitomad.DeepSeek.Endpoints.Models;

public interface IModelEndpoint
{
    public Task<ModelListResponse> List();
}

public class ModelEndpoint: IModelEndpoint
{
    private HttpClient _httpClient;

    internal ModelEndpoint(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ModelListResponse> List()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(Endpoint.List);
        var modelListResponse = await response.Content.ReadFromJsonAsync<ModelListResponse>();

        return modelListResponse;
    }

    private static class Endpoint
    {
        internal const string List = "models";
    }
}