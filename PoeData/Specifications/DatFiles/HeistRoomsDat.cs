// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistRooms.dat data.
/// </summary>
public sealed partial class HeistRoomsDat
{
    /// <summary> Gets HeistAreasKey.</summary>
    /// <remarks> references <see cref="HeistAreasDat"/> on <see cref="Specification.GetHeistAreasDat"/> index.</remarks>
    public required int? HeistAreasKey { get; init; }

    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets ARMFile.</summary>
    public required string ARMFile { get; init; }

    /// <summary> Gets HeistJobsKey1.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.GetHeistJobsDat"/> index.</remarks>
    public required int? HeistJobsKey1 { get; init; }

    /// <summary> Gets HeistJobsKey2.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.GetHeistJobsDat"/> index.</remarks>
    public required int? HeistJobsKey2 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int Unknown60 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required string Unknown72 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required float Unknown80 { get; init; }

    /// <summary> Gets a value indicating whether Unknown84 is set.</summary>
    public required bool Unknown84 { get; init; }

    /// <summary> Gets a value indicating whether Unknown85 is set.</summary>
    public required bool Unknown85 { get; init; }

    /// <summary>
    /// Gets HeistRoomsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistRoomsDat.</returns>
    internal static HeistRoomsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistRooms.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistRoomsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HeistAreasKey
            (var heistareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ARMFile
            (var armfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeistJobsKey1
            (var heistjobskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HeistJobsKey2
            (var heistjobskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown85
            (var unknown85Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistRoomsDat()
            {
                HeistAreasKey = heistareaskeyLoading,
                Id = idLoading,
                ARMFile = armfileLoading,
                HeistJobsKey1 = heistjobskey1Loading,
                HeistJobsKey2 = heistjobskey2Loading,
                Unknown60 = unknown60Loading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown80 = unknown80Loading,
                Unknown84 = unknown84Loading,
                Unknown85 = unknown85Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
