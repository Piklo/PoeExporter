using System.Text.Json.Serialization;

namespace PoeDataGenerator.SchemaJson;

/// <summary>
/// Class containing data about each column in the schema json file.
/// </summary>
internal sealed class Column
{
    /// <summary>Gets name.</summary>
    [JsonPropertyName("name")]
    public required string? Name { get; init; }

    /// <summary>Gets description.</summary>
    [JsonPropertyName("description")]
    public required string? Description { get; init; }

    /// <summary>Gets a value indicating whether column is an array.</summary>
    [JsonPropertyName("array")]
    public required bool Array { get; init; }

    /// <summary>Gets type.</summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>Gets a value indicating whether values in the column are unique.</summary>
    [JsonPropertyName("unique")]
    public required bool Unique { get; init; }

    /// <summary>Gets a value indicating whether values in the column are localized.</summary>
    [JsonPropertyName("localized")]
    public required bool Localized { get; init; }

    /// <summary>Gets info about referenced table by that column.</summary>
    [JsonPropertyName("references")]
    public required References? References { get; init; }

    /// <summary>Gets until.</summary>
    /// <remarks>always null?.</remarks>
    [JsonPropertyName("until")]
    public required object? Until { get; init; }

    /// <summary>Gets file extension.</summary>
    [JsonPropertyName("file")]
    public required string? File { get; init; }

    /// <summary>Gets files extensions.</summary>
    [JsonPropertyName("files")]
    public required string[]? Files { get; init; }
}
