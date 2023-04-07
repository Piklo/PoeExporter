// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing PerandusGuards.dat data.
/// </summary>
public sealed partial class PerandusGuardsDat : IDat<PerandusGuardsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets MonsterPacksKeys.</summary>
    /// <remarks> references <see cref="MonsterPacksDat"/> on <see cref="Specification.GetMonsterPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterPacksKeys { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<int> Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <inheritdoc/>
    public static PerandusGuardsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/PerandusGuards.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PerandusGuardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterPacksKeys
            (var tempmonsterpackskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterpackskeysLoading = tempmonsterpackskeysLoading.AsReadOnly();

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PerandusGuardsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                MonsterPacksKeys = monsterpackskeysLoading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
