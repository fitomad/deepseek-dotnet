namespace Fitomad.DeepSeek;

public class DeepSeekSettingsBuilder
{
    private const string UserSecretApiKey = "";
    private DeepSeekSettings _settings;

    public DeepSeekSettingsBuilder()
    {
        _settings = new DeepSeekSettings();
    }

    public DeepSeekSettingsBuilder WithApiKey(string apiKey)
    {
        _settings.ApiKey = apiKey;
        return this;
    }

    public DeepSeekSettingsBuilder WithApiKeyFromEnvironmentVariable(string name)
    {
        var key = Environment.GetEnvironmentVariable(name);

        if(key is not null)
        {
            _settings.ApiKey = key;
        }

        return this;
    }

    public DeepSeekSettings Build()
    {
        if(string.IsNullOrEmpty(_settings.ApiKey))
        {
            throw new DeepSeekException("Yoy must specified an API key");
        }

        return _settings;
    }
}