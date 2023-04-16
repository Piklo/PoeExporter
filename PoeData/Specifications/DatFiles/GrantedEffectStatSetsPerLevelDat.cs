// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GrantedEffectStatSetsPerLevel.dat data.
/// </summary>
public sealed partial class GrantedEffectStatSetsPerLevelDat
{
    /// <summary> Gets StatSet.</summary>
    /// <remarks> references <see cref="GrantedEffectStatSetsDat"/> on <see cref="Specification.GetGrantedEffectStatSetsDat"/> index.</remarks>
    public required int? StatSet { get; init; }

    /// <summary> Gets GemLevel.</summary>
    public required int GemLevel { get; init; }

    /// <summary> Gets PlayerLevelReq.</summary>
    public required float PlayerLevelReq { get; init; }

    /// <summary> Gets SpellCritChance.</summary>
    public required int SpellCritChance { get; init; }

    /// <summary> Gets AttackCritChance.</summary>
    public required int AttackCritChance { get; init; }

    /// <summary> Gets BaseMultiplier.</summary>
    public required int BaseMultiplier { get; init; }

    /// <summary> Gets DamageEffectiveness.</summary>
    public required int DamageEffectiveness { get; init; }

    /// <summary> Gets AdditionalFlags.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AdditionalFlags { get; init; }

    /// <summary> Gets FloatStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> FloatStats { get; init; }

    /// <summary> Gets InterpolationBases.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> InterpolationBases { get; init; }

    /// <summary> Gets AdditionalStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AdditionalStats { get; init; }

    /// <summary> Gets StatInterpolations.</summary>
    /// <remarks> references <see cref="StatInterpolationTypesDat"/> on <see cref="Specification.GetStatInterpolationTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatInterpolations { get; init; }

    /// <summary> Gets FloatStatsValues.</summary>
    public required ReadOnlyCollection<float> FloatStatsValues { get; init; }

    /// <summary> Gets BaseResolvedValues.</summary>
    public required ReadOnlyCollection<int> BaseResolvedValues { get; init; }

    /// <summary> Gets AdditionalStatsValues.</summary>
    public required ReadOnlyCollection<int> AdditionalStatsValues { get; init; }

    /// <summary> Gets GrantedEffects.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.GetGrantedEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> GrantedEffects { get; init; }

    /// <summary>
    /// Gets GrantedEffectStatSetsPerLevelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of GrantedEffectStatSetsPerLevelDat.</returns>
    internal static GrantedEffectStatSetsPerLevelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/GrantedEffectStatSetsPerLevel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrantedEffectStatSetsPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatSet
            (var statsetLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading GemLevel
            (var gemlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PlayerLevelReq
            (var playerlevelreqLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading SpellCritChance
            (var spellcritchanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AttackCritChance
            (var attackcritchanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseMultiplier
            (var basemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DamageEffectiveness
            (var damageeffectivenessLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AdditionalFlags
            (var tempadditionalflagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var additionalflagsLoading = tempadditionalflagsLoading.AsReadOnly();

            // loading FloatStats
            (var tempfloatstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var floatstatsLoading = tempfloatstatsLoading.AsReadOnly();

            // loading InterpolationBases
            (var tempinterpolationbasesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var interpolationbasesLoading = tempinterpolationbasesLoading.AsReadOnly();

            // loading AdditionalStats
            (var tempadditionalstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var additionalstatsLoading = tempadditionalstatsLoading.AsReadOnly();

            // loading StatInterpolations
            (var tempstatinterpolationsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statinterpolationsLoading = tempstatinterpolationsLoading.AsReadOnly();

            // loading FloatStatsValues
            (var tempfloatstatsvaluesLoading, offset) = SpecificationFileLoader.LoadFloatArray(decompressedFile, offset, dataOffset);
            var floatstatsvaluesLoading = tempfloatstatsvaluesLoading.AsReadOnly();

            // loading BaseResolvedValues
            (var tempbaseresolvedvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var baseresolvedvaluesLoading = tempbaseresolvedvaluesLoading.AsReadOnly();

            // loading AdditionalStatsValues
            (var tempadditionalstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var additionalstatsvaluesLoading = tempadditionalstatsvaluesLoading.AsReadOnly();

            // loading GrantedEffects
            (var tempgrantedeffectsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var grantedeffectsLoading = tempgrantedeffectsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectStatSetsPerLevelDat()
            {
                StatSet = statsetLoading,
                GemLevel = gemlevelLoading,
                PlayerLevelReq = playerlevelreqLoading,
                SpellCritChance = spellcritchanceLoading,
                AttackCritChance = attackcritchanceLoading,
                BaseMultiplier = basemultiplierLoading,
                DamageEffectiveness = damageeffectivenessLoading,
                AdditionalFlags = additionalflagsLoading,
                FloatStats = floatstatsLoading,
                InterpolationBases = interpolationbasesLoading,
                AdditionalStats = additionalstatsLoading,
                StatInterpolations = statinterpolationsLoading,
                FloatStatsValues = floatstatsvaluesLoading,
                BaseResolvedValues = baseresolvedvaluesLoading,
                AdditionalStatsValues = additionalstatsvaluesLoading,
                GrantedEffects = grantedeffectsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
