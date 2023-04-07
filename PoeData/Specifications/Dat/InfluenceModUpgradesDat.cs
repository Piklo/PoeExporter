// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing InfluenceModUpgrades.dat data.
/// </summary>
public sealed partial class InfluenceModUpgradesDat : IDat<InfluenceModUpgradesDat>
{
    /// <summary> Gets InfluenceMod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? InfluenceMod { get; init; }

    /// <summary> Gets UpgradedMod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? UpgradedMod { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <inheritdoc/>
    public static InfluenceModUpgradesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/InfluenceModUpgrades.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new InfluenceModUpgradesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading InfluenceMod
            (var influencemodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading UpgradedMod
            (var upgradedmodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new InfluenceModUpgradesDat()
            {
                InfluenceMod = influencemodLoading,
                UpgradedMod = upgradedmodLoading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
