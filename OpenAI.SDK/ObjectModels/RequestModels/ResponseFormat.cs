using System.Text.Json.Serialization;
using Betalgo.Ranul.OpenAI.ObjectModels.SharedModels;

namespace Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;

/// <summary>
///     An object specifying the format that the model must output.
///     Used to enable JSON mode.
/// </summary>
public class ResponseFormat
{
    /// <summary>
    ///     Setting to json_object enables JSON mode.
    ///     This guarantees that the message the model generates is valid JSON.
    ///     Note that the message content may be partial if finish_reason="length",
    ///     which indicates the generation exceeded max_tokens or the conversation exceeded the max context length.
    /// </summary>

    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    [JsonPropertyName("json_schema")]
    public JsonSchema JsonSchema { get; set; }
}

public class JsonSchema
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("strict")]
    public bool? Strict { get; set; }

    [JsonPropertyName("schema")]
    public PropertyDefinition? Schema { get; set; }
}

[JsonConverter(typeof(ResponseFormatOptionConverter))]
public class ResponseFormatOneOfType
{
    public ResponseFormatOneOfType()
    {
    }

    public ResponseFormatOneOfType(string asString)
    {
        AsString = asString;
    }

    public ResponseFormatOneOfType(ResponseFormat asObject)
    {
        AsObject = asObject;
    }

    [JsonIgnore]
    public string? AsString { get; set; }

    [JsonIgnore]
    public ResponseFormat? AsObject { get; set; }
}

public class Audio
{
    [JsonPropertyName("voice")]
    public string Voice { get; set; }

    [JsonPropertyName("format")]
    public string Format { get; set; }
}

public class SearchParameters
{
    private List<object>? _sourcesObjects;

    [JsonIgnore]
    public DateTime? FromDate { get; set; }

    [JsonPropertyName("from_date")]
    public string? FromDateStr
    {
        get => FromDate?.ToString("yyyy-MM-dd");
        set
        {
            if (DateTime.TryParse(value, out var date))
            {
                FromDate = date;
            }
            else
            {
                FromDate = null;
            }
        }
    }

    [JsonIgnore]
    public DateTime? ToDate { get; set; }

    [JsonPropertyName("to_date")]
    public string? ToDateStr
    {
        get => ToDate?.ToString("yyyy-MM-dd");
        set
        {
            if (DateTime.TryParse(value, out var date))
            {
                ToDate = date;
            }
            else
            {
                ToDate = null;
            }
        }
    }

    [JsonPropertyName("max_search_results")]
    public int? MaxSearchResults { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    [JsonPropertyName("return_citations")]
    public bool? ReturnCitations { get; set; }

    [JsonIgnore]
    public List<ISearchParametersSource>? Sources { get; set; }

    [JsonPropertyName("sources")]
    public List<object>? SourcesObjects
    {
        get => _sourcesObjects ?? Sources?.Cast<object>().ToList();
        set => _sourcesObjects = value;
    }
}

public interface ISearchParametersSource
{

    [JsonPropertyName("type")]
    string Type { get; }
}

public class SearchParametersSourceWeb : ISearchParametersSource
{
    [JsonPropertyName("type")]
    public string Type => StaticValues.CompletionStatics.SearchParameters.Mode.Web;
 
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("allowed_websites")]
    public List<string>? AllowedWebsites { get; set; }

    [JsonPropertyName("excluded_websites")]
    public List<string>? ExcludedWebsites { get; set; }

    [JsonPropertyName("safe_search")]
    public bool? SafeSearch { get; set; }
}

public class SearchParametersSourceNews : ISearchParametersSource
{
    [JsonPropertyName("type")]
    public string Type => StaticValues.CompletionStatics.SearchParameters.Mode.News;
 
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("excluded_websites")]
    public List<string>? ExcludedWebsites { get; set; }

    [JsonPropertyName("safe_search")]
    public bool? SafeSearch { get; set; }
}

public class SearchParametersSourceRss : ISearchParametersSource
{
    [JsonPropertyName("type")]
    public string Type => StaticValues.CompletionStatics.SearchParameters.Mode.Rss;
 
    [JsonPropertyName("links")]
    public List<string> Links { get; set; } = new List<string>();
}

public class SearchParametersSourceX : ISearchParametersSource
{
    [JsonPropertyName("type")]
    public string Type => StaticValues.CompletionStatics.SearchParameters.Mode.X;
 
    [JsonPropertyName("x_handles")]
    public List<string> XHandles { get; set; } = new List<string>();
}
