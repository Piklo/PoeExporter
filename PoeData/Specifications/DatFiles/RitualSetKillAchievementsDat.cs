// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing RitualSetKillAchievements.dat data.
/// </summary>
public sealed partial class RitualSetKillAchievementsDat
{
    /// <summary> Gets Achievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? Achievement { get; init; }

    /// <summary> Gets KillBosses.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> KillBosses { get; init; }

    /// <summary>
    /// Gets RitualSetKillAchievementsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of RitualSetKillAchievementsDat.</returns>
    internal static RitualSetKillAchievementsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/RitualSetKillAchievements.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RitualSetKillAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Achievement
            (var achievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading KillBosses
            (var tempkillbossesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var killbossesLoading = tempkillbossesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RitualSetKillAchievementsDat()
            {
                Achievement = achievementLoading,
                KillBosses = killbossesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
