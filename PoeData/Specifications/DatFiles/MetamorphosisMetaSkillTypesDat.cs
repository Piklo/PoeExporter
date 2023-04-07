// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MetamorphosisMetaSkillTypes.dat data.
/// </summary>
public sealed partial class MetamorphosisMetaSkillTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets UnavailableArt.</summary>
    public required string UnavailableArt { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required string Unknown32 { get; init; }

    /// <summary> Gets AvailableArt.</summary>
    public required string AvailableArt { get; init; }

    /// <summary> Gets ItemisedSample.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? ItemisedSample { get; init; }

    /// <summary> Gets BodypartName.</summary>
    public required string BodypartName { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets BodypartNamePlural.</summary>
    public required string BodypartNamePlural { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required int Unknown100 { get; init; }

    /// <summary>
    /// Gets MetamorphosisMetaSkillTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MetamorphosisMetaSkillTypesDat.</returns>
    internal static MetamorphosisMetaSkillTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MetamorphosisMetaSkillTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MetamorphosisMetaSkillTypesDat[tableRows];
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

            // loading UnavailableArt
            (var unavailableartLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AvailableArt
            (var availableartLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ItemisedSample
            (var itemisedsampleLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BodypartName
            (var bodypartnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading BodypartNamePlural
            (var bodypartnamepluralLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MetamorphosisMetaSkillTypesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Description = descriptionLoading,
                UnavailableArt = unavailableartLoading,
                Unknown32 = unknown32Loading,
                AvailableArt = availableartLoading,
                ItemisedSample = itemisedsampleLoading,
                BodypartName = bodypartnameLoading,
                Unknown72 = unknown72Loading,
                AchievementItemsKeys = achievementitemskeysLoading,
                BodypartNamePlural = bodypartnamepluralLoading,
                Unknown100 = unknown100Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
