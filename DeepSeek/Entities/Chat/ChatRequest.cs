using System.Text.Json.Serialization;

namespace Fitomad.DeepSeek.Entities.Chat;

public record ChatRequest
{
    [JsonPropertyName("model")]
    public string ModelName { get; internal set; }
    [JsonPropertyName("messages")]
    public List<IMessage> Messages { get; internal set; }
    [JsonPropertyName("frequency_penalty")]
    public double FrequencyPenalty { get; internal set; }
    [JsonPropertyName("max_tokens")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MaximumTokens { get; internal set; }
    [JsonPropertyName("n")]
    public int CompletionChoices { get; internal set; }
    [JsonPropertyName("presence_penalty")]
    public double PresencePenalty { get; internal set; }
    [JsonPropertyName("response_format")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Response? ResponseFormat { get; internal set; }
    [JsonPropertyName("temperature")]
    public double Temperature { get; internal set; }
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? User { get; internal set; }

    internal ChatRequest()
    {
        FrequencyPenalty = 0.0;
        PresencePenalty = 0.0;
        Temperature = 1.0;
        Messages = new List<IMessage>();
        CompletionChoices = 1;
    }

    public interface IMessage
    {
        [JsonPropertyName("content")]
        string Content { get; }
        [JsonPropertyName("role")]
        string Role { get;}
    }

    public record struct Message: IMessage
    {
        [JsonPropertyName("content")]
        public string Content { get; internal set; }

        [JsonPropertyName("role")]
        public string Role { get; internal set; }

        [JsonPropertyName("name")]
        public string Name { get; internal set; }
    }

    public record struct ToolMessage: IMessage
    {
        [JsonPropertyName("content")]
        public string Content { get; internal set; }
        [JsonPropertyName("role")]
        public string Role { get; internal set; }
        [JsonPropertyName("tool_call_id")]
        public string ToolCallId { get; internal set; }
    }

    public record Response 
    {
        [JsonPropertyName("type")]
        public string Type { get; internal init; }
    }
}