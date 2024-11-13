﻿using System.Text.Json.Serialization;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using Betalgo.Ranul.OpenAI.ObjectModels.ResponseModels;

namespace Betalgo.Ranul.OpenAI.ObjectModels.SharedModels;

public record ThreadResponse : BaseResponse, IOpenAIModels.IId, IOpenAIModels.ICreatedAt, IOpenAIModels.IMetaData
{
    /// <summary>
    ///     A set of resources that are made available to the assistant's tools in this thread. The resources are specific to
    ///     the type of tool. For example, the code_interpreter tool requires a list of file IDs, while the file_search tool
    ///     requires a list of vector store IDs.
    /// </summary>
    [JsonPropertyName("tool_resources")]
    public ToolResources? ToolResources { get; set; }

    /// <summary>
    ///     The Unix timestamp (in seconds) for when the assistant was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public long CreatedAtUnix { get; set; }

    /// <summary>
    ///    for when the assistant was created.
    /// </summary>
    public DateTimeOffset CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnix);

    /// <summary>
    ///     The identifier, which can be referenced in API endpoints.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    ///     Set of 16 key-value pairs that can be attached to an object.
    ///     This can be useful for storing additional information about the object in a structured format.
    ///     Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }
}