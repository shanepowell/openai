using System.Text.Json.Serialization;

namespace OpenAI.ObjectModels.RequestModels;

public class ResponseFormat
{
    public static ResponseFormat JsonObjectResponseFormat = new() {Type = "json_object"};

    [JsonPropertyName("type")]
    object? Type { get; set; }
}