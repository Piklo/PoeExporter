﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing MapDeviceRecipes.dat data.
/// </summary>
public sealed partial class MapDeviceRecipesDat : ISpecificationFile<MapDeviceRecipesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets RecipeItems.</summary>
    public required ReadOnlyCollection<int> RecipeItems { get; init; }

    /// <summary> Gets WorldArea.</summary>
    public required int? WorldArea { get; init; }

    /// <summary> Gets MicrotransactionPortalVariation.</summary>
    public required int? MicrotransactionPortalVariation { get; init; }

    /// <summary> Gets AreaLevel.</summary>
    public required int AreaLevel { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int? Unknown60 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required int Unknown76 { get; init; }

    /// <summary> Gets a value indicating whether Unknown80 is set.</summary>
    public required bool Unknown80 { get; init; }

    /// <summary> Gets a value indicating whether Unknown81 is set.</summary>
    public required bool Unknown81 { get; init; }

    /// <summary> Gets a value indicating whether Unknown82 is set.</summary>
    public required bool Unknown82 { get; init; }

    /// <summary> Gets OpenAchievemnts.</summary>
    public required ReadOnlyCollection<int> OpenAchievemnts { get; init; }

    /// <inheritdoc/>
    public static MapDeviceRecipesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MapDeviceRecipes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapDeviceRecipesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();
            // specification.GetWorldAreasDat();
            // specification.GetMicrotransactionPortalVariationsDat();
            // specification.GetAchievementItemsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RecipeItems
            (var temprecipeitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var recipeitemsLoading = temprecipeitemsLoading.AsReadOnly();

            // loading WorldArea
            (var worldareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MicrotransactionPortalVariation
            (var microtransactionportalvariationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown81
            (var unknown81Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading OpenAchievemnts
            (var tempopenachievemntsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var openachievemntsLoading = tempopenachievemntsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapDeviceRecipesDat()
            {
                Id = idLoading,
                RecipeItems = recipeitemsLoading,
                WorldArea = worldareaLoading,
                MicrotransactionPortalVariation = microtransactionportalvariationLoading,
                AreaLevel = arealevelLoading,
                Unknown60 = unknown60Loading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
                Unknown81 = unknown81Loading,
                Unknown82 = unknown82Loading,
                OpenAchievemnts = openachievemntsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
