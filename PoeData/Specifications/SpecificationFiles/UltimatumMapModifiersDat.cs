// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing UltimatumMapModifiers.dat data.
/// </summary>
public sealed partial class UltimatumMapModifiersDat : ISpecificationFile<UltimatumMapModifiersDat>
{
    /// <summary> Gets Stat.</summary>
    public required int? Stat { get; init; }

    /// <summary> Gets Mods.</summary>
    public required ReadOnlyCollection<int> Mods { get; init; }

    /// <inheritdoc/>
    public static UltimatumMapModifiersDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/UltimatumMapModifiers.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UltimatumMapModifiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetStatsDat();
            // specification.GetUltimatumModifiersDat();

            // loading Stat
            (var statLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Mods
            (var tempmodsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modsLoading = tempmodsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UltimatumMapModifiersDat()
            {
                Stat = statLoading,
                Mods = modsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
