// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing CharacterPanelDescriptionModes.dat data.
/// </summary>
public sealed partial class CharacterPanelDescriptionModesDat : ISpecificationFile<CharacterPanelDescriptionModesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required string Unknown8 { get; init; }

    /// <summary> Gets FormatString_Positive.</summary>
    public required string FormatString_Positive { get; init; }

    /// <summary> Gets FormatString_Negative.</summary>
    public required string FormatString_Negative { get; init; }

    /// <inheritdoc/>
    public static CharacterPanelDescriptionModesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/CharacterPanelDescriptionModes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterPanelDescriptionModesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FormatString_Positive
            (var formatstring_positiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FormatString_Negative
            (var formatstring_negativeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterPanelDescriptionModesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                FormatString_Positive = formatstring_positiveLoading,
                FormatString_Negative = formatstring_negativeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
