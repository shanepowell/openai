using System.Text.Json.Serialization;

namespace Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;

public class Annotation
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("url_citation")]
    public UrlCitation? UrlCitation { get; set; }
}