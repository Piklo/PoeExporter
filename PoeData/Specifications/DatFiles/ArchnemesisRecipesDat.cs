// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ArchnemesisRecipes.dat data.
/// </summary>
public sealed partial class ArchnemesisRecipesDat
{
    /// <summary> Gets Result.</summary>
    /// <remarks> references <see cref="ArchnemesisModsDat"/> on <see cref="Specification.GetArchnemesisModsDat"/> index.</remarks>
    public required int? Result { get; init; }

    /// <summary> Gets Recipe.</summary>
    /// <remarks> references <see cref="ArchnemesisModsDat"/> on <see cref="Specification.GetArchnemesisModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Recipe { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary>
    /// Gets ArchnemesisRecipesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ArchnemesisRecipesDat.</returns>
    internal static ArchnemesisRecipesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ArchnemesisRecipes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArchnemesisRecipesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Result
            (var resultLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Recipe
            (var temprecipeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var recipeLoading = temprecipeLoading.AsReadOnly();

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArchnemesisRecipesDat()
            {
                Result = resultLoading,
                Recipe = recipeLoading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
