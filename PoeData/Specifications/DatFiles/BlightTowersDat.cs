// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BlightTowers.dat data.
/// </summary>
public sealed partial class BlightTowersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets NextUpgradeOptions.</summary>
    /// <remarks> references <see cref="BlightTowersDat"/> on <see cref="Specification.GetBlightTowersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NextUpgradeOptions { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Tier.</summary>
    public required string Tier { get; init; }

    /// <summary> Gets Radius.</summary>
    public required int Radius { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets SpendResourceAchievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? SpendResourceAchievement { get; init; }

    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets StatsKeys2.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys2 { get; init; }

    /// <summary> Gets a value indicating whether Unknown132 is set.</summary>
    public required bool Unknown132 { get; init; }

    /// <summary>
    /// Gets BlightTowersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BlightTowersDat.</returns>
    internal static BlightTowersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BlightTowers.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightTowersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NextUpgradeOptions
            (var tempnextupgradeoptionsLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var nextupgradeoptionsLoading = tempnextupgradeoptionsLoading.AsReadOnly();

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Radius
            (var radiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpendResourceAchievement
            (var spendresourceachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatsKeys2
            (var tempstatskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeys2Loading = tempstatskeys2Loading.AsReadOnly();

            // loading Unknown132
            (var unknown132Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightTowersDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Description = descriptionLoading,
                Icon = iconLoading,
                NextUpgradeOptions = nextupgradeoptionsLoading,
                Unknown48 = unknown48Loading,
                Tier = tierLoading,
                Radius = radiusLoading,
                Unknown64 = unknown64Loading,
                SpendResourceAchievement = spendresourceachievementLoading,
                StatsKey = statskeyLoading,
                StatsKeys = statskeysLoading,
                StatsKeys2 = statskeys2Loading,
                Unknown132 = unknown132Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
