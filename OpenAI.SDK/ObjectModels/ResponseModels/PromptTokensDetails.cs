using System.Text.Json.Serialization;

namespace Betalgo.Ranul.OpenAI.ObjectModels.ResponseModels
{
    public class PromptTokensDetails
    {
        [JsonPropertyName("cached_tokens")]
        public int? CachedTokens { get; set; }  
        [JsonPropertyName("audio_tokens")]
        public int? AudioTokens { get; set; }
        [JsonPropertyName("image_tokens")]
        public int? ImageTokens { get; set; }
        [JsonPropertyName("text_tokens")]
        public int? TextTokens { get; set; }
    }
}

