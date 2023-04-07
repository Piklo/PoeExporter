// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TalismanMonsterMods.dat data.
/// </summary>
public sealed partial class TalismanMonsterModsDat
{
    /// <summary> Gets ModTypeKey.</summary>
    /// <remarks> references <see cref="ModTypeDat"/> on <see cref="Specification.GetModTypeDat"/> index.</remarks>
    public required int? ModTypeKey { get; init; }

    /// <summary> Gets ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? ModsKey { get; init; }

    /// <summary>
    /// Gets TalismanMonsterModsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of TalismanMonsterModsDat.</returns>
    internal static TalismanMonsterModsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/TalismanMonsterMods.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TalismanMonsterModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ModTypeKey
            (var modtypekeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TalismanMonsterModsDat()
            {
                ModTypeKey = modtypekeyLoading,
                ModsKey = modskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
