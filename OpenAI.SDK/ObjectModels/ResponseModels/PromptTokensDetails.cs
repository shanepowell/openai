using System.Text.Json.Serialization;

namespace Betalgo.Ranul.OpenAI.ObjectModels.ResponseModels
{
    public class PromptTokensDetails
    {
        [JsonPropertyName("cached_tokens")]
        public int? CachedTokens { get; set; }  
        [JsonPropertyName("audio_tokens")]
        public int? AudioTokens { get; set; }
    }
}

