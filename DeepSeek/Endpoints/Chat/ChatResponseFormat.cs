namespace Fitomad.DeepSeek.Endpoints.Chat;

public enum ChatResponseFormat
{
    Text,
    JsonObject
}

public static class ChatResponseFormatExtension
{
    public static string GetValue(this ChatResponseFormat responseFormat)
    {
        var format = responseFormat switch 
        {
            ChatResponseFormat.Text => "text",
            ChatResponseFormat.JsonObject => "json_object",
            _ => "json_object"
        };

        return format;
    }
}