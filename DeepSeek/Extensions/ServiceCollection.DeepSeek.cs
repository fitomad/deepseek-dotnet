using System.Net.Http.Headers;
using System.Reflection.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Fitomad.DeepSeek;

public static class ServiceColletionDeepSeek
{
    private static string DeepSeekApiKeyHeader = "Bearer";
    
    public static void AddDeepSeekHttpClient(this IServiceCollection services, DeepSeekSettings settings) 
    {
        services.AddHttpClient<IDeepSeekClient, DeepSeekClient>(client =>
        {
            var deepSeekBaseUri = "https://api.deepseek.com";
            client.BaseAddress = new Uri(deepSeekBaseUri);

            var jsonMediaType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(jsonMediaType);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(DeepSeekApiKeyHeader, settings.ApiKey);
        });
    }
}