// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BetrayalTargets.dat data.
/// </summary>
public sealed partial class BetrayalTargetsDat : ISpecificationFile<BetrayalTargetsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BetrayalRanksKey.</summary>
    /// <remarks> references <see cref="BetrayalRanksDat"/> on <see cref="Specification.GetBetrayalRanksDat"/> index.</remarks>
    public required int? BetrayalRanksKey { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets BetrayalJobsKey.</summary>
    /// <remarks> references <see cref="BetrayalJobsDat"/> on <see cref="Specification.GetBetrayalJobsDat"/> index.</remarks>
    public required int? BetrayalJobsKey { get; init; }

    /// <summary> Gets Art.</summary>
    public required string Art { get; init; }

    /// <summary> Gets a value indicating whether Unknown64 is set.</summary>
    public required bool Unknown64 { get; init; }

    /// <summary> Gets ItemClasses.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.GetItemClassesDat"/> index.</remarks>
    public required int? ItemClasses { get; init; }

    /// <summary> Gets FullName.</summary>
    public required string FullName { get; init; }

    /// <summary> Gets Safehouse_ARMFile.</summary>
    public required string Safehouse_ARMFile { get; init; }

    /// <summary> Gets ShortName.</summary>
    public required string ShortName { get; init; }

    /// <summary> Gets Unknown105.</summary>
    public required int Unknown105 { get; init; }

    /// <summary> Gets SafehouseLeader_AcheivementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? SafehouseLeader_AcheivementItemsKey { get; init; }

    /// <summary> Gets Level3_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? Level3_AchievementItemsKey { get; init; }

    /// <summary> Gets Unknown141.</summary>
    public required int Unknown141 { get; init; }

    /// <summary> Gets Unknown145.</summary>
    public required int Unknown145 { get; init; }

    /// <summary> Gets Unknown149.</summary>
    public required int Unknown149 { get; init; }

    /// <summary> Gets Unknown153.</summary>
    public required int? Unknown153 { get; init; }

    /// <summary> Gets ScriptArgument.</summary>
    public required string ScriptArgument { get; init; }

    /// <inheritdoc/>
    public static BetrayalTargetsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BetrayalTargets.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalTargetsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BetrayalRanksKey
            (var betrayalrankskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BetrayalJobsKey
            (var betrayaljobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Art
            (var artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ItemClasses
            (var itemclassesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading FullName
            (var fullnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Safehouse_ARMFile
            (var safehouse_armfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShortName
            (var shortnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SafehouseLeader_AcheivementItemsKey
            (var safehouseleader_acheivementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Level3_AchievementItemsKey
            (var level3_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown145
            (var unknown145Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown153
            (var unknown153Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ScriptArgument
            (var scriptargumentLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalTargetsDat()
            {
                Id = idLoading,
                BetrayalRanksKey = betrayalrankskeyLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                BetrayalJobsKey = betrayaljobskeyLoading,
                Art = artLoading,
                Unknown64 = unknown64Loading,
                ItemClasses = itemclassesLoading,
                FullName = fullnameLoading,
                Safehouse_ARMFile = safehouse_armfileLoading,
                ShortName = shortnameLoading,
                Unknown105 = unknown105Loading,
                SafehouseLeader_AcheivementItemsKey = safehouseleader_acheivementitemskeyLoading,
                Level3_AchievementItemsKey = level3_achievementitemskeyLoading,
                Unknown141 = unknown141Loading,
                Unknown145 = unknown145Loading,
                Unknown149 = unknown149Loading,
                Unknown153 = unknown153Loading,
                ScriptArgument = scriptargumentLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
