using System.Net;
using Fitomad.DeepSeek;
using Fitomad.DeepSeek.Endpoints;

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

    [Theory]
    [InlineData(HttpStatusCode.BadRequest, DeepSeekException.Failure.InvalidFormat)]
    [InlineData(HttpStatusCode.Unauthorized, DeepSeekException.Failure.AuthenticationFails)]
    [InlineData(HttpStatusCode.PaymentRequired, DeepSeekException.Failure.InsufficientBalance)]
    [InlineData(HttpStatusCode.UnprocessableContent, DeepSeekException.Failure.InvalidParameters)]
    [InlineData(HttpStatusCode.TooManyRequests, DeepSeekException.Failure.RateLimitExceeded)]
    [InlineData(HttpStatusCode.InternalServerError, DeepSeekException.Failure.ServerError)]
    [InlineData(HttpStatusCode.ServiceUnavailable, DeepSeekException.Failure.ServerOverloaded)]
    [InlineData(HttpStatusCode.GatewayTimeout, DeepSeekException.Failure.Unknown)]
    public void DeepLinkException_FailureTest(HttpStatusCode httpStatusCode, DeepSeekException.Failure deepSeekFailure)
    {
        var endpoint = new Endpoint();
        DeepSeekException.Failure associatedFailure = endpoint.ProcessHttpStatus(httpStatusCode);
        Assert.Equal(deepSeekFailure, associatedFailure);
    }
}