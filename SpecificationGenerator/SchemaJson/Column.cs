using System.Text.Json.Serialization;

namespace SpecificationGenerator.SchemaJson;

/// <summary>
/// Class containing data about each column in the schema json file.
/// </summary>
public class Column
{
    /// <summary>Gets name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Gets description.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Gets a value indicating whether column is an array.</summary>
    [JsonPropertyName("array")]
    public bool Array { get; init; }

    /// <summary>Gets type.</summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>Gets a value indicating whether values in the column are unique.</summary>
    [JsonPropertyName("unique")]
    public bool Unique { get; init; }

    /// <summary>Gets a value indicating whether values in the column are localized.</summary>
    [JsonPropertyName("localized")]
    public bool Localized { get; init; }

    /// <summary>Gets info about referenced table by that column.</summary>
    [JsonPropertyName("references")]
    public References? References { get; init; }

    /// <summary>Gets until.</summary>
    /// <remarks>always null?.</remarks>
    [JsonPropertyName("until")]
    public object? Until { get; init; }

    /// <summary>Gets file extension.</summary>
    [JsonPropertyName("file")]
    public string? File { get; init; }

    /// <summary>Gets files extensions.</summary>
    [JsonPropertyName("files")]
    public string[]? Files { get; init; }
}
