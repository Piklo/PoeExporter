// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing HarvestEncounterScaling.dat data.
/// </summary>
public sealed partial class HarvestEncounterScalingDat : ISpecificationFile<HarvestEncounterScalingDat>
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets Multiplier.</summary>
    public required float Multiplier { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets StatsValues.</summary>
    public required ReadOnlyCollection<int> StatsValues { get; init; }

    /// <inheritdoc/>
    public static HarvestEncounterScalingDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/HarvestEncounterScaling.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestEncounterScalingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetStatsDat();

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Multiplier
            (var multiplierLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatsValues
            (var tempstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statsvaluesLoading = tempstatsvaluesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestEncounterScalingDat()
            {
                Level = levelLoading,
                Multiplier = multiplierLoading,
                StatsKeys = statskeysLoading,
                StatsValues = statsvaluesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
