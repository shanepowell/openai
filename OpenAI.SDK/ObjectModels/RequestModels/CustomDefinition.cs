using System.Text.Json.Serialization;

namespace Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;

public class CustomDefinition
{
    /// <summary>
    ///     The name of the custom tool, used to identify it in tool calls.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Optional description of the custom tool, used to provide more context.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    ///     The input format for the custom tool. Default is unconstrained text.
    /// </summary>
    [JsonPropertyName("format")]
    public CustomFormat? Format { get; set; }
}

public class CustomFormat
{
    /// <summary>
    ///     The type of the custom format, e.g., "text", "grammar".
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    ///     A grammar defined by the user.
    /// </summary>
    [JsonPropertyName("grammar")]
    public CustomGrammarFormat? Grammar { get; set; }


    public static CustomFormat DefineText()
    {
        return new CustomFormat
        {
            Type = StaticValues.CompletionStatics.CustomToolType.Text
        };
    }

    public static CustomFormat DefineGrammar(string definition, string syntax)
    {
        return new CustomFormat
        {
            Type = StaticValues.CompletionStatics.CustomToolType.Grammar,
            Grammar = new CustomGrammarFormat
            {
                Definition = definition,
                Syntax = syntax
            }
        };
    }
}

public class CustomGrammarFormat
{
    /// <summary>
    ///     The grammar definition.
    /// </summary>
    [JsonPropertyName("definition")]
    public string Definition { get; set; }

    /// <summary>
    ///     The syntax of the grammar definition. One of lark or regex.
    /// </summary>
    [JsonPropertyName("syntax")]
    public string Syntax { get; set; }
}