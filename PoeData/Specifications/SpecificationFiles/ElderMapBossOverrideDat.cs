// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing ElderMapBossOverride.dat data.
/// </summary>
public sealed partial class ElderMapBossOverrideDat : ISpecificationFile<ElderMapBossOverrideDat>
{
    /// <summary> Gets WorldAreasKey.</summary>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets MonsterVarietiesKeys.</summary>
    public required ReadOnlyCollection<int> MonsterVarietiesKeys { get; init; }

    /// <summary> Gets TerrainMetadata.</summary>
    public required string TerrainMetadata { get; init; }

    /// <inheritdoc/>
    public static ElderMapBossOverrideDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ElderMapBossOverride.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ElderMapBossOverrideDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetWorldAreasDat();
            // specification.GetMonsterVarietiesDat();

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterVarietiesKeys
            (var tempmonstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monstervarietieskeysLoading = tempmonstervarietieskeysLoading.AsReadOnly();

            // loading TerrainMetadata
            (var terrainmetadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ElderMapBossOverrideDat()
            {
                WorldAreasKey = worldareaskeyLoading,
                MonsterVarietiesKeys = monstervarietieskeysLoading,
                TerrainMetadata = terrainmetadataLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
