// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BestiaryRecipes.dat data.
/// </summary>
public sealed partial class BestiaryRecipesDat : IDat<BestiaryRecipesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets BestiaryRecipeComponentKeys.</summary>
    /// <remarks> references <see cref="BestiaryRecipeComponentDat"/> on <see cref="Specification.GetBestiaryRecipeComponentDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BestiaryRecipeComponentKeys { get; init; }

    /// <summary> Gets Notes.</summary>
    public required string Notes { get; init; }

    /// <summary> Gets Category.</summary>
    /// <remarks> references <see cref="BestiaryRecipeCategoriesDat"/> on <see cref="Specification.GetBestiaryRecipeCategoriesDat"/> index.</remarks>
    public required int? Category { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets Achievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements { get; init; }

    /// <summary> Gets a value indicating whether Unknown73 is set.</summary>
    public required bool Unknown73 { get; init; }

    /// <summary> Gets Unknown74.</summary>
    public required int Unknown74 { get; init; }

    /// <summary> Gets Unknown78.</summary>
    public required int Unknown78 { get; init; }

    /// <summary> Gets Unknown82.</summary>
    public required int Unknown82 { get; init; }

    /// <summary> Gets FlaskMod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? FlaskMod { get; init; }

    /// <inheritdoc/>
    public static BestiaryRecipesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/BestiaryRecipes.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryRecipesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BestiaryRecipeComponentKeys
            (var tempbestiaryrecipecomponentkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var bestiaryrecipecomponentkeysLoading = tempbestiaryrecipecomponentkeysLoading.AsReadOnly();

            // loading Notes
            (var notesLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Category
            (var categoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FlaskMod
            (var flaskmodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryRecipesDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                BestiaryRecipeComponentKeys = bestiaryrecipecomponentkeysLoading,
                Notes = notesLoading,
                Category = categoryLoading,
                Unknown56 = unknown56Loading,
                Achievements = achievementsLoading,
                Unknown73 = unknown73Loading,
                Unknown74 = unknown74Loading,
                Unknown78 = unknown78Loading,
                Unknown82 = unknown82Loading,
                FlaskMod = flaskmodLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
