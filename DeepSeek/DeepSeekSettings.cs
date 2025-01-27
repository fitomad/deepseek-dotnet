namespace Fitomad.DeepSeek;

public struct DeepSeekSettings
{
    private string _apiKey;

    public string ApiKey 
    {
        get => _apiKey;
        internal set => _apiKey = value;
    }
}