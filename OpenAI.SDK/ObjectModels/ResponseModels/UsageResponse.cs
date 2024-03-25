using System.Text.Json.Serialization;

namespace OpenAI.ObjectModels.ResponseModels;

public record UsageResponse
{
    [JsonPropertyName("prompt_tokens")] public int PromptTokens { get; set; }

    [JsonPropertyName("completion_tokens")]
    public int? CompletionTokens { get; set; }

    [JsonPropertyName("total_tokens")] public int TotalTokens { get; set; }

    // Groq API specific fields
    [JsonPropertyName("queue_time")] public float? QueueTime { get; set; }
    [JsonPropertyName("prompt_time")] public float? PromptTime { get; set; }
    [JsonPropertyName("completion_time")] public float? CompletionTime { get; set; }
    [JsonPropertyName("total_time")] public float? TotalTime { get; set; }
}