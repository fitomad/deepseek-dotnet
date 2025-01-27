using System.Text.Json.Serialization; 

namespace Fitomad.DeepSeek.Entities.Chat;

public record Choice
{
    [JsonPropertyName("index")]
    public int Index { get; init; }
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; init; }
    [JsonPropertyName("message")]
    public Message ReceivedMessage { get; init; }

    public record Message
    {
        [JsonPropertyName("role")]
        public string Role { get; init; }
        [JsonPropertyName("content")]
        public string Content { get; init; }
    }
}