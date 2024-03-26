﻿using System.Text.Json.Serialization;

namespace OpenAI.ObjectModels.ResponseModels;

public record GroqUsageResponse
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("usage")] public UsageResponse? Usage { get; set; }
}