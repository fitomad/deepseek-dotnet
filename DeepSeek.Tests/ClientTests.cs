using Fitomad.DeepSeek;

namespace Fitomad.DeepSeek.Tests;

public class ClientTests
{
    [Theory]
    [InlineData("API_TEST")]
    public void DeepSeekClient_WithApi(string apiKey)
    {
        var settings = new DeepSeekSettingsBuilder()
            .WithApiKey(apiKey)
            .Build();

        Assert.Equal(settings.ApiKey, apiKey);
    }

    [Fact]
    public void DeepSeekClient_NoSettings()
    {
        Assert.Throws<DeepSeekException>(() =>
        {
            var client = new DeepSeekSettingsBuilder()
                .Build();
        });
    }
}