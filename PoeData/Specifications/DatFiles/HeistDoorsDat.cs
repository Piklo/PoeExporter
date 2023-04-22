// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistDoors.dat data.
/// </summary>
public sealed partial class HeistDoorsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required string Unknown8 { get; init; }

    /// <summary> Gets HeistJobsKey1.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.LoadHeistJobsDat"/> index.</remarks>
    public required int? HeistJobsKey1 { get; init; }

    /// <summary> Gets HeistJobsKey2.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.LoadHeistJobsDat"/> index.</remarks>
    public required int? HeistJobsKey2 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required string Unknown48 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required ReadOnlyCollection<string> Unknown56 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required ReadOnlyCollection<string> Unknown72 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int Unknown88 { get; init; }

    /// <summary> Gets HeistAreasKey.</summary>
    /// <remarks> references <see cref="HeistAreasDat"/> on <see cref="Specification.LoadHeistAreasDat"/> index.</remarks>
    public required int? HeistAreasKey { get; init; }

    /// <summary>
    /// Gets HeistDoorsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistDoorsDat.</returns>
    internal static HeistDoorsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistDoors.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistDoorsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeistJobsKey1
            (var heistjobskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistJobsKey2
            (var heistjobskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading Unknown72
            (var tempunknown72Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown72Loading = tempunknown72Loading.AsReadOnly();

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HeistAreasKey
            (var heistareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistDoorsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                HeistJobsKey1 = heistjobskey1Loading,
                HeistJobsKey2 = heistjobskey2Loading,
                Unknown48 = unknown48Loading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
                Unknown88 = unknown88Loading,
                HeistAreasKey = heistareaskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
