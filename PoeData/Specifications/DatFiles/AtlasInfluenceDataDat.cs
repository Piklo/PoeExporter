// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasInfluenceData.dat data.
/// </summary>
public sealed partial class AtlasInfluenceDataDat
{
    /// <summary> Gets InfluencePack.</summary>
    /// <remarks> references <see cref="AtlasInfluenceOutcomesDat"/> on <see cref="Specification.LoadAtlasInfluenceOutcomesDat"/> index.</remarks>
    public required int? InfluencePack { get; init; }

    /// <summary> Gets MonsterPacks.</summary>
    /// <remarks> references <see cref="MonsterPacksDat"/> on <see cref="Specification.LoadMonsterPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterPacks { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown48 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required ReadOnlyCollection<int> Unknown68 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required ReadOnlyCollection<int> Unknown84 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required int Unknown100 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required int Unknown104 { get; init; }

    /// <summary>
    /// Gets AtlasInfluenceDataDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AtlasInfluenceDataDat.</returns>
    internal static AtlasInfluenceDataDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AtlasInfluenceData.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasInfluenceDataDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading InfluencePack
            (var influencepackLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterPacks
            (var tempmonsterpacksLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterpacksLoading = tempmonsterpacksLoading.AsReadOnly();

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var tempunknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown48Loading = tempunknown48Loading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var tempunknown68Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown68Loading = tempunknown68Loading.AsReadOnly();

            // loading Unknown84
            (var tempunknown84Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown84Loading = tempunknown84Loading.AsReadOnly();

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasInfluenceDataDat()
            {
                InfluencePack = influencepackLoading,
                MonsterPacks = monsterpacksLoading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown84 = unknown84Loading,
                Unknown100 = unknown100Loading,
                Unknown104 = unknown104Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
