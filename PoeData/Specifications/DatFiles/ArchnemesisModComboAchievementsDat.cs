// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ArchnemesisModComboAchievements.dat data.
/// </summary>
public sealed partial class ArchnemesisModComboAchievementsDat
{
    /// <summary> Gets Achievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? Achievement { get; init; }

    /// <summary> Gets Mods.</summary>
    /// <remarks> references <see cref="ArchnemesisModsDat"/> on <see cref="Specification.GetArchnemesisModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Mods { get; init; }

    /// <summary>
    /// Gets ArchnemesisModComboAchievementsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ArchnemesisModComboAchievementsDat.</returns>
    internal static ArchnemesisModComboAchievementsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ArchnemesisModComboAchievements.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArchnemesisModComboAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Achievement
            (var achievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Mods
            (var tempmodsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modsLoading = tempmodsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArchnemesisModComboAchievementsDat()
            {
                Achievement = achievementLoading,
                Mods = modsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
