# DeepSeek API Client for .NET 9


## DeepSeek API key storage recommendations

API key is a sensitive information part that must be keep safe during your development and deployment process.

I strongly recommend **the usage of environment variables** when you deploy your solution to store your DeepSeek API key.

During the development stage you could use user-secrets technology to store the API key.

### User secrets

This is the recommended storage system for development. For a detailed information about the usage of this storage system, please refer to [Safe storage of app secrets in development in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=linux) article.

```cs
var configuration = new ConfigurationBuilder()
    .AddUserSecrets<ImageTests >()
    .Build();

_apiKey = configuration.GetValue<string>("DeepSeek:ApiKey");
```

### Environment variables

Environment variables are used to avoid storage of app secrets in code or in local configuration files. Environment variables override configuration values for all previously specified configuration sources.

```cs
using Fitomad.DeepSeek;

...

var deepSeekSettings = new DeepSeekSettingsBuilder()
    .WithApWithApiKeyFromEnvironmentVariableiKey("DeepSeek:ApiKey")
    .Build();
```

## Dependency Injection. Create an `OpenAIClient` instance

To create a `OpenAIClient` instance, the entry point to the whole Fitomad.DeepSeek framework, developers must use DI.

I provide a helper method registered as an `IServiceCollection` extension named `AddDeepSeekHttpClient` which receives an `DeepSeekSettings` object as parameter.

This is an example of DI in an Unit Testing (xunit) environment.

```cs
var deepSeekSettings = new DeepSeekSettingsBuilder()
    .WithApiKey(_apiKey)
    .Build();

var services = new ServiceCollection();
services.AddDeepSeekHttpClient(settings: aiSettings);
```

Below this lines you will find an example of the usage of DI in ASP.NET

```cs
using Fitomad.DeepSeek;

...

var developApiKey = builder.Configuration["DeepSeek:ApiKey"];

var deepSeekSettings = new DeepSeekSettingsBuilder()
    .WithApiKey(developApiKey)
    .Build();

builder.Services.AddDeepSeekHttpClient(settings: deepSeekSettings);
```

And now, thanks to the built-on DI container available in .NET we can use the `DeepSeekClient` registered type

```cs
...

[ApiController]
[Route("games")]
public class GameController: ControllerBase
{
    private IDeepSeekClient _deepSeekClient;

    public GameController(IDeepSeekClient deepSeekClient)
    {
        _deepSeekClient = deepSeekClient;
    }

    ...
}
```

## Changes

### 1.0.0

- Chat endpoint models brings support the following:
    - `gpt-4o` 🚀
    - `gpt-4-turbo`
    - `gpt-4-turbo-2024-04-09`
    - `gpt-4-turbo-preview`



