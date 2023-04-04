// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AtlasBaseTypeDrops.dat data.
/// </summary>
public sealed partial class AtlasBaseTypeDropsDat : ISpecificationFile<AtlasBaseTypeDropsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets AtlasRegionsKey.</summary>
    /// <remarks> references <see cref="AtlasRegionsDat"/> on <see cref="Specification.GetAtlasRegionsDat"/> index.</remarks>
    public required int? AtlasRegionsKey { get; init; }

    /// <summary> Gets MinTier.</summary>
    public required int MinTier { get; init; }

    /// <summary> Gets MaxTier.</summary>
    public required int MaxTier { get; init; }

    /// <summary> Gets SpawnWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_TagsKeys { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <inheritdoc/>
    public static AtlasBaseTypeDropsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AtlasBaseTypeDrops.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasBaseTypeDropsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AtlasRegionsKey
            (var atlasregionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MinTier
            (var mintierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxTier
            (var maxtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasBaseTypeDropsDat()
            {
                Id = idLoading,
                AtlasRegionsKey = atlasregionskeyLoading,
                MinTier = mintierLoading,
                MaxTier = maxtierLoading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
