// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing EclipseMods.dat data.
/// </summary>
public sealed partial class EclipseModsDat
{
    /// <summary> Gets Key.</summary>
    public required string Key { get; init; }

    /// <summary> Gets SpawnWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_TagsKeys { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <summary> Gets ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? ModsKey { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets a value indicating whether IsPrefix is set.</summary>
    public required bool IsPrefix { get; init; }

    /// <inheritdoc/>
    public static EclipseModsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/EclipseMods.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EclipseModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Key
            (var keyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsPrefix
            (var isprefixLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EclipseModsDat()
            {
                Key = keyLoading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                ModsKey = modskeyLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                IsPrefix = isprefixLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
