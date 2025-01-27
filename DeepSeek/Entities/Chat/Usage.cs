using System.Text.Json.Serialization;

namespace Fitomad.DeepSeek.Entities.Chat;

public record Usage
{
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; init; }
    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; init; }
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; init; }
}