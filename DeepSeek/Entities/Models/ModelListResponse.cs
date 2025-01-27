using System.Text.Json.Serialization;

namespace Fitomad.DeepSeek.Entities.Models;

public record ModelListResponse {
    [JsonPropertyName("object")]
    public string ObjectType { get; init; }
    [JsonPropertyName("data")]
    public ModelResponse[] Results { get; init;}
}