namespace Fitomad.DeepSeek.Endpoints.Chat;

public enum ChatModelType
{
    DeepSeekChat,
    DeepSeekReasoner
}

public static class ChatModelKindExtension
{
    public static string GetValue(this ChatModelType model)
    {
        var modelName = model switch
        {
            ChatModelType.DeepSeekChat => "deepseek-chat",
            ChatModelType.DeepSeekReasoner => "deepseek-reasoner",
            _ => "deepseek-chat"
        };

        return modelName;
    }
}