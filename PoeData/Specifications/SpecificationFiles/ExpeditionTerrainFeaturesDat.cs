// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing ExpeditionTerrainFeatures.dat data.
/// </summary>
public sealed partial class ExpeditionTerrainFeaturesDat : ISpecificationFile<ExpeditionTerrainFeaturesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ExtraFeature.</summary>
    public required int? ExtraFeature { get; init; }

    /// <summary> Gets ExpeditionFaction.</summary>
    public required int? ExpeditionFaction { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Area.</summary>
    public required int? Area { get; init; }

    /// <summary> Gets ExpeditionAreas.</summary>
    public required ReadOnlyCollection<int> ExpeditionAreas { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required int Unknown84 { get; init; }

    /// <summary> Gets a value indicating whether Unknown88 is set.</summary>
    public required bool Unknown88 { get; init; }

    /// <summary> Gets UnearthAchievements.</summary>
    public required ReadOnlyCollection<int> UnearthAchievements { get; init; }

    /// <inheritdoc/>
    public static ExpeditionTerrainFeaturesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ExpeditionTerrainFeatures.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionTerrainFeaturesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetExtraTerrainFeaturesDat();
            // specification.GetExpeditionFactionsDat();
            // specification.GetWorldAreasDat();
            // specification.GetExpeditionAreasDat();
            // specification.GetAchievementItemsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ExtraFeature
            (var extrafeatureLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ExpeditionFaction
            (var expeditionfactionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Area
            (var areaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ExpeditionAreas
            (var tempexpeditionareasLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var expeditionareasLoading = tempexpeditionareasLoading.AsReadOnly();

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading UnearthAchievements
            (var tempunearthachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unearthachievementsLoading = tempunearthachievementsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionTerrainFeaturesDat()
            {
                Id = idLoading,
                ExtraFeature = extrafeatureLoading,
                ExpeditionFaction = expeditionfactionLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                Unknown48 = unknown48Loading,
                Area = areaLoading,
                ExpeditionAreas = expeditionareasLoading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                UnearthAchievements = unearthachievementsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
