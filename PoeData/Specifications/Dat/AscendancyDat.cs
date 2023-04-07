// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Ascendancy.dat data.
/// </summary>
public sealed partial class AscendancyDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ClassNo.</summary>
    public required int ClassNo { get; init; }

    /// <summary> Gets Characters.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.GetCharactersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Characters { get; init; }

    /// <summary> Gets CoordinateRect.</summary>
    public required string CoordinateRect { get; init; }

    /// <summary> Gets RGBFlavourTextColour.</summary>
    public required string RGBFlavourTextColour { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets FlavourText.</summary>
    public required string FlavourText { get; init; }

    /// <summary> Gets OGGFile.</summary>
    public required string OGGFile { get; init; }

    /// <summary> Gets PassiveTreeImage.</summary>
    public required string PassiveTreeImage { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required int Unknown76 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int Unknown80 { get; init; }

    /// <summary> Gets BackgroundImage.</summary>
    public required string BackgroundImage { get; init; }

    /// <summary>
    /// Gets AscendancyDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AscendancyDat.</returns>
    internal static AscendancyDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Ascendancy.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AscendancyDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ClassNo
            (var classnoLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Characters
            (var tempcharactersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var charactersLoading = tempcharactersLoading.AsReadOnly();

            // loading CoordinateRect
            (var coordinaterectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RGBFlavourTextColour
            (var rgbflavourtextcolourLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OGGFile
            (var oggfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveTreeImage
            (var passivetreeimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BackgroundImage
            (var backgroundimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AscendancyDat()
            {
                Id = idLoading,
                ClassNo = classnoLoading,
                Characters = charactersLoading,
                CoordinateRect = coordinaterectLoading,
                RGBFlavourTextColour = rgbflavourtextcolourLoading,
                Name = nameLoading,
                FlavourText = flavourtextLoading,
                OGGFile = oggfileLoading,
                PassiveTreeImage = passivetreeimageLoading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
                BackgroundImage = backgroundimageLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
