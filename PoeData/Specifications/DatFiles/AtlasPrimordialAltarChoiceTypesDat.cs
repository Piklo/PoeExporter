// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasPrimordialAltarChoiceTypes.dat data.
/// </summary>
public sealed partial class AtlasPrimordialAltarChoiceTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets TopIconEater.</summary>
    public required string TopIconEater { get; init; }

    /// <summary> Gets BottomIconEater.</summary>
    public required string BottomIconEater { get; init; }

    /// <summary> Gets TopIconExarch.</summary>
    public required string TopIconExarch { get; init; }

    /// <summary> Gets BottomIconExarch.</summary>
    public required string BottomIconExarch { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary>
    /// Gets AtlasPrimordialAltarChoiceTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AtlasPrimordialAltarChoiceTypesDat.</returns>
    internal static AtlasPrimordialAltarChoiceTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AtlasPrimordialAltarChoiceTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasPrimordialAltarChoiceTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TopIconEater
            (var topiconeaterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BottomIconEater
            (var bottomiconeaterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TopIconExarch
            (var topiconexarchLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BottomIconExarch
            (var bottomiconexarchLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasPrimordialAltarChoiceTypesDat()
            {
                Id = idLoading,
                TopIconEater = topiconeaterLoading,
                BottomIconEater = bottomiconeaterLoading,
                TopIconExarch = topiconexarchLoading,
                BottomIconExarch = bottomiconexarchLoading,
                Text = textLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
