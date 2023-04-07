// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing IncursionBrackets.dat data.
/// </summary>
public sealed partial class IncursionBracketsDat : IDat<IncursionBracketsDat>
{
    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets Incursion_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? Incursion_WorldAreasKey { get; init; }

    /// <summary> Gets Template_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? Template_WorldAreasKey { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required ReadOnlyCollection<float> Unknown36 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required float Unknown52 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <inheritdoc/>
    public static IncursionBracketsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/IncursionBrackets.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionBracketsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Incursion_WorldAreasKey
            (var incursion_worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Template_WorldAreasKey
            (var template_worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown36
            (var tempunknown36Loading, offset) = SpecificationFileLoader.LoadFloatArray(decompressedFile, offset, dataOffset);
            var unknown36Loading = tempunknown36Loading.AsReadOnly();

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionBracketsDat()
            {
                MinLevel = minlevelLoading,
                Incursion_WorldAreasKey = incursion_worldareaskeyLoading,
                Template_WorldAreasKey = template_worldareaskeyLoading,
                Unknown36 = unknown36Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
