// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GrantedEffectsPerLevel.dat data.
/// </summary>
public sealed partial class GrantedEffectsPerLevelDat
{
    /// <summary> Gets GrantedEffect.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required int? GrantedEffect { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets PlayerLevelReq.</summary>
    public required float PlayerLevelReq { get; init; }

    /// <summary> Gets CostMultiplier.</summary>
    public required int CostMultiplier { get; init; }

    /// <summary> Gets StoredUses.</summary>
    public required int StoredUses { get; init; }

    /// <summary> Gets Cooldown.</summary>
    public required int Cooldown { get; init; }

    /// <summary> Gets CooldownBypassType.</summary>
    /// <remarks> references <see cref="CooldownBypassTypesDat"/> on <see cref="Specification.LoadCooldownBypassTypesDat"/> index.</remarks>
    public required int CooldownBypassType { get; init; }

    /// <summary> Gets VaalSouls.</summary>
    public required int VaalSouls { get; init; }

    /// <summary> Gets VaalStoredUses.</summary>
    public required int VaalStoredUses { get; init; }

    /// <summary> Gets CooldownGroup.</summary>
    public required int CooldownGroup { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets SoulGainPreventionDuration.</summary>
    public required int SoulGainPreventionDuration { get; init; }

    /// <summary> Gets AttackSpeedMultiplier.</summary>
    public required int AttackSpeedMultiplier { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets CostAmounts.</summary>
    public required ReadOnlyCollection<int> CostAmounts { get; init; }

    /// <summary> Gets CostTypes.</summary>
    /// <remarks> references <see cref="CostTypesDat"/> on <see cref="Specification.LoadCostTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CostTypes { get; init; }

    /// <summary> Gets ManaReservationFlat.</summary>
    public required int ManaReservationFlat { get; init; }

    /// <summary> Gets ManaReservationPercent.</summary>
    public required int ManaReservationPercent { get; init; }

    /// <summary> Gets LifeReservationFlat.</summary>
    public required int LifeReservationFlat { get; init; }

    /// <summary> Gets LifeReservationPercent.</summary>
    public required int LifeReservationPercent { get; init; }

    /// <summary> Gets AttackTime.</summary>
    public required int AttackTime { get; init; }

    /// <summary>
    /// Gets GrantedEffectsPerLevelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of GrantedEffectsPerLevelDat.</returns>
    internal static GrantedEffectsPerLevelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/GrantedEffectsPerLevel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrantedEffectsPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading GrantedEffect
            (var grantedeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PlayerLevelReq
            (var playerlevelreqLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading CostMultiplier
            (var costmultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StoredUses
            (var storedusesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Cooldown
            (var cooldownLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CooldownBypassType
            (var cooldownbypasstypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading VaalSouls
            (var vaalsoulsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading VaalStoredUses
            (var vaalstoredusesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CooldownGroup
            (var cooldowngroupLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SoulGainPreventionDuration
            (var soulgainpreventiondurationLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AttackSpeedMultiplier
            (var attackspeedmultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CostAmounts
            (var tempcostamountsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var costamountsLoading = tempcostamountsLoading.AsReadOnly();

            // loading CostTypes
            (var tempcosttypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var costtypesLoading = tempcosttypesLoading.AsReadOnly();

            // loading ManaReservationFlat
            (var manareservationflatLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ManaReservationPercent
            (var manareservationpercentLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifeReservationFlat
            (var lifereservationflatLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifeReservationPercent
            (var lifereservationpercentLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AttackTime
            (var attacktimeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectsPerLevelDat()
            {
                GrantedEffect = grantedeffectLoading,
                Level = levelLoading,
                PlayerLevelReq = playerlevelreqLoading,
                CostMultiplier = costmultiplierLoading,
                StoredUses = storedusesLoading,
                Cooldown = cooldownLoading,
                CooldownBypassType = cooldownbypasstypeLoading,
                VaalSouls = vaalsoulsLoading,
                VaalStoredUses = vaalstoredusesLoading,
                CooldownGroup = cooldowngroupLoading,
                Unknown52 = unknown52Loading,
                SoulGainPreventionDuration = soulgainpreventiondurationLoading,
                AttackSpeedMultiplier = attackspeedmultiplierLoading,
                Unknown64 = unknown64Loading,
                CostAmounts = costamountsLoading,
                CostTypes = costtypesLoading,
                ManaReservationFlat = manareservationflatLoading,
                ManaReservationPercent = manareservationpercentLoading,
                LifeReservationFlat = lifereservationflatLoading,
                LifeReservationPercent = lifereservationpercentLoading,
                AttackTime = attacktimeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
