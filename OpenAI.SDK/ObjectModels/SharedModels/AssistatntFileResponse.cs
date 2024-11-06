﻿using System.Text.Json.Serialization;
using Betalgo.Ranul.OpenAI.ObjectModels.ResponseModels;

namespace Betalgo.Ranul.OpenAI.ObjectModels.SharedModels
{
    public record AssistatntFileResponse : BaseResponse, IOpenAIModels.IId, IOpenAIModels.ICreatedAt
    {
        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id {  get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the assistant was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        /// <summary>
        /// The assistant ID that the file is attached to.
        /// </summary>
        [JsonPropertyName("assistant_id")]
        public string AssistantId {  get; set; }
    }
}
