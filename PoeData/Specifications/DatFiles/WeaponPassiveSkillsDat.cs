// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing WeaponPassiveSkills.dat data.
/// </summary>
public sealed partial class WeaponPassiveSkillsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Mod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? Mod { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required ReadOnlyCollection<int> Unknown28 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required ReadOnlyCollection<int> Unknown44 { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="WeaponPassiveSkillTypesDat"/> on <see cref="Specification.LoadWeaponPassiveSkillTypesDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets Achievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements { get; init; }

    /// <summary>
    /// Gets WeaponPassiveSkillsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of WeaponPassiveSkillsDat.</returns>
    internal static WeaponPassiveSkillsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/WeaponPassiveSkills.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WeaponPassiveSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Mod
            (var modLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown28
            (var tempunknown28Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown28Loading = tempunknown28Loading.AsReadOnly();

            // loading Unknown44
            (var tempunknown44Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown44Loading = tempunknown44Loading.AsReadOnly();

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WeaponPassiveSkillsDat()
            {
                Id = idLoading,
                Tier = tierLoading,
                Mod = modLoading,
                Unknown28 = unknown28Loading,
                Unknown44 = unknown44Loading,
                Icon = iconLoading,
                Type = typeLoading,
                Achievements = achievementsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
