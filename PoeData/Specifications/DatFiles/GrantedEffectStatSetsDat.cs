// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GrantedEffectStatSets.dat data.
/// </summary>
public sealed partial class GrantedEffectStatSetsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ImplicitStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ImplicitStats { get; init; }

    /// <summary> Gets ConstantStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ConstantStats { get; init; }

    /// <summary> Gets ConstantStatsValues.</summary>
    public required ReadOnlyCollection<int> ConstantStatsValues { get; init; }

    /// <summary> Gets BaseEffectiveness.</summary>
    public required float BaseEffectiveness { get; init; }

    /// <summary> Gets IncrementalEffectiveness.</summary>
    public required float IncrementalEffectiveness { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary>
    /// Gets GrantedEffectStatSetsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of GrantedEffectStatSetsDat.</returns>
    internal static GrantedEffectStatSetsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/GrantedEffectStatSets.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrantedEffectStatSetsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ImplicitStats
            (var tempimplicitstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var implicitstatsLoading = tempimplicitstatsLoading.AsReadOnly();

            // loading ConstantStats
            (var tempconstantstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var constantstatsLoading = tempconstantstatsLoading.AsReadOnly();

            // loading ConstantStatsValues
            (var tempconstantstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var constantstatsvaluesLoading = tempconstantstatsvaluesLoading.AsReadOnly();

            // loading BaseEffectiveness
            (var baseeffectivenessLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading IncrementalEffectiveness
            (var incrementaleffectivenessLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectStatSetsDat()
            {
                Id = idLoading,
                ImplicitStats = implicitstatsLoading,
                ConstantStats = constantstatsLoading,
                ConstantStatsValues = constantstatsvaluesLoading,
                BaseEffectiveness = baseeffectivenessLoading,
                IncrementalEffectiveness = incrementaleffectivenessLoading,
                Unknown64 = unknown64Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
