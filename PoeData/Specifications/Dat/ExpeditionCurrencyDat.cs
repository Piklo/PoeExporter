// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ExpeditionCurrency.dat data.
/// </summary>
public sealed partial class ExpeditionCurrencyDat : ISpecificationFile<ExpeditionCurrencyDat>
{
    /// <summary> Gets BaseItemType.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemType { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets NPC.</summary>
    /// <remarks> references <see cref="ExpeditionNPCsDat"/> on <see cref="Specification.GetExpeditionNPCsDat"/> index.</remarks>
    public required int? NPC { get; init; }

    /// <summary> Gets LootSound.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.GetSoundEffectsDat"/> index.</remarks>
    public required int? LootSound { get; init; }

    /// <inheritdoc/>
    public static ExpeditionCurrencyDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ExpeditionCurrency.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionCurrencyDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading NPC
            (var npcLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading LootSound
            (var lootsoundLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionCurrencyDat()
            {
                BaseItemType = baseitemtypeLoading,
                Tier = tierLoading,
                NPC = npcLoading,
                LootSound = lootsoundLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
