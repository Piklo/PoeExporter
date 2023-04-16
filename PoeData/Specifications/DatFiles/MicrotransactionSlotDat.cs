// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MicrotransactionSlot.dat data.
/// </summary>
public sealed partial class MicrotransactionSlotDat
{
    /// <summary> Gets Id.</summary>
    /// <remarks> references <see cref="MicrotransactionSlotIdDat"/> on <see cref="Specification.GetMicrotransactionSlotIdDat"/> index.</remarks>
    public required int Id { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int? Unknown4 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets MicrotransactionSlotDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MicrotransactionSlotDat.</returns>
    internal static MicrotransactionSlotDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MicrotransactionSlot.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionSlotDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionSlotDat()
            {
                Id = idLoading,
                Unknown4 = unknown4Loading,
                Name = nameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
