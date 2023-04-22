// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DelveFeatures.dat data.
/// </summary>
public sealed partial class DelveFeaturesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required ReadOnlyCollection<int> SpawnWeight { get; init; }

    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets Image.</summary>
    public required string Image { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required ReadOnlyCollection<int> Unknown72 { get; init; }

    /// <summary> Gets MinDepth.</summary>
    public required ReadOnlyCollection<int> MinDepth { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required int Unknown112 { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required ReadOnlyCollection<int> Unknown116 { get; init; }

    /// <summary> Gets Unknown132.</summary>
    public required ReadOnlyCollection<int> Unknown132 { get; init; }

    /// <summary> Gets Unknown148.</summary>
    public required ReadOnlyCollection<int> Unknown148 { get; init; }

    /// <summary>
    /// Gets DelveFeaturesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of DelveFeaturesDat.</returns>
    internal static DelveFeaturesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/DelveFeatures.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveFeaturesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnWeight
            (var tempspawnweightLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweightLoading = tempspawnweightLoading.AsReadOnly();

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Image
            (var imageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading Unknown72
            (var tempunknown72Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown72Loading = tempunknown72Loading.AsReadOnly();

            // loading MinDepth
            (var tempmindepthLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var mindepthLoading = tempmindepthLoading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown116
            (var tempunknown116Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown116Loading = tempunknown116Loading.AsReadOnly();

            // loading Unknown132
            (var tempunknown132Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown132Loading = tempunknown132Loading.AsReadOnly();

            // loading Unknown148
            (var tempunknown148Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown148Loading = tempunknown148Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveFeaturesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                SpawnWeight = spawnweightLoading,
                WorldAreasKey = worldareaskeyLoading,
                Image = imageLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                Unknown72 = unknown72Loading,
                MinDepth = mindepthLoading,
                Description = descriptionLoading,
                Unknown112 = unknown112Loading,
                Unknown116 = unknown116Loading,
                Unknown132 = unknown132Loading,
                Unknown148 = unknown148Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
