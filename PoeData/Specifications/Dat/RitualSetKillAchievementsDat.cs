// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing RitualSetKillAchievements.dat data.
/// </summary>
public sealed partial class RitualSetKillAchievementsDat : ISpecificationFile<RitualSetKillAchievementsDat>
{
    /// <summary> Gets Achievement.</summary>
    public required int? Achievement { get; init; }

    /// <summary> Gets KillBosses.</summary>
    public required ReadOnlyCollection<int> KillBosses { get; init; }

    /// <inheritdoc/>
    public static RitualSetKillAchievementsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/RitualSetKillAchievements.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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

            // loading referenced tables if any
            // specification.GetAchievementItemsDat();
            // specification.GetMonsterVarietiesDat();

            // loading Achievement
            (var achievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading KillBosses
            (var tempkillbossesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var killbossesLoading = tempkillbossesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
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
