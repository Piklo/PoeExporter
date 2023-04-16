// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MetamorphosisScaling.dat data.
/// </summary>
public sealed partial class MetamorphosisScalingDat
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets StatValueMultiplier.</summary>
    public required float StatValueMultiplier { get; init; }

    /// <summary> Gets ScalingStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ScalingStats { get; init; }

    /// <summary> Gets ScalingValues.</summary>
    public required ReadOnlyCollection<int> ScalingValues { get; init; }

    /// <summary> Gets ScalingValues2.</summary>
    public required ReadOnlyCollection<int> ScalingValues2 { get; init; }

    /// <summary> Gets ScalingValuesHardmode.</summary>
    public required ReadOnlyCollection<int> ScalingValuesHardmode { get; init; }

    /// <summary> Gets ScalingValuesHardmode2.</summary>
    public required ReadOnlyCollection<int> ScalingValuesHardmode2 { get; init; }

    /// <summary>
    /// Gets MetamorphosisScalingDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MetamorphosisScalingDat.</returns>
    internal static MetamorphosisScalingDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MetamorphosisScaling.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MetamorphosisScalingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatValueMultiplier
            (var statvaluemultiplierLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading ScalingStats
            (var tempscalingstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var scalingstatsLoading = tempscalingstatsLoading.AsReadOnly();

            // loading ScalingValues
            (var tempscalingvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var scalingvaluesLoading = tempscalingvaluesLoading.AsReadOnly();

            // loading ScalingValues2
            (var tempscalingvalues2Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var scalingvalues2Loading = tempscalingvalues2Loading.AsReadOnly();

            // loading ScalingValuesHardmode
            (var tempscalingvalueshardmodeLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var scalingvalueshardmodeLoading = tempscalingvalueshardmodeLoading.AsReadOnly();

            // loading ScalingValuesHardmode2
            (var tempscalingvalueshardmode2Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var scalingvalueshardmode2Loading = tempscalingvalueshardmode2Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MetamorphosisScalingDat()
            {
                Level = levelLoading,
                StatValueMultiplier = statvaluemultiplierLoading,
                ScalingStats = scalingstatsLoading,
                ScalingValues = scalingvaluesLoading,
                ScalingValues2 = scalingvalues2Loading,
                ScalingValuesHardmode = scalingvalueshardmodeLoading,
                ScalingValuesHardmode2 = scalingvalueshardmode2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
