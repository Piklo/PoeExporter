// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AtlasPrimordialAltarChoices.dat data.
/// </summary>
public sealed partial class AtlasPrimordialAltarChoicesDat : IDat<AtlasPrimordialAltarChoicesDat>
{
    /// <summary> Gets Mod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? Mod { get; init; }

    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="AtlasPrimordialAltarChoiceTypesDat"/> on <see cref="Specification.GetAtlasPrimordialAltarChoiceTypesDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <inheritdoc/>
    public static AtlasPrimordialAltarChoicesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/AtlasPrimordialAltarChoices.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasPrimordialAltarChoicesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Mod
            (var modLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasPrimordialAltarChoicesDat()
            {
                Mod = modLoading,
                Type = typeLoading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
