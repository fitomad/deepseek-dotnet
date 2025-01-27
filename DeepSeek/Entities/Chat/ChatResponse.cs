using System.Text.Json.Serialization;

namespace Fitomad.DeepSeek.Entities.Chat;

public record ChatResponse
{
    [JsonPropertyName("id")]
    public string ResponseId { get; init; }
    [JsonPropertyName("object")]
    public string Object { get; init; }
    [JsonPropertyName("created")]
    public int CreationDate { get; init; }
    [JsonPropertyName("model")]
    public string Model { get; init; }
    [JsonPropertyName("system_fingerprint")]
    public string SystemFingerprint { get; init; }
    [JsonPropertyName("choices")]
    public Choice[] Choices { get; init; }
    [JsonPropertyName("usage")]
    public Usage RequestUsage { get; init; }
}