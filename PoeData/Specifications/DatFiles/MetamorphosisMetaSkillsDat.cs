// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MetamorphosisMetaSkills.dat data.
/// </summary>
public sealed partial class MetamorphosisMetaSkillsDat
{
    /// <summary> Gets Monster.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? Monster { get; init; }

    /// <summary> Gets SkillType.</summary>
    /// <remarks> references <see cref="MetamorphosisMetaSkillTypesDat"/> on <see cref="Specification.LoadMetamorphosisMetaSkillTypesDat"/> index.</remarks>
    public required int? SkillType { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required ReadOnlyCollection<int> Unknown48 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required ReadOnlyCollection<int> Unknown64 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int? Unknown80 { get; init; }

    /// <summary> Gets Animation.</summary>
    /// <remarks> references <see cref="AnimationDat"/> on <see cref="Specification.LoadAnimationDat"/> index.</remarks>
    public required int? Animation { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets StatsValues.</summary>
    public required ReadOnlyCollection<int> StatsValues { get; init; }

    /// <summary> Gets Unknown144.</summary>
    public required int Unknown144 { get; init; }

    /// <summary> Gets Unknown148.</summary>
    public required int? Unknown148 { get; init; }

    /// <summary> Gets GrantedEffects.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> GrantedEffects { get; init; }

    /// <summary> Gets Unknown180.</summary>
    public required int Unknown180 { get; init; }

    /// <summary> Gets Unknown184.</summary>
    public required int? Unknown184 { get; init; }

    /// <summary> Gets Script1.</summary>
    public required string Script1 { get; init; }

    /// <summary> Gets Script2.</summary>
    public required string Script2 { get; init; }

    /// <summary> Gets Mods.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Mods { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Unknown240.</summary>
    public required int Unknown240 { get; init; }

    /// <summary> Gets Unknown244.</summary>
    public required ReadOnlyCollection<int> Unknown244 { get; init; }

    /// <summary> Gets Unknown260.</summary>
    public required int Unknown260 { get; init; }

    /// <summary> Gets Unknown264.</summary>
    public required int Unknown264 { get; init; }

    /// <summary> Gets Unknown268.</summary>
    public required ReadOnlyCollection<int> Unknown268 { get; init; }

    /// <summary> Gets Unknown284.</summary>
    public required ReadOnlyCollection<int> Unknown284 { get; init; }

    /// <summary> Gets Unknown300.</summary>
    public required ReadOnlyCollection<int> Unknown300 { get; init; }

    /// <summary> Gets Unknown316.</summary>
    public required ReadOnlyCollection<int> Unknown316 { get; init; }

    /// <summary> Gets MiscAnimations.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscAnimations { get; init; }

    /// <summary> Gets a value indicating whether Unknown348 is set.</summary>
    public required bool Unknown348 { get; init; }

    /// <summary>
    /// Gets MetamorphosisMetaSkillsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MetamorphosisMetaSkillsDat.</returns>
    internal static MetamorphosisMetaSkillsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MetamorphosisMetaSkills.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MetamorphosisMetaSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Monster
            (var monsterLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SkillType
            (var skilltypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var tempunknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown48Loading = tempunknown48Loading.AsReadOnly();

            // loading Unknown64
            (var tempunknown64Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown64Loading = tempunknown64Loading.AsReadOnly();

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Animation
            (var animationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading StatsValues
            (var tempstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statsvaluesLoading = tempstatsvaluesLoading.AsReadOnly();

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading GrantedEffects
            (var tempgrantedeffectsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var grantedeffectsLoading = tempgrantedeffectsLoading.AsReadOnly();

            // loading Unknown180
            (var unknown180Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown184
            (var unknown184Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Script1
            (var script1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Script2
            (var script2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Mods
            (var tempmodsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modsLoading = tempmodsLoading.AsReadOnly();

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown240
            (var unknown240Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown244
            (var tempunknown244Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown244Loading = tempunknown244Loading.AsReadOnly();

            // loading Unknown260
            (var unknown260Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown264
            (var unknown264Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown268
            (var tempunknown268Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown268Loading = tempunknown268Loading.AsReadOnly();

            // loading Unknown284
            (var tempunknown284Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown284Loading = tempunknown284Loading.AsReadOnly();

            // loading Unknown300
            (var tempunknown300Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown300Loading = tempunknown300Loading.AsReadOnly();

            // loading Unknown316
            (var tempunknown316Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown316Loading = tempunknown316Loading.AsReadOnly();

            // loading MiscAnimations
            (var tempmiscanimationsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var miscanimationsLoading = tempmiscanimationsLoading.AsReadOnly();

            // loading Unknown348
            (var unknown348Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MetamorphosisMetaSkillsDat()
            {
                Monster = monsterLoading,
                SkillType = skilltypeLoading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Unknown80 = unknown80Loading,
                Animation = animationLoading,
                Stats = statsLoading,
                StatsValues = statsvaluesLoading,
                Unknown144 = unknown144Loading,
                Unknown148 = unknown148Loading,
                GrantedEffects = grantedeffectsLoading,
                Unknown180 = unknown180Loading,
                Unknown184 = unknown184Loading,
                Script1 = script1Loading,
                Script2 = script2Loading,
                Mods = modsLoading,
                Name = nameLoading,
                Unknown240 = unknown240Loading,
                Unknown244 = unknown244Loading,
                Unknown260 = unknown260Loading,
                Unknown264 = unknown264Loading,
                Unknown268 = unknown268Loading,
                Unknown284 = unknown284Loading,
                Unknown300 = unknown300Loading,
                Unknown316 = unknown316Loading,
                MiscAnimations = miscanimationsLoading,
                Unknown348 = unknown348Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
