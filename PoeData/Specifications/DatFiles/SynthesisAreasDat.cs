// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SynthesisAreas.dat data.
/// </summary>
public sealed partial class SynthesisAreasDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets TopologiesKey.</summary>
    /// <remarks> references <see cref="TopologiesDat"/> on <see cref="Specification.GetTopologiesDat"/> index.</remarks>
    public required int? TopologiesKey { get; init; }

    /// <summary> Gets MonsterPacksKeys.</summary>
    /// <remarks> references <see cref="MonsterPacksDat"/> on <see cref="Specification.GetMonsterPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterPacksKeys { get; init; }

    /// <summary> Gets ArtFile.</summary>
    public required string ArtFile { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets SynthesisAreaSizeKey.</summary>
    /// <remarks> references <see cref="SynthesisAreaSizeDat"/> on <see cref="Specification.GetSynthesisAreaSizeDat"/> index.</remarks>
    public required int? SynthesisAreaSizeKey { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItemsKey { get; init; }

    /// <summary>
    /// Gets SynthesisAreasDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SynthesisAreasDat.</returns>
    internal static SynthesisAreasDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SynthesisAreas.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SynthesisAreasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TopologiesKey
            (var topologieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterPacksKeys
            (var tempmonsterpackskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterpackskeysLoading = tempmonsterpackskeysLoading.AsReadOnly();

            // loading ArtFile
            (var artfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SynthesisAreaSizeKey
            (var synthesisareasizekeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKey
            (var achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SynthesisAreasDat()
            {
                Id = idLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                Weight = weightLoading,
                TopologiesKey = topologieskeyLoading,
                MonsterPacksKeys = monsterpackskeysLoading,
                ArtFile = artfileLoading,
                Name = nameLoading,
                SynthesisAreaSizeKey = synthesisareasizekeyLoading,
                AchievementItemsKey = achievementitemskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
