// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AfflictionFixedMods.dat data.
/// </summary>
public sealed partial class AfflictionFixedModsDat
{
    /// <summary> Gets Rarity.</summary>
    public required int Rarity { get; init; }

    /// <summary> Gets Mod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? Mod { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }

    /// <summary>
    /// Gets AfflictionFixedModsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AfflictionFixedModsDat.</returns>
    internal static AfflictionFixedModsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AfflictionFixedMods.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AfflictionFixedModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Rarity
            (var rarityLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Mod
            (var modLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AfflictionFixedModsDat()
            {
                Rarity = rarityLoading,
                Mod = modLoading,
                Unknown20 = unknown20Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
