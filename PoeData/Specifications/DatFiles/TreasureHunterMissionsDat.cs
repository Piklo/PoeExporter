// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TreasureHunterMissions.dat data.
/// </summary>
public sealed partial class TreasureHunterMissionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int? Unknown8 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<int> Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required ReadOnlyCollection<int> Unknown56 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required ReadOnlyCollection<int> Unknown72 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int Unknown88 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required int Unknown92 { get; init; }

    /// <summary> Gets Unknown96.</summary>
    public required int Unknown96 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required string Unknown100 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required string Unknown108 { get; init; }

    /// <summary> Gets OTFile.</summary>
    public required string OTFile { get; init; }

    /// <summary> Gets a value indicating whether Unknown124 is set.</summary>
    public required bool Unknown124 { get; init; }

    /// <summary> Gets Unknown125.</summary>
    public required int Unknown125 { get; init; }

    /// <summary> Gets Unknown129.</summary>
    public required int Unknown129 { get; init; }

    /// <summary>
    /// Gets TreasureHunterMissionsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of TreasureHunterMissionsDat.</returns>
    internal static TreasureHunterMissionsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/TreasureHunterMissions.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TreasureHunterMissionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading Unknown72
            (var tempunknown72Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown72Loading = tempunknown72Loading.AsReadOnly();

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OTFile
            (var otfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown125
            (var unknown125Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown129
            (var unknown129Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TreasureHunterMissionsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
                Unknown88 = unknown88Loading,
                Unknown92 = unknown92Loading,
                Unknown96 = unknown96Loading,
                Unknown100 = unknown100Loading,
                Unknown108 = unknown108Loading,
                OTFile = otfileLoading,
                Unknown124 = unknown124Loading,
                Unknown125 = unknown125Loading,
                Unknown129 = unknown129Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
