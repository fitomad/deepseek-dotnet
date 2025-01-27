using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitomad.DeepSeek.Endpoints;
using Fitomad.DeepSeek.Endpoints.Chat;
using Fitomad.DeepSeek.Entities.Chat;

namespace Fitomad.DeepSeek.Tests;

public class ChatCompletionTests
{
    private string _apiKey;
    private IDeepSeekClient _client;

    public ChatCompletionTests()
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

    [Theory]
    [InlineData(ChatModelType.DeepSeekChat)]
    [InlineData(ChatModelType.DeepSeekReasoner)]
    public void Chat_Models(ChatModelType kind)
    {
        ChatRequest request = new ChatRequestBuilder()
            .WithModel(kind)
            .WithUserMessage("¿Cuál es la distancia de la Tierra al Sol?")
            .WithTemperatute(Temperature.Precise)
            .Build();
        
    }

    [Fact]
    public void Chat_NoModel()
    {
        Assert.Throws<DeepSeekException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithUserMessage("¿Cuál es la distancia de la Tierra al Sol?")
                .WithTemperatute(0.8)
                .Build();
        });
    }

    [Fact]
    public void Chat_NoMessage()
    {
        Assert.Throws<DeepSeekException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelType.DeepSeekChat)
                .WithTemperatute(0.8)
                .Build();
        });
        
    }

    [Fact]
    public void Chat_NoModelNoMessage()
    {
        Assert.Throws<DeepSeekException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithTemperatute(0.8)
                .Build();
        });
    }

    [Fact]
    public void Chat_SystemAndUserMessages()
    {
        ChatRequest request = new ChatRequestBuilder()
            .WithModel(ChatModelType.DeepSeekChat)
            .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
            .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
            .WithTemperatute(Temperature.Precise)
            .Build();
    }

    [Fact]
    public void Chat_TemperatureOutOfRange_Bottom()
    {
        Assert.Throws<DeepSeekException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelType.DeepSeekChat)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithTemperatute(-3.0)
                .Build();
        });
    }

    [Fact]
    public void Chat_TemperatureOutOfRange_Top()
    {
        Assert.Throws<DeepSeekException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelType.DeepSeekChat)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithTemperatute(3.0)
                .Build();
        });
    }

    [Fact]
    public void Chat_FrequencyOutOfRange_Bottom()
    {
        Assert.Throws<DeepSeekException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelType.DeepSeekChat)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithFrequencyPenalty(-3.0)
                .Build();
        });
    }

    [Fact]
    public void Chat_FrequencyOutOfRange_Top()
    {
        Assert.Throws<DeepSeekException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelType.DeepSeekChat)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithFrequencyPenalty(3.0)
                .Build();
        });
    }

    [Fact]
    public void Chat_PresenceOutOfRange_Bottom()
    {
        Assert.Throws<DeepSeekException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelType.DeepSeekChat)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithPresencePenalty(-3.0)
                .Build();
        });
    }

    [Fact]
    public void Chat_PresenceOutOfRange_Top()
    {
        Assert.Throws<DeepSeekException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelType.DeepSeekChat)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithPresencePenalty(3.0)
                .Build();
        });
    }

    [Fact]
    public async Task ChatRequest_Test()
    {
        Assert.NotNull(_client);

        ChatRequest request = new ChatRequestBuilder()
            .WithModel(ChatModelType.DeepSeekChat)
            .WithSystemMessage("Eres un profesor de alumnos de 10 años.")
            .WithUserMessage("Explícame qué es una estrella.")
            .WithTemperatute(Temperature.Precise)
            .WithReponseFormat(ChatResponseFormat.Text)
            .Build();

        var chatResponse = await _client.ChatCompletion.CreateChatAsync(request);
        Assert.NotNull(chatResponse);
        Assert.NotEmpty(chatResponse.ResponseId);
        Assert.NotEmpty(chatResponse.Choices);
    }
}