// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing RaceTimes.dat data.
/// </summary>
public sealed partial class RaceTimesDat
{
    /// <summary> Gets RacesKey.</summary>
    /// <remarks> references <see cref="RacesDat"/> on <see cref="Specification.LoadRacesDat"/> index.</remarks>
    public required int? RacesKey { get; init; }

    /// <summary> Gets Index.</summary>
    public required int Index { get; init; }

    /// <summary> Gets StartUNIXTime.</summary>
    public required int StartUNIXTime { get; init; }

    /// <summary> Gets EndUNIXTime.</summary>
    public required int EndUNIXTime { get; init; }

    /// <summary>
    /// Gets RaceTimesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of RaceTimesDat.</returns>
    internal static RaceTimesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/RaceTimes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RaceTimesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading RacesKey
            (var raceskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Index
            (var indexLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StartUNIXTime
            (var startunixtimeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EndUNIXTime
            (var endunixtimeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RaceTimesDat()
            {
                RacesKey = raceskeyLoading,
                Index = indexLoading,
                StartUNIXTime = startunixtimeLoading,
                EndUNIXTime = endunixtimeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
