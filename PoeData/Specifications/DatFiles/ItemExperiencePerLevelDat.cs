// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemExperiencePerLevel.dat data.
/// </summary>
public sealed partial class ItemExperiencePerLevelDat
{
    /// <summary> Gets ItemExperienceType.</summary>
    /// <remarks> references <see cref="ItemExperienceTypesDat"/> on <see cref="Specification.LoadItemExperienceTypesDat"/> index.</remarks>
    public required int? ItemExperienceType { get; init; }

    /// <summary> Gets ItemCurrentLevel.</summary>
    public required int ItemCurrentLevel { get; init; }

    /// <summary> Gets Experience.</summary>
    public required int Experience { get; init; }

    /// <summary>
    /// Gets ItemExperiencePerLevelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ItemExperiencePerLevelDat.</returns>
    internal static ItemExperiencePerLevelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ItemExperiencePerLevel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemExperiencePerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ItemExperienceType
            (var itemexperiencetypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemCurrentLevel
            (var itemcurrentlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Experience
            (var experienceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemExperiencePerLevelDat()
            {
                ItemExperienceType = itemexperiencetypeLoading,
                ItemCurrentLevel = itemcurrentlevelLoading,
                Experience = experienceLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
