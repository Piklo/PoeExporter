// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HarvestCraftOptions.dat data.
/// </summary>
public sealed partial class HarvestCraftOptionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets Command.</summary>
    public required string Command { get; init; }

    /// <summary> Gets Parameters.</summary>
    public required string Parameters { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required ReadOnlyCollection<int> Unknown48 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets a value indicating whether Unknown76 is set.</summary>
    public required bool Unknown76 { get; init; }

    /// <summary> Gets LifeforceType.</summary>
    public required int LifeforceType { get; init; }

    /// <summary> Gets LifeforceCost.</summary>
    public required int LifeforceCost { get; init; }

    /// <summary> Gets SacredCost.</summary>
    public required int SacredCost { get; init; }

    /// <summary> Gets a value indicating whether Unknown89 is set.</summary>
    public required bool Unknown89 { get; init; }

    /// <summary> Gets Achievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required int Unknown106 { get; init; }

    /// <summary>
    /// Gets HarvestCraftOptionsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HarvestCraftOptionsDat.</returns>
    internal static HarvestCraftOptionsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HarvestCraftOptions.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestCraftOptionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Command
            (var commandLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Parameters
            (var parametersLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var tempunknown48Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown48Loading = tempunknown48Loading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading LifeforceType
            (var lifeforcetypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifeforceCost
            (var lifeforcecostLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SacredCost
            (var sacredcostLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown89
            (var unknown89Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestCraftOptionsDat()
            {
                Id = idLoading,
                Text = textLoading,
                Unknown16 = unknown16Loading,
                Command = commandLoading,
                Parameters = parametersLoading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Description = descriptionLoading,
                Unknown76 = unknown76Loading,
                LifeforceType = lifeforcetypeLoading,
                LifeforceCost = lifeforcecostLoading,
                SacredCost = sacredcostLoading,
                Unknown89 = unknown89Loading,
                Achievements = achievementsLoading,
                Unknown106 = unknown106Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
