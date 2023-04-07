// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HeistStorageLayout.dat data.
/// </summary>
public sealed partial class HeistStorageLayoutDat : IDat<HeistStorageLayoutDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BaseItemType.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemType { get; init; }

    /// <summary> Gets a value indicating whether Unknown24 is set.</summary>
    public required bool Unknown24 { get; init; }

    /// <summary> Gets ButtonFile.</summary>
    public required string ButtonFile { get; init; }

    /// <summary> Gets Unknown33.</summary>
    public required int Unknown33 { get; init; }

    /// <summary> Gets HeistJobsKey.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.GetHeistJobsDat"/> index.</remarks>
    public required int? HeistJobsKey { get; init; }

    /// <summary> Gets Columns.</summary>
    public required int Columns { get; init; }

    /// <summary> Gets Rows.</summary>
    public required int Rows { get; init; }

    /// <summary> Gets Unknown61.</summary>
    public required int Unknown61 { get; init; }

    /// <summary> Gets Unknown65.</summary>
    public required int Unknown65 { get; init; }

    /// <summary> Gets Unknown69.</summary>
    public required int Unknown69 { get; init; }

    /// <summary> Gets Unknown73.</summary>
    public required int Unknown73 { get; init; }

    /// <summary> Gets ItemClass.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.GetItemClassesDat"/> index.</remarks>
    public required int? ItemClass { get; init; }

    /// <inheritdoc/>
    public static HeistStorageLayoutDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/HeistStorageLayout.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistStorageLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ButtonFile
            (var buttonfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HeistJobsKey
            (var heistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Columns
            (var columnsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Rows
            (var rowsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemClass
            (var itemclassLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistStorageLayoutDat()
            {
                Id = idLoading,
                BaseItemType = baseitemtypeLoading,
                Unknown24 = unknown24Loading,
                ButtonFile = buttonfileLoading,
                Unknown33 = unknown33Loading,
                HeistJobsKey = heistjobskeyLoading,
                Columns = columnsLoading,
                Rows = rowsLoading,
                Unknown61 = unknown61Loading,
                Unknown65 = unknown65Loading,
                Unknown69 = unknown69Loading,
                Unknown73 = unknown73Loading,
                ItemClass = itemclassLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
