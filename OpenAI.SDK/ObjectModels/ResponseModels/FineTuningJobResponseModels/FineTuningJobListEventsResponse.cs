﻿using System.Text.Json.Serialization;
using OpenAI.ObjectModels.SharedModels;

namespace OpenAI.ObjectModels.ResponseModels.FineTuningJobResponseModels;

public record FineTuningJobListEventsResponse : BaseResponse
{
    [JsonPropertyName("data")]
    public List<EventResponse> Data { get; set; }

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}