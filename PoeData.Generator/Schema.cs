using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace PoeData.Generator;

internal sealed class Schema : IEquatable<Schema>
{
    [JsonPropertyName("version")]
    public required int Version { get; init; }

    [JsonPropertyName("createdAt")]
    public required int CreatedAt { get; init; }

    [JsonPropertyName("tables")]
    public required Table[] Tables { get; init; }

    [JsonPropertyName("enumerations")]
    public required Enumeration[] Enumerations { get; init; }

    public bool Equals(Schema other)
    {
        return Version == other.Version && CreatedAt == other.CreatedAt && Tables.SequenceEqual(other.Tables) && Enumerations.SequenceEqual(other.Enumerations);
    }

    public override bool Equals(object? obj)
    {
        return obj is Schema other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hashCodeGenerator = new HashCode();

        hashCodeGenerator.Add(Version);
        hashCodeGenerator.Add(CreatedAt);
        hashCodeGenerator.Add(Tables);
        hashCodeGenerator.Add(Enumerations);

        var hashCode = hashCodeGenerator.ToHashCode();

        return hashCode;
    }
}

internal sealed class Table : IEquatable<Table>
{
    [JsonPropertyName("validFor")]
    public required int ValidFor { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("columns")]
    public required Column[] Columns { get; init; }

    [JsonPropertyName("tags")]
    public required string[] Tags { get; init; }

    public bool Equals(Table other)
    {
        return ValidFor == other.ValidFor && Name == other.Name && Columns.SequenceEqual(other.Columns) && Tags.SequenceEqual(other.Tags);
    }

    public override bool Equals(object? obj)
    {
        return obj is Table other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hashCodeGenerator = new HashCode();

        hashCodeGenerator.Add(ValidFor);
        hashCodeGenerator.Add(Name);
        hashCodeGenerator.Add(Columns);
        hashCodeGenerator.Add(Tags);

        var hashCode = hashCodeGenerator.ToHashCode();

        return hashCode;
    }
}

internal sealed class Column : IEquatable<Column>
{
    [JsonPropertyName("name")]
    public required string? Name { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("array")]
    public required bool Array { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("unique")]
    public required bool Unique { get; init; }

    [JsonPropertyName("localized")]
    public required bool Localized { get; init; }

    [JsonPropertyName("references")]
    public required Reference? References { get; init; }

    [JsonPropertyName("until")]
    public required object? Until { get; init; }

    [JsonPropertyName("file")]
    public required string File { get; init; }

    [JsonPropertyName("files")]
    public required string[]? Files { get; init; }

    [JsonPropertyName("interval")]
    public required bool Interval { get; init; }

    public bool Equals(Column other)
    {
        return Name == other.Name
               && Description == other.Description
               && Array == other.Array
               && Type == other.Type
               && Unique == other.Unique
               && Localized == other.Localized
               && Equals(References, other.References)
               && Equals(Until, other.Until)
               && File == other.File
               && EqualityExtensions.SequenceEqual(Files, other.Files)
               && Interval == other.Interval;
    }

    public override bool Equals(object? obj)
    {
        return obj is Column other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hashCodeGenerator = new HashCode();

        hashCodeGenerator.Add(Name);
        hashCodeGenerator.Add(Description);
        hashCodeGenerator.Add(Array);
        hashCodeGenerator.Add(Type);
        hashCodeGenerator.Add(Unique);
        hashCodeGenerator.Add(Localized);
        hashCodeGenerator.Add(References);
        hashCodeGenerator.Add(Until);
        hashCodeGenerator.Add(File);
        hashCodeGenerator.Add(Files);
        hashCodeGenerator.Add(Interval);

        var hashCode = hashCodeGenerator.ToHashCode();

        return hashCode;
    }
}

internal sealed class Reference : IEquatable<Reference>
{
    [JsonPropertyName("table")]
    public required string Table { get; init; }

    [JsonPropertyName("column")]
    public string? Column { get; init; }

    public bool Equals(Reference other)
    {
        return Table == other.Table && Column == other.Column;
    }

    public override bool Equals(object? obj)
    {
        return obj is Reference other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hashCodeGenerator = new HashCode();
        hashCodeGenerator.Add(Table);
        hashCodeGenerator.Add(Column);

        var hashCode = hashCodeGenerator.ToHashCode();

        return hashCode;
    }
}

internal sealed class Enumeration : IEquatable<Enumeration>
{
    [JsonPropertyName("validFor")]
    public required int ValidFor { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("indexing")]
    public required int Indexing { get; init; }

    [JsonPropertyName("enumerators")]
    public required string[] Enumerators { get; init; }

    public bool Equals(Enumeration other)
    {
        return ValidFor == other.ValidFor && Name == other.Name && Indexing == other.Indexing && Enumerators.SequenceEqual(other.Enumerators);
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hashCodeGenerator = new HashCode();

        hashCodeGenerator.Add(ValidFor);
        hashCodeGenerator.Add(Name);
        hashCodeGenerator.Add(Indexing);
        hashCodeGenerator.Add(Enumerators);

        var hashCode = hashCodeGenerator.ToHashCode();

        return hashCode;
    }
}
