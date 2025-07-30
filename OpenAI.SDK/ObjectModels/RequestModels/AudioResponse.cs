using System.Text.Json.Serialization;

namespace Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;

public class AudioResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("data")]
    public string Data { get; set; }

    [JsonPropertyName("expires_at")]
    public int ExpiresAt { get; set; }

    [JsonPropertyName("transcript")]
    public string Transcript { get; set; }
}