﻿using System.Text.Json.Serialization;

namespace OpenAI.ObjectModels.RequestModels;

/// <summary>
///     Describes a tool call returned from GPT.
///     The tool calls generated by the model, such as function calls.
/// </summary>
public class ToolCall
{
    /// <summary>
    ///     The ID of the tool call.
    ///     This ID must be referenced 
    ///     when you submit the tool outputs in using the Submit tool outputs to run endpoint.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    ///     The type of the tool. Currently, only function is supported.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    ///     The function that the model called.
    /// </summary>
    [JsonPropertyName("function")]
    public FunctionCall? FunctionCall { get; set; }
}