// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LabyrinthSecrets.dat data.
/// </summary>
public sealed partial class LabyrinthSecretsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Id2.</summary>
    public required string Id2 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required ReadOnlyCollection<int> Unknown16 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets LabyrinthSecretEffectsKeys0.</summary>
    /// <remarks> references <see cref="LabyrinthSecretEffectsDat"/> on <see cref="Specification.LoadLabyrinthSecretEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSecretEffectsKeys0 { get; init; }

    /// <summary> Gets LabyrinthSecretEffectsKeys1.</summary>
    /// <remarks> references <see cref="LabyrinthSecretEffectsDat"/> on <see cref="Specification.LoadLabyrinthSecretEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSecretEffectsKeys1 { get; init; }

    /// <summary> Gets LabyrinthSecretEffectsKeys2.</summary>
    /// <remarks> references <see cref="LabyrinthSecretEffectsDat"/> on <see cref="Specification.LoadLabyrinthSecretEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSecretEffectsKeys2 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int Unknown88 { get; init; }

    /// <summary> Gets LabyrinthSecretEffectsKeys3.</summary>
    /// <remarks> references <see cref="LabyrinthSecretEffectsDat"/> on <see cref="Specification.LoadLabyrinthSecretEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSecretEffectsKeys3 { get; init; }

    /// <summary> Gets a value indicating whether Unknown108 is set.</summary>
    public required bool Unknown108 { get; init; }

    /// <summary> Gets a value indicating whether Unknown109 is set.</summary>
    public required bool Unknown109 { get; init; }

    /// <summary> Gets Unknown110.</summary>
    public required int Unknown110 { get; init; }

    /// <summary> Gets a value indicating whether Unknown114 is set.</summary>
    public required bool Unknown114 { get; init; }

    /// <summary> Gets a value indicating whether Unknown115 is set.</summary>
    public required bool Unknown115 { get; init; }

    /// <summary> Gets a value indicating whether Unknown116 is set.</summary>
    public required bool Unknown116 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItemsKey { get; init; }

    /// <summary> Gets LabyrinthTierMinimum.</summary>
    public required int LabyrinthTierMinimum { get; init; }

    /// <summary> Gets LabyrinthTierMaximum.</summary>
    public required int LabyrinthTierMaximum { get; init; }

    /// <summary> Gets a value indicating whether Unknown149 is set.</summary>
    public required bool Unknown149 { get; init; }

    /// <summary>
    /// Gets LabyrinthSecretsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of LabyrinthSecretsDat.</returns>
    internal static LabyrinthSecretsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/LabyrinthSecrets.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthSecretsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Id2
            (var id2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var tempunknown16Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown16Loading = tempunknown16Loading.AsReadOnly();

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LabyrinthSecretEffectsKeys0
            (var templabyrinthsecreteffectskeys0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsecreteffectskeys0Loading = templabyrinthsecreteffectskeys0Loading.AsReadOnly();

            // loading LabyrinthSecretEffectsKeys1
            (var templabyrinthsecreteffectskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsecreteffectskeys1Loading = templabyrinthsecreteffectskeys1Loading.AsReadOnly();

            // loading LabyrinthSecretEffectsKeys2
            (var templabyrinthsecreteffectskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsecreteffectskeys2Loading = templabyrinthsecreteffectskeys2Loading.AsReadOnly();

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LabyrinthSecretEffectsKeys3
            (var templabyrinthsecreteffectskeys3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsecreteffectskeys3Loading = templabyrinthsecreteffectskeys3Loading.AsReadOnly();

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown110
            (var unknown110Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown114
            (var unknown114Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKey
            (var achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading LabyrinthTierMinimum
            (var labyrinthtierminimumLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LabyrinthTierMaximum
            (var labyrinthtiermaximumLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthSecretsDat()
            {
                Id = idLoading,
                Id2 = id2Loading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                LabyrinthSecretEffectsKeys0 = labyrinthsecreteffectskeys0Loading,
                LabyrinthSecretEffectsKeys1 = labyrinthsecreteffectskeys1Loading,
                LabyrinthSecretEffectsKeys2 = labyrinthsecreteffectskeys2Loading,
                Unknown88 = unknown88Loading,
                LabyrinthSecretEffectsKeys3 = labyrinthsecreteffectskeys3Loading,
                Unknown108 = unknown108Loading,
                Unknown109 = unknown109Loading,
                Unknown110 = unknown110Loading,
                Unknown114 = unknown114Loading,
                Unknown115 = unknown115Loading,
                Unknown116 = unknown116Loading,
                Name = nameLoading,
                AchievementItemsKey = achievementitemskeyLoading,
                LabyrinthTierMinimum = labyrinthtierminimumLoading,
                LabyrinthTierMaximum = labyrinthtiermaximumLoading,
                Unknown149 = unknown149Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
