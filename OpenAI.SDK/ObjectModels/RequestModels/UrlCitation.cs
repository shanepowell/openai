using System.Text.Json.Serialization;

namespace Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;

public class UrlCitation
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("end_index")]
    public int EndIndex { get; set; }
    [JsonPropertyName("start_index")]
    public int StartIndex { get; set; }
}