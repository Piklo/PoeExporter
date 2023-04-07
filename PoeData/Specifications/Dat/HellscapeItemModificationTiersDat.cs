// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HellscapeItemModificationTiers.dat data.
/// </summary>
public sealed partial class HellscapeItemModificationTiersDat : IDat<HellscapeItemModificationTiersDat>
{
    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets a value indicating whether IsMap is set.</summary>
    public required bool IsMap { get; init; }

    /// <summary> Gets Unknown5.</summary>
    public required int Unknown5 { get; init; }

    /// <summary> Gets MinItemLvl.</summary>
    public required int MinItemLvl { get; init; }

    /// <inheritdoc/>
    public static HellscapeItemModificationTiersDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/HellscapeItemModificationTiers.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeItemModificationTiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsMap
            (var ismapLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown5
            (var unknown5Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinItemLvl
            (var minitemlvlLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeItemModificationTiersDat()
            {
                Tier = tierLoading,
                IsMap = ismapLoading,
                Unknown5 = unknown5Loading,
                MinItemLvl = minitemlvlLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
