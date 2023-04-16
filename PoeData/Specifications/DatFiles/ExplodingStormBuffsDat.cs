// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ExplodingStormBuffs.dat data.
/// </summary>
public sealed partial class ExplodingStormBuffsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BuffDefinitionsKey1.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.GetBuffDefinitionsDat"/> index.</remarks>
    public required int? BuffDefinitionsKey1 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets StatValues.</summary>
    public required ReadOnlyCollection<int> StatValues { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required ReadOnlyCollection<int> Unknown60 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required int Unknown76 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int Unknown80 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required int Unknown84 { get; init; }

    /// <summary> Gets Friendly_MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? Friendly_MonsterVarietiesKey { get; init; }

    /// <summary> Gets MiscObjectsKey.</summary>
    /// <remarks> references <see cref="MiscObjectsDat"/> on <see cref="Specification.GetMiscObjectsDat"/> index.</remarks>
    public required int? MiscObjectsKey { get; init; }

    /// <summary> Gets MiscAnimatedKey.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey { get; init; }

    /// <summary> Gets BuffVisualsKey.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.GetBuffVisualsDat"/> index.</remarks>
    public required int? BuffVisualsKey { get; init; }

    /// <summary> Gets Enemy_MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? Enemy_MonsterVarietiesKey { get; init; }

    /// <summary> Gets Unknown168.</summary>
    public required int Unknown168 { get; init; }

    /// <summary> Gets Unknown172.</summary>
    public required int Unknown172 { get; init; }

    /// <summary> Gets Unknown176.</summary>
    public required int Unknown176 { get; init; }

    /// <summary> Gets BuffDefinitionsKey2.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.GetBuffDefinitionsDat"/> index.</remarks>
    public required int? BuffDefinitionsKey2 { get; init; }

    /// <summary> Gets a value indicating whether IsOnlySpawningNearPlayer is set.</summary>
    public required bool IsOnlySpawningNearPlayer { get; init; }

    /// <summary>
    /// Gets ExplodingStormBuffsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ExplodingStormBuffsDat.</returns>
    internal static ExplodingStormBuffsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ExplodingStormBuffs.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExplodingStormBuffsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffDefinitionsKey1
            (var buffdefinitionskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatValues
            (var tempstatvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statvaluesLoading = tempstatvaluesLoading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var tempunknown60Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown60Loading = tempunknown60Loading.AsReadOnly();

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Friendly_MonsterVarietiesKey
            (var friendly_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MiscObjectsKey
            (var miscobjectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MiscAnimatedKey
            (var miscanimatedkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BuffVisualsKey
            (var buffvisualskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Enemy_MonsterVarietiesKey
            (var enemy_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown168
            (var unknown168Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown172
            (var unknown172Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown176
            (var unknown176Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey2
            (var buffdefinitionskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsOnlySpawningNearPlayer
            (var isonlyspawningnearplayerLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExplodingStormBuffsDat()
            {
                Id = idLoading,
                BuffDefinitionsKey1 = buffdefinitionskey1Loading,
                Unknown24 = unknown24Loading,
                StatValues = statvaluesLoading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
                Unknown84 = unknown84Loading,
                Friendly_MonsterVarietiesKey = friendly_monstervarietieskeyLoading,
                MiscObjectsKey = miscobjectskeyLoading,
                MiscAnimatedKey = miscanimatedkeyLoading,
                BuffVisualsKey = buffvisualskeyLoading,
                Enemy_MonsterVarietiesKey = enemy_monstervarietieskeyLoading,
                Unknown168 = unknown168Loading,
                Unknown172 = unknown172Loading,
                Unknown176 = unknown176Loading,
                BuffDefinitionsKey2 = buffdefinitionskey2Loading,
                IsOnlySpawningNearPlayer = isonlyspawningnearplayerLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
