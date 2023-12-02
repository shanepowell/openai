﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.ObjectModels.SharedModels
{
    /// <summary>
    /// File citation |File path
    /// </summary>
    public record MessageAnnotation
    {
        /// <summary>
        /// type can be：file_citation、file_path
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
        /// <summary>
        /// The text in the message content that needs to be replaced.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("start_index")]
        public int StartIndex { get; set; }

        [JsonPropertyName("end_index")]
        public int EndIndex { get; set; }

        /// <summary>
        ///  The ID of the specific File the citation/content  is from.
        /// </summary>
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        /// <summary>
        ///  The specific quote in the file.
        /// </summary>
        [JsonPropertyName("quote")]
        public string Quote { get; set; }
    }
}
