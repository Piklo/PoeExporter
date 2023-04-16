// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UltimatumEncounterTypes.dat data.
/// </summary>
public sealed partial class UltimatumEncounterTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets ProgressBarText.</summary>
    public required string ProgressBarText { get; init; }

    /// <summary> Gets a value indicating whether Unknown24 is set.</summary>
    public required bool Unknown24 { get; init; }

    /// <summary> Gets a value indicating whether Unknown25 is set.</summary>
    public required bool Unknown25 { get; init; }

    /// <summary> Gets NormalAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NormalAchievements { get; init; }

    /// <summary> Gets InscribedAchievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? InscribedAchievement { get; init; }

    /// <summary>
    /// Gets UltimatumEncounterTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of UltimatumEncounterTypesDat.</returns>
    internal static UltimatumEncounterTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/UltimatumEncounterTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UltimatumEncounterTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ProgressBarText
            (var progressbartextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading NormalAchievements
            (var tempnormalachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var normalachievementsLoading = tempnormalachievementsLoading.AsReadOnly();

            // loading InscribedAchievement
            (var inscribedachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UltimatumEncounterTypesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                ProgressBarText = progressbartextLoading,
                Unknown24 = unknown24Loading,
                Unknown25 = unknown25Loading,
                NormalAchievements = normalachievementsLoading,
                InscribedAchievement = inscribedachievementLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
