// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing StatsAffectingGeneration.dat data.
/// </summary>
public sealed partial class StatsAffectingGenerationDat
{
    /// <summary> Gets Stat.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Stat { get; init; }

    /// <summary> Gets StatValue.</summary>
    public required int StatValue { get; init; }

    /// <summary>
    /// Gets StatsAffectingGenerationDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of StatsAffectingGenerationDat.</returns>
    internal static StatsAffectingGenerationDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/StatsAffectingGeneration.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StatsAffectingGenerationDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Stat
            (var statLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatValue
            (var statvalueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StatsAffectingGenerationDat()
            {
                Stat = statLoading,
                StatValue = statvalueLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
