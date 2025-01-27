using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitomad.DeepSeek.Endpoints;
using System.ComponentModel;
using Fitomad.DeepSeek;
using Fitomad.DeepSeek.Entities.Models;
using Fitomad.DeepSeek.Endpoints.Models;

namespace Fitomad.DeepSeek.Tests;

public class ModelTests
{
    private string _apiKey;
    private IDeepSeekClient _client;

    public ModelTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<ChatCompletionTests>()
            .Build();

        _apiKey = configuration.GetValue<string>("DeepSeek:ApiKey");

        var testSettings = new DeepSeekSettingsBuilder()
            .WithApiKey(_apiKey!)
            .Build();

        var services = new ServiceCollection();
        services.AddDeepSeekHttpClient(settings: testSettings);
        var provider = services.BuildServiceProvider();
        
        _client = provider.GetRequiredService<IDeepSeekClient>();
    }

    [Fact]
    public async Task Models_TestList()
    {
        Assert.NotNull(_client);

        ModelListResponse response = await _client.Models.List();
        
        Assert.NotNull(response);
        Assert.NotEmpty(response.Results);
        
        foreach(var model in response.Results)
        {
            Assert.NotEmpty(model.ModelId);
            Assert.NotEmpty(model.Owner);
            Assert.NotEmpty(model.ObjectType);
            Assert.NotEqual(0, model.CreatedDate);
        }
    }
}