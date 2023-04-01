// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing DelveBiomes.dat data.
/// </summary>
public sealed partial class DelveBiomesDat : ISpecificationFile<DelveBiomesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets WorldAreasKeys.</summary>
    public required ReadOnlyCollection<int> WorldAreasKeys { get; init; }

    /// <summary> Gets UIImage.</summary>
    public required string UIImage { get; init; }

    /// <summary> Gets SpawnWeight_Depth.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Depth { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required ReadOnlyCollection<int> Unknown72 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required ReadOnlyCollection<int> Unknown88 { get; init; }

    /// <summary> Gets Art2D.</summary>
    public required string Art2D { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown128 is set.</summary>
    public required bool Unknown128 { get; init; }

    /// <summary> Gets Unknown129.</summary>
    public required ReadOnlyCollection<int> Unknown129 { get; init; }

    /// <inheritdoc/>
    public static DelveBiomesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/DelveBiomes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveBiomesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetWorldAreasDat();
            // specification.GetAchievementItemsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WorldAreasKeys
            (var tempworldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var worldareaskeysLoading = tempworldareaskeysLoading.AsReadOnly();

            // loading UIImage
            (var uiimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnWeight_Depth
            (var tempspawnweight_depthLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_depthLoading = tempspawnweight_depthLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading Unknown72
            (var tempunknown72Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown72Loading = tempunknown72Loading.AsReadOnly();

            // loading Unknown88
            (var tempunknown88Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown88Loading = tempunknown88Loading.AsReadOnly();

            // loading Art2D
            (var art2dLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading Unknown128
            (var unknown128Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown129
            (var tempunknown129Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown129Loading = tempunknown129Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveBiomesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                WorldAreasKeys = worldareaskeysLoading,
                UIImage = uiimageLoading,
                SpawnWeight_Depth = spawnweight_depthLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                Unknown72 = unknown72Loading,
                Unknown88 = unknown88Loading,
                Art2D = art2dLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                Unknown128 = unknown128Loading,
                Unknown129 = unknown129Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
