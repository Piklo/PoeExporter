// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing GrantedEffectStatSets.dat data.
/// </summary>
public sealed partial class GrantedEffectStatSetsDat : IDat<GrantedEffectStatSetsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ImplicitStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ImplicitStats { get; init; }

    /// <summary> Gets ConstantStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ConstantStats { get; init; }

    /// <summary> Gets ConstantStatsValues.</summary>
    public required ReadOnlyCollection<int> ConstantStatsValues { get; init; }

    /// <summary> Gets BaseEffectiveness.</summary>
    public required float BaseEffectiveness { get; init; }

    /// <summary> Gets IncrementalEffectiveness.</summary>
    public required float IncrementalEffectiveness { get; init; }

    /// <inheritdoc/>
    public static GrantedEffectStatSetsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/GrantedEffectStatSets.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectStatSetsDat()
            {
                Id = idLoading,
                ImplicitStats = implicitstatsLoading,
                ConstantStats = constantstatsLoading,
                ConstantStatsValues = constantstatsvaluesLoading,
                BaseEffectiveness = baseeffectivenessLoading,
                IncrementalEffectiveness = incrementaleffectivenessLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
