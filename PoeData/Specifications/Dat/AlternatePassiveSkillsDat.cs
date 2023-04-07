// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AlternatePassiveSkills.dat data.
/// </summary>
public sealed partial class AlternatePassiveSkillsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets AlternateTreeVersionsKey.</summary>
    /// <remarks> references <see cref="AlternateTreeVersionsDat"/> on <see cref="Specification.GetAlternateTreeVersionsDat"/> index.</remarks>
    public required int? AlternateTreeVersionsKey { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets PassiveType.</summary>
    public required ReadOnlyCollection<int> PassiveType { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets Stat1Min.</summary>
    public required int Stat1Min { get; init; }

    /// <summary> Gets Stat1Max.</summary>
    public required int Stat1Max { get; init; }

    /// <summary> Gets Stat2Min.</summary>
    public required int Stat2Min { get; init; }

    /// <summary> Gets Stat2Max.</summary>
    public required int Stat2Max { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int Unknown80 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required int Unknown84 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int Unknown88 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required int Unknown92 { get; init; }

    /// <summary> Gets Unknown96.</summary>
    public required int Unknown96 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required int Unknown100 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required int Unknown104 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required int Unknown108 { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required int Unknown116 { get; init; }

    /// <summary> Gets RandomMin.</summary>
    public required int RandomMin { get; init; }

    /// <summary> Gets RandomMax.</summary>
    public required int RandomMax { get; init; }

    /// <summary> Gets FlavourText.</summary>
    public required string FlavourText { get; init; }

    /// <summary> Gets DDSIcon.</summary>
    public required string DDSIcon { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets Unknown160.</summary>
    public required int Unknown160 { get; init; }

    /// <summary> Gets Unknown164.</summary>
    public required int Unknown164 { get; init; }

    /// <inheritdoc/>
    public static AlternatePassiveSkillsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/AlternatePassiveSkills.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AlternatePassiveSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AlternateTreeVersionsKey
            (var alternatetreeversionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveType
            (var temppassivetypeLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var passivetypeLoading = temppassivetypeLoading.AsReadOnly();

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading Stat1Min
            (var stat1minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat1Max
            (var stat1maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Min
            (var stat2minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Max
            (var stat2maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RandomMin
            (var randomminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RandomMax
            (var randommaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DDSIcon
            (var ddsiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading Unknown160
            (var unknown160Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AlternatePassiveSkillsDat()
            {
                Id = idLoading,
                AlternateTreeVersionsKey = alternatetreeversionskeyLoading,
                Name = nameLoading,
                PassiveType = passivetypeLoading,
                StatsKeys = statskeysLoading,
                Stat1Min = stat1minLoading,
                Stat1Max = stat1maxLoading,
                Stat2Min = stat2minLoading,
                Stat2Max = stat2maxLoading,
                Unknown80 = unknown80Loading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown92 = unknown92Loading,
                Unknown96 = unknown96Loading,
                Unknown100 = unknown100Loading,
                Unknown104 = unknown104Loading,
                Unknown108 = unknown108Loading,
                SpawnWeight = spawnweightLoading,
                Unknown116 = unknown116Loading,
                RandomMin = randomminLoading,
                RandomMax = randommaxLoading,
                FlavourText = flavourtextLoading,
                DDSIcon = ddsiconLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                Unknown160 = unknown160Loading,
                Unknown164 = unknown164Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
