// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HeistJobs.dat data.
/// </summary>
public sealed partial class HeistJobsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets RequiredSkillIcon.</summary>
    public required string RequiredSkillIcon { get; init; }

    /// <summary> Gets SkillIcon.</summary>
    public required string SkillIcon { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required float Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets MapIcon.</summary>
    public required string MapIcon { get; init; }

    /// <summary> Gets Level_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Level_StatsKey { get; init; }

    /// <summary> Gets Alert_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Alert_StatsKey { get; init; }

    /// <summary> Gets Alarm_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Alarm_StatsKey { get; init; }

    /// <summary> Gets Cost_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Cost_StatsKey { get; init; }

    /// <summary> Gets ExperienceGain_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? ExperienceGain_StatsKey { get; init; }

    /// <summary> Gets ConsoleBlueprintLegend.</summary>
    public required string ConsoleBlueprintLegend { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <inheritdoc/>
    public static HeistJobsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/HeistJobs.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistJobsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RequiredSkillIcon
            (var requiredskilliconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SkillIcon
            (var skilliconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MapIcon
            (var mapiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Level_StatsKey
            (var level_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Alert_StatsKey
            (var alert_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Alarm_StatsKey
            (var alarm_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Cost_StatsKey
            (var cost_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ExperienceGain_StatsKey
            (var experiencegain_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ConsoleBlueprintLegend
            (var consoleblueprintlegendLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistJobsDat()
            {
                Id = idLoading,
                Name = nameLoading,
                RequiredSkillIcon = requiredskilliconLoading,
                SkillIcon = skilliconLoading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                MapIcon = mapiconLoading,
                Level_StatsKey = level_statskeyLoading,
                Alert_StatsKey = alert_statskeyLoading,
                Alarm_StatsKey = alarm_statskeyLoading,
                Cost_StatsKey = cost_statskeyLoading,
                ExperienceGain_StatsKey = experiencegain_statskeyLoading,
                ConsoleBlueprintLegend = consoleblueprintlegendLoading,
                Description = descriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
