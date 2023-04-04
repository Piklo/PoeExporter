// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Talismans.dat data.
/// </summary>
public sealed partial class TalismansDat : ISpecificationFile<TalismansDat>
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? ModsKey { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets a value indicating whether Unknown40 is set.</summary>
    public required bool Unknown40 { get; init; }

    /// <summary> Gets a value indicating whether Unknown41 is set.</summary>
    public required bool Unknown41 { get; init; }

    /// <summary> Gets Unknown42.</summary>
    public required int? Unknown42 { get; init; }

    /// <summary> Gets Unknown58.</summary>
    public required int? Unknown58 { get; init; }

    /// <summary> Gets Unknown74.</summary>
    public required int Unknown74 { get; init; }

    /// <inheritdoc/>
    public static TalismansDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Talismans.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TalismansDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown58
            (var unknown58Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TalismansDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                SpawnWeight = spawnweightLoading,
                ModsKey = modskeyLoading,
                Tier = tierLoading,
                Unknown40 = unknown40Loading,
                Unknown41 = unknown41Loading,
                Unknown42 = unknown42Loading,
                Unknown58 = unknown58Loading,
                Unknown74 = unknown74Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
