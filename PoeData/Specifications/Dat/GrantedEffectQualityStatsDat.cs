// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing GrantedEffectQualityStats.dat data.
/// </summary>
public sealed partial class GrantedEffectQualityStatsDat
{
    /// <summary> Gets GrantedEffectsKey.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.GetGrantedEffectsDat"/> index.</remarks>
    public required int? GrantedEffectsKey { get; init; }

    /// <summary> Gets SetId.</summary>
    public required int SetId { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets StatsValuesPermille.</summary>
    public required ReadOnlyCollection<int> StatsValuesPermille { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required ReadOnlyCollection<int> Unknown56 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required ReadOnlyCollection<int> Unknown72 { get; init; }

    /// <summary>
    /// Gets GrantedEffectQualityStatsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of GrantedEffectQualityStatsDat.</returns>
    internal static GrantedEffectQualityStatsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/GrantedEffectQualityStats.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrantedEffectQualityStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading GrantedEffectsKey
            (var grantedeffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SetId
            (var setidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatsValuesPermille
            (var tempstatsvaluespermilleLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statsvaluespermilleLoading = tempstatsvaluespermilleLoading.AsReadOnly();

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading Unknown72
            (var tempunknown72Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown72Loading = tempunknown72Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectQualityStatsDat()
            {
                GrantedEffectsKey = grantedeffectskeyLoading,
                SetId = setidLoading,
                StatsKeys = statskeysLoading,
                StatsValuesPermille = statsvaluespermilleLoading,
                Weight = weightLoading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
