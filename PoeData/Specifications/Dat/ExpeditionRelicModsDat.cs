// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ExpeditionRelicMods.dat data.
/// </summary>
public sealed partial class ExpeditionRelicModsDat : ISpecificationFile<ExpeditionRelicModsDat>
{
    /// <summary> Gets Mod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? Mod { get; init; }

    /// <summary> Gets Categories.</summary>
    /// <remarks> references <see cref="ExpeditionRelicModCategoriesDat"/> on <see cref="Specification.GetExpeditionRelicModCategoriesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Categories { get; init; }

    /// <summary> Gets DestroyAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> DestroyAchievements { get; init; }

    /// <inheritdoc/>
    public static ExpeditionRelicModsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ExpeditionRelicMods.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionRelicModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Mod
            (var modLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Categories
            (var tempcategoriesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var categoriesLoading = tempcategoriesLoading.AsReadOnly();

            // loading DestroyAchievements
            (var tempdestroyachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var destroyachievementsLoading = tempdestroyachievementsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionRelicModsDat()
            {
                Mod = modLoading,
                Categories = categoriesLoading,
                DestroyAchievements = destroyachievementsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
