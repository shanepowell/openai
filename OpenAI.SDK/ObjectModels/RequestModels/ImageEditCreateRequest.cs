using System.Text.Json.Serialization;
using Betalgo.Ranul.OpenAI.ObjectModels.SharedModels;

namespace Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;

public record ImageEditFile
{
    public string Name { get; set; } 
    public byte[] Content { get; set; }
}

public record ImageEditCreateRequest : SharedImageRequestBaseModel
{
    /// <summary>
    ///     The image to edit. Must be a valid PNG file, less than 4MB, and square.
    /// </summary>
    public List<ImageEditFile> Images { get; set; } = [];

    /// <summary>
    ///     An additional image whose fully transparent areas (e.g. where alpha is zero) indicate where image should be edited.
    ///     Must be a valid PNG file, less than 4MB, and have the same dimensions as image.
    /// </summary>
    public ImageEditFile? Mask { get; set; }

    /// <summary>
    ///     A text description of the desired image(s). The maximum length is 1000 characters.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; }

    [JsonPropertyName("background")]
    public string? Background { get; set; }

    [JsonPropertyName("input_fidelity")]
    public string? InputFidelity { get; set; }

    [JsonPropertyName("output_compression")]
    public int? OutputCompression { get; set; }
    
    [JsonPropertyName("output_format")]
    public string? OutputFormat { get; set; }
    
    [JsonPropertyName("partial_images")]
    public int? PartialImages { get; set; }

    [JsonPropertyName("quality")]
    public string? Quality { get; set; }

    [JsonPropertyName("stream")]
    public bool? Stream { get; set; }
}