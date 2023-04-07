// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MiscEffectPacks.dat data.
/// </summary>
public sealed partial class MiscEffectPacksDat : IDat<MiscEffectPacksDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets EPKFile.</summary>
    public required string EPKFile { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets PreloadGroups.</summary>
    /// <remarks> references <see cref="PreloadGroupsDat"/> on <see cref="Specification.GetPreloadGroupsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PreloadGroups { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary> Gets PlayerOnly_EPKFile.</summary>
    public required string PlayerOnly_EPKFile { get; init; }

    /// <inheritdoc/>
    public static MiscEffectPacksDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/MiscEffectPacks.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MiscEffectPacksDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EPKFile
            (var epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PreloadGroups
            (var temppreloadgroupsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var preloadgroupsLoading = temppreloadgroupsLoading.AsReadOnly();

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading PlayerOnly_EPKFile
            (var playeronly_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MiscEffectPacksDat()
            {
                Id = idLoading,
                EPKFile = epkfileLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                PreloadGroups = preloadgroupsLoading,
                Unknown44 = unknown44Loading,
                PlayerOnly_EPKFile = playeronly_epkfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
