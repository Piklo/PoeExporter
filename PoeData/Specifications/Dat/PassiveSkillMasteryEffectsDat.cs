// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing PassiveSkillMasteryEffects.dat data.
/// </summary>
public sealed partial class PassiveSkillMasteryEffectsDat : IDat<PassiveSkillMasteryEffectsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets Stat1Value.</summary>
    public required int Stat1Value { get; init; }

    /// <summary> Gets Stat2Value.</summary>
    public required int Stat2Value { get; init; }

    /// <summary> Gets Stat3Value.</summary>
    public required int Stat3Value { get; init; }

    /// <summary> Gets AchievementItem.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItem { get; init; }

    /// <inheritdoc/>
    public static PassiveSkillMasteryEffectsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/PassiveSkillMasteryEffects.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillMasteryEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading Stat1Value
            (var stat1valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Value
            (var stat2valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat3Value
            (var stat3valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementItem
            (var achievementitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillMasteryEffectsDat()
            {
                Id = idLoading,
                HASH16 = hash16Loading,
                Stats = statsLoading,
                Stat1Value = stat1valueLoading,
                Stat2Value = stat2valueLoading,
                Stat3Value = stat3valueLoading,
                AchievementItem = achievementitemLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
