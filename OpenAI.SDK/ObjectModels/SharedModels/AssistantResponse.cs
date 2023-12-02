﻿using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.ObjectModels.SharedModels
{
    public record AssistantResponse : BaseResponse, IOpenAiModels.IId, IOpenAiModels.ICreatedAt, IOpenAiModels.IModel, IOpenAiModels.IMetaData
    {
        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the assistant was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        /// <summary>
        /// The name of the assistant. The maximum length is 256 characters.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The description of the assistant. The maximum length is 512 characters.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// ID of the model to use
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        ///The system instructions that the assistant uses. 
        ///The maximum length is 32768 characters.
        /// </summary>
        [JsonPropertyName("instructions")]
        public string Insturctions { get; set; }

        /// <summary>
        /// A list of tools enabled on the assistant.
        /// </summary>
        [JsonPropertyName("tools")]
        public List<ToolDefinition> Tools { get; set; }

        /// <summary>
        /// A list of file IDs attached to this assistant. 
        /// There can be a maximum of 20 files attached to the assistant. 
        /// Files are ordered by their creation date in ascending order.
        /// </summary>
        [JsonPropertyName("file_ids")]
        public List<string> FileIDs { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> MetaData { get; set; }
    }
}
