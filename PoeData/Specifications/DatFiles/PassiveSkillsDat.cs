// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveSkills.dat data.
/// </summary>
public sealed partial class PassiveSkillsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Icon_DDSFile.</summary>
    public required string Icon_DDSFile { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets Stat1Value.</summary>
    public required int Stat1Value { get; init; }

    /// <summary> Gets Stat2Value.</summary>
    public required int Stat2Value { get; init; }

    /// <summary> Gets Stat3Value.</summary>
    public required int Stat3Value { get; init; }

    /// <summary> Gets Stat4Value.</summary>
    public required int Stat4Value { get; init; }

    /// <summary> Gets PassiveSkillGraphId.</summary>
    public required int PassiveSkillGraphId { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Characters.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Characters { get; init; }

    /// <summary> Gets a value indicating whether IsKeystone is set.</summary>
    public required bool IsKeystone { get; init; }

    /// <summary> Gets a value indicating whether IsNotable is set.</summary>
    public required bool IsNotable { get; init; }

    /// <summary> Gets FlavourText.</summary>
    public required string FlavourText { get; init; }

    /// <summary> Gets a value indicating whether IsJustIcon is set.</summary>
    public required bool IsJustIcon { get; init; }

    /// <summary> Gets AchievementItem.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItem { get; init; }

    /// <summary> Gets a value indicating whether IsJewelSocket is set.</summary>
    public required bool IsJewelSocket { get; init; }

    /// <summary> Gets AscendancyKey.</summary>
    /// <remarks> references <see cref="AscendancyDat"/> on <see cref="Specification.LoadAscendancyDat"/> index.</remarks>
    public required int? AscendancyKey { get; init; }

    /// <summary> Gets a value indicating whether IsAscendancyStartingNode is set.</summary>
    public required bool IsAscendancyStartingNode { get; init; }

    /// <summary> Gets ReminderStrings.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ReminderStrings { get; init; }

    /// <summary> Gets SkillPointsGranted.</summary>
    public required int SkillPointsGranted { get; init; }

    /// <summary> Gets a value indicating whether IsMultipleChoice is set.</summary>
    public required bool IsMultipleChoice { get; init; }

    /// <summary> Gets a value indicating whether IsMultipleChoiceOption is set.</summary>
    public required bool IsMultipleChoiceOption { get; init; }

    /// <summary> Gets Stat5Value.</summary>
    public required int Stat5Value { get; init; }

    /// <summary> Gets PassiveSkillBuffs.</summary>
    /// <remarks> references <see cref="BuffTemplatesDat"/> on <see cref="Specification.LoadBuffTemplatesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PassiveSkillBuffs { get; init; }

    /// <summary> Gets GrantedEffectsPerLevel.</summary>
    /// <remarks> references <see cref="GrantedEffectsPerLevelDat"/> on <see cref="Specification.LoadGrantedEffectsPerLevelDat"/> index.</remarks>
    public required int? GrantedEffectsPerLevel { get; init; }

    /// <summary> Gets a value indicating whether IsAnointmentOnly is set.</summary>
    public required bool IsAnointmentOnly { get; init; }

    /// <summary> Gets Unknown180.</summary>
    public required int Unknown180 { get; init; }

    /// <summary> Gets a value indicating whether IsExpansion is set.</summary>
    public required bool IsExpansion { get; init; }

    /// <summary> Gets a value indicating whether IsProxyPassive is set.</summary>
    public required bool IsProxyPassive { get; init; }

    /// <summary> Gets SkillType.</summary>
    /// <remarks> references <see cref="PassiveSkillTypesDat"/> on <see cref="Specification.LoadPassiveSkillTypesDat"/> index.</remarks>
    public required int SkillType { get; init; }

    /// <summary> Gets MasteryGroup.</summary>
    /// <remarks> references <see cref="PassiveSkillMasteryGroupsDat"/> on <see cref="Specification.LoadPassiveSkillMasteryGroupsDat"/> index.</remarks>
    public required int? MasteryGroup { get; init; }

    /// <summary> Gets Unknown206.</summary>
    public required int? Unknown206 { get; init; }

    /// <summary> Gets SoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.LoadSoundEffectsDat"/> index.</remarks>
    public required int? SoundEffect { get; init; }

    /// <summary> Gets Unknown238.</summary>
    public required string Unknown238 { get; init; }

    /// <summary> Gets Unknown246.</summary>
    public required int Unknown246 { get; init; }

    /// <summary> Gets Unknown250.</summary>
    public required int Unknown250 { get; init; }

    /// <summary> Gets Unknown254.</summary>
    public required int Unknown254 { get; init; }

    /// <summary> Gets Unknown258.</summary>
    public required int Unknown258 { get; init; }

    /// <summary> Gets Unknown262.</summary>
    public required int Unknown262 { get; init; }

    /// <summary> Gets a value indicating whether Unknown266 is set.</summary>
    public required bool Unknown266 { get; init; }

    /// <summary> Gets Unknown267.</summary>
    public required ReadOnlyCollection<int> Unknown267 { get; init; }

    /// <summary> Gets Unknown283.</summary>
    public required int Unknown283 { get; init; }

    /// <summary> Gets Unknown287.</summary>
    public required ReadOnlyCollection<int> Unknown287 { get; init; }

    /// <summary> Gets a value indicating whether Unknown303 is set.</summary>
    public required bool Unknown303 { get; init; }

    /// <summary>
    /// Gets PassiveSkillsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PassiveSkillsDat.</returns>
    internal static PassiveSkillsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PassiveSkills.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon_DDSFile
            (var icon_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading Stat1Value
            (var stat1valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Value
            (var stat2valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat3Value
            (var stat3valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat4Value
            (var stat4valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PassiveSkillGraphId
            (var passiveskillgraphidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Characters
            (var tempcharactersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var charactersLoading = tempcharactersLoading.AsReadOnly();

            // loading IsKeystone
            (var iskeystoneLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsNotable
            (var isnotableLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsJustIcon
            (var isjusticonLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AchievementItem
            (var achievementitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsJewelSocket
            (var isjewelsocketLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AscendancyKey
            (var ascendancykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsAscendancyStartingNode
            (var isascendancystartingnodeLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ReminderStrings
            (var tempreminderstringsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var reminderstringsLoading = tempreminderstringsLoading.AsReadOnly();

            // loading SkillPointsGranted
            (var skillpointsgrantedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsMultipleChoice
            (var ismultiplechoiceLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsMultipleChoiceOption
            (var ismultiplechoiceoptionLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Stat5Value
            (var stat5valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PassiveSkillBuffs
            (var temppassiveskillbuffsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var passiveskillbuffsLoading = temppassiveskillbuffsLoading.AsReadOnly();

            // loading GrantedEffectsPerLevel
            (var grantedeffectsperlevelLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsAnointmentOnly
            (var isanointmentonlyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown180
            (var unknown180Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsExpansion
            (var isexpansionLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsProxyPassive
            (var isproxypassiveLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SkillType
            (var skilltypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MasteryGroup
            (var masterygroupLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown206
            (var unknown206Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SoundEffect
            (var soundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown238
            (var unknown238Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown246
            (var unknown246Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown250
            (var unknown250Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown254
            (var unknown254Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown258
            (var unknown258Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown262
            (var unknown262Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown266
            (var unknown266Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown267
            (var tempunknown267Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown267Loading = tempunknown267Loading.AsReadOnly();

            // loading Unknown283
            (var unknown283Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown287
            (var tempunknown287Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown287Loading = tempunknown287Loading.AsReadOnly();

            // loading Unknown303
            (var unknown303Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillsDat()
            {
                Id = idLoading,
                Icon_DDSFile = icon_ddsfileLoading,
                Stats = statsLoading,
                Stat1Value = stat1valueLoading,
                Stat2Value = stat2valueLoading,
                Stat3Value = stat3valueLoading,
                Stat4Value = stat4valueLoading,
                PassiveSkillGraphId = passiveskillgraphidLoading,
                Name = nameLoading,
                Characters = charactersLoading,
                IsKeystone = iskeystoneLoading,
                IsNotable = isnotableLoading,
                FlavourText = flavourtextLoading,
                IsJustIcon = isjusticonLoading,
                AchievementItem = achievementitemLoading,
                IsJewelSocket = isjewelsocketLoading,
                AscendancyKey = ascendancykeyLoading,
                IsAscendancyStartingNode = isascendancystartingnodeLoading,
                ReminderStrings = reminderstringsLoading,
                SkillPointsGranted = skillpointsgrantedLoading,
                IsMultipleChoice = ismultiplechoiceLoading,
                IsMultipleChoiceOption = ismultiplechoiceoptionLoading,
                Stat5Value = stat5valueLoading,
                PassiveSkillBuffs = passiveskillbuffsLoading,
                GrantedEffectsPerLevel = grantedeffectsperlevelLoading,
                IsAnointmentOnly = isanointmentonlyLoading,
                Unknown180 = unknown180Loading,
                IsExpansion = isexpansionLoading,
                IsProxyPassive = isproxypassiveLoading,
                SkillType = skilltypeLoading,
                MasteryGroup = masterygroupLoading,
                Unknown206 = unknown206Loading,
                SoundEffect = soundeffectLoading,
                Unknown238 = unknown238Loading,
                Unknown246 = unknown246Loading,
                Unknown250 = unknown250Loading,
                Unknown254 = unknown254Loading,
                Unknown258 = unknown258Loading,
                Unknown262 = unknown262Loading,
                Unknown266 = unknown266Loading,
                Unknown267 = unknown267Loading,
                Unknown283 = unknown283Loading,
                Unknown287 = unknown287Loading,
                Unknown303 = unknown303Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
