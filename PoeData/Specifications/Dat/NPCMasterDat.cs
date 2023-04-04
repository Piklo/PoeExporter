// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing NPCMaster.dat data.
/// </summary>
public sealed partial class NPCMasterDat : IDat<NPCMasterDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown9 is set.</summary>
    public required bool Unknown9 { get; init; }

    /// <summary> Gets Signature_ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? Signature_ModsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown26 is set.</summary>
    public required bool Unknown26 { get; init; }

    /// <summary> Gets SpawnWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_TagsKeys { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <summary> Gets Unknown59.</summary>
    public required ReadOnlyCollection<int> Unknown59 { get; init; }

    /// <summary> Gets Unknown75.</summary>
    public required ReadOnlyCollection<int> Unknown75 { get; init; }

    /// <summary> Gets Unknown91.</summary>
    public required int? Unknown91 { get; init; }

    /// <summary> Gets Unknown107.</summary>
    public required int Unknown107 { get; init; }

    /// <summary> Gets AreaDescription.</summary>
    public required string AreaDescription { get; init; }

    /// <summary> Gets Unknown119.</summary>
    public required int? Unknown119 { get; init; }

    /// <summary> Gets Unknown135.</summary>
    public required int Unknown135 { get; init; }

    /// <summary> Gets Unknown139.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown139 { get; init; }

    /// <summary> Gets a value indicating whether HasAreaMissions is set.</summary>
    public required bool HasAreaMissions { get; init; }

    /// <summary> Gets Unknown156.</summary>
    public required ReadOnlyCollection<int> Unknown156 { get; init; }

    /// <summary> Gets Unknown172.</summary>
    public required ReadOnlyCollection<int> Unknown172 { get; init; }

    /// <summary> Gets Unknown188.</summary>
    public required int? Unknown188 { get; init; }

    /// <inheritdoc/>
    public static NPCMasterDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/NPCMaster.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCMasterDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown9
            (var unknown9Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Signature_ModsKey
            (var signature_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown26
            (var unknown26Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading Unknown59
            (var tempunknown59Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown59Loading = tempunknown59Loading.AsReadOnly();

            // loading Unknown75
            (var tempunknown75Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown75Loading = tempunknown75Loading.AsReadOnly();

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown107
            (var unknown107Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AreaDescription
            (var areadescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown119
            (var unknown119Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown135
            (var unknown135Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown139
            (var unknown139Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HasAreaMissions
            (var hasareamissionsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown156
            (var tempunknown156Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown156Loading = tempunknown156Loading.AsReadOnly();

            // loading Unknown172
            (var tempunknown172Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown172Loading = tempunknown172Loading.AsReadOnly();

            // loading Unknown188
            (var unknown188Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCMasterDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown9 = unknown9Loading,
                Signature_ModsKey = signature_modskeyLoading,
                Unknown26 = unknown26Loading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                Unknown59 = unknown59Loading,
                Unknown75 = unknown75Loading,
                Unknown91 = unknown91Loading,
                Unknown107 = unknown107Loading,
                AreaDescription = areadescriptionLoading,
                Unknown119 = unknown119Loading,
                Unknown135 = unknown135Loading,
                Unknown139 = unknown139Loading,
                HasAreaMissions = hasareamissionsLoading,
                Unknown156 = unknown156Loading,
                Unknown172 = unknown172Loading,
                Unknown188 = unknown188Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
