// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HellscapeMods.dat data.
/// </summary>
public sealed partial class HellscapeModsDat
{
    /// <summary> Gets Mod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? Mod { get; init; }

    /// <summary> Gets TiersWhitelist.</summary>
    public required ReadOnlyCollection<int> TiersWhitelist { get; init; }

    /// <summary> Gets TransformAchievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? TransformAchievement { get; init; }

    /// <summary> Gets ModFamilies.</summary>
    /// <remarks> references <see cref="ModFamilyDat"/> on <see cref="Specification.GetModFamilyDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModFamilies { get; init; }

    /// <summary>
    /// Gets HellscapeModsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HellscapeModsDat.</returns>
    internal static HellscapeModsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HellscapeMods.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Mod
            (var modLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading TiersWhitelist
            (var temptierswhitelistLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var tierswhitelistLoading = temptierswhitelistLoading.AsReadOnly();

            // loading TransformAchievement
            (var transformachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ModFamilies
            (var tempmodfamiliesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modfamiliesLoading = tempmodfamiliesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeModsDat()
            {
                Mod = modLoading,
                TiersWhitelist = tierswhitelistLoading,
                TransformAchievement = transformachievementLoading,
                ModFamilies = modfamiliesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
