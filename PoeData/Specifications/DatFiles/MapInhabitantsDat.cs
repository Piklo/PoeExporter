// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapInhabitants.dat data.
/// </summary>
public sealed partial class MapInhabitantsDat
{
    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }

    /// <summary> Gets MonsterPacksKeys.</summary>
    /// <remarks> references <see cref="MonsterPacksDat"/> on <see cref="Specification.GetMonsterPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterPacksKeys { get; init; }

    /// <summary>
    /// Gets MapInhabitantsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MapInhabitantsDat.</returns>
    internal static MapInhabitantsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MapInhabitants.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapInhabitantsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterPacksKeys
            (var tempmonsterpackskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterpackskeysLoading = tempmonsterpackskeysLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapInhabitantsDat()
            {
                StatsKey = statskeyLoading,
                MonsterPacksKeys = monsterpackskeysLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
