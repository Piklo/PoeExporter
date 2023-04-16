// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Pet.dat data.
/// </summary>
public sealed partial class PetDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets HASH32.</summary>
    public required int HASH32 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets a value indicating whether Unknown36 is set.</summary>
    public required bool Unknown36 { get; init; }

    /// <summary> Gets a value indicating whether Unknown37 is set.</summary>
    public required bool Unknown37 { get; init; }

    /// <summary> Gets Unknown38.</summary>
    public required ReadOnlyCollection<int> Unknown38 { get; init; }

    /// <summary> Gets Unknown54.</summary>
    public required int? Unknown54 { get; init; }

    /// <summary>
    /// Gets PetDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PetDat.</returns>
    internal static PetDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Pet.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PetDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HASH32
            (var hash32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown38
            (var tempunknown38Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown38Loading = tempunknown38Loading.AsReadOnly();

            // loading Unknown54
            (var unknown54Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PetDat()
            {
                Id = idLoading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
                HASH16 = hash16Loading,
                HASH32 = hash32Loading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                Unknown37 = unknown37Loading,
                Unknown38 = unknown38Loading,
                Unknown54 = unknown54Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
