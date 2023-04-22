// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BuffDefinitions.dat data.
/// </summary>
public sealed partial class BuffDefinitionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets a value indicating whether Invisible is set.</summary>
    public required bool Invisible { get; init; }

    /// <summary> Gets a value indicating whether Removable is set.</summary>
    public required bool Removable { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown42 is set.</summary>
    public required bool Unknown42 { get; init; }

    /// <summary> Gets Unknown43.</summary>
    public required int Unknown43 { get; init; }

    /// <summary> Gets a value indicating whether Unknown47 is set.</summary>
    public required bool Unknown47 { get; init; }

    /// <summary> Gets Maximum_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Maximum_StatsKey { get; init; }

    /// <summary> Gets Current_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Current_StatsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown80 is set.</summary>
    public required bool Unknown80 { get; init; }

    /// <summary> Gets Unknown81.</summary>
    public required int Unknown81 { get; init; }

    /// <summary> Gets BuffVisualsKey.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.LoadBuffVisualsDat"/> index.</remarks>
    public required int? BuffVisualsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown101 is set.</summary>
    public required bool Unknown101 { get; init; }

    /// <summary> Gets a value indicating whether Unknown102 is set.</summary>
    public required bool Unknown102 { get; init; }

    /// <summary> Gets Unknown103.</summary>
    public required int Unknown103 { get; init; }

    /// <summary> Gets a value indicating whether Unknown107 is set.</summary>
    public required bool Unknown107 { get; init; }

    /// <summary> Gets a value indicating whether Unknown108 is set.</summary>
    public required bool Unknown108 { get; init; }

    /// <summary> Gets a value indicating whether Unknown109 is set.</summary>
    public required bool Unknown109 { get; init; }

    /// <summary> Gets a value indicating whether Unknown110 is set.</summary>
    public required bool Unknown110 { get; init; }

    /// <summary> Gets BuffLimit.</summary>
    public required int BuffLimit { get; init; }

    /// <summary> Gets a value indicating whether Unknown115 is set.</summary>
    public required bool Unknown115 { get; init; }

    /// <summary> Gets Id2.</summary>
    public required string Id2 { get; init; }

    /// <summary> Gets a value indicating whether IsRecovery is set.</summary>
    public required bool IsRecovery { get; init; }

    /// <summary> Gets a value indicating whether Unknown125 is set.</summary>
    public required bool Unknown125 { get; init; }

    /// <summary> Gets Unknown126.</summary>
    public required int? Unknown126 { get; init; }

    /// <summary> Gets a value indicating whether Unknown142 is set.</summary>
    public required bool Unknown142 { get; init; }

    /// <summary> Gets Unknown143.</summary>
    public required int Unknown143 { get; init; }

    /// <summary> Gets a value indicating whether Unknown147 is set.</summary>
    public required bool Unknown147 { get; init; }

    /// <summary> Gets a value indicating whether Unknown148 is set.</summary>
    public required bool Unknown148 { get; init; }

    /// <summary> Gets Unknown149.</summary>
    public required int Unknown149 { get; init; }

    /// <summary> Gets Unknown153.</summary>
    public required ReadOnlyCollection<int> Unknown153 { get; init; }

    /// <summary> Gets a value indicating whether Unknown169 is set.</summary>
    public required bool Unknown169 { get; init; }

    /// <summary> Gets a value indicating whether Unknown170 is set.</summary>
    public required bool Unknown170 { get; init; }

    /// <summary> Gets Unknown171.</summary>
    public required ReadOnlyCollection<int> Unknown171 { get; init; }

    /// <summary> Gets a value indicating whether Unknown187 is set.</summary>
    public required bool Unknown187 { get; init; }

    /// <summary> Gets Unknown188.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown188 { get; init; }

    /// <summary> Gets Unknown204.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown204 { get; init; }

    /// <summary> Gets Unknown220.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown220 { get; init; }

    /// <summary> Gets Unknown236.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown236 { get; init; }

    /// <summary> Gets a value indicating whether Unknown252 is set.</summary>
    public required bool Unknown252 { get; init; }

    /// <summary> Gets a value indicating whether Unknown253 is set.</summary>
    public required bool Unknown253 { get; init; }

    /// <summary> Gets a value indicating whether Unknown254 is set.</summary>
    public required bool Unknown254 { get; init; }

    /// <summary> Gets a value indicating whether Unknown255 is set.</summary>
    public required bool Unknown255 { get; init; }

    /// <summary> Gets a value indicating whether Unknown256 is set.</summary>
    public required bool Unknown256 { get; init; }

    /// <summary> Gets Unknown257.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Unknown257 { get; init; }

    /// <summary> Gets Unknown273.</summary>
    public required ReadOnlyCollection<int> Unknown273 { get; init; }

    /// <summary> Gets Unknown289.</summary>
    public required string Unknown289 { get; init; }

    /// <summary> Gets Unknown297.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown297 { get; init; }

    /// <summary>
    /// Gets BuffDefinitionsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BuffDefinitionsDat.</returns>
    internal static BuffDefinitionsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BuffDefinitions.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BuffDefinitionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Invisible
            (var invisibleLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Removable
            (var removableLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown43
            (var unknown43Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown47
            (var unknown47Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Maximum_StatsKey
            (var maximum_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Current_StatsKey
            (var current_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown81
            (var unknown81Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffVisualsKey
            (var buffvisualskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown102
            (var unknown102Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown103
            (var unknown103Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown107
            (var unknown107Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown110
            (var unknown110Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading BuffLimit
            (var bufflimitLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Id2
            (var id2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsRecovery
            (var isrecoveryLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown125
            (var unknown125Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown126
            (var unknown126Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown143
            (var unknown143Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown147
            (var unknown147Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown153
            (var tempunknown153Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown153Loading = tempunknown153Loading.AsReadOnly();

            // loading Unknown169
            (var unknown169Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown170
            (var unknown170Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown171
            (var tempunknown171Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown171Loading = tempunknown171Loading.AsReadOnly();

            // loading Unknown187
            (var unknown187Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown188
            (var tempunknown188Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown188Loading = tempunknown188Loading.AsReadOnly();

            // loading Unknown204
            (var tempunknown204Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown204Loading = tempunknown204Loading.AsReadOnly();

            // loading Unknown220
            (var tempunknown220Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown220Loading = tempunknown220Loading.AsReadOnly();

            // loading Unknown236
            (var tempunknown236Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown236Loading = tempunknown236Loading.AsReadOnly();

            // loading Unknown252
            (var unknown252Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown253
            (var unknown253Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown254
            (var unknown254Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown255
            (var unknown255Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown256
            (var unknown256Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown257
            (var unknown257Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown273
            (var tempunknown273Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown273Loading = tempunknown273Loading.AsReadOnly();

            // loading Unknown289
            (var unknown289Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown297
            (var tempunknown297Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown297Loading = tempunknown297Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BuffDefinitionsDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                Invisible = invisibleLoading,
                Removable = removableLoading,
                Name = nameLoading,
                StatsKeys = statskeysLoading,
                Unknown42 = unknown42Loading,
                Unknown43 = unknown43Loading,
                Unknown47 = unknown47Loading,
                Maximum_StatsKey = maximum_statskeyLoading,
                Current_StatsKey = current_statskeyLoading,
                Unknown80 = unknown80Loading,
                Unknown81 = unknown81Loading,
                BuffVisualsKey = buffvisualskeyLoading,
                Unknown101 = unknown101Loading,
                Unknown102 = unknown102Loading,
                Unknown103 = unknown103Loading,
                Unknown107 = unknown107Loading,
                Unknown108 = unknown108Loading,
                Unknown109 = unknown109Loading,
                Unknown110 = unknown110Loading,
                BuffLimit = bufflimitLoading,
                Unknown115 = unknown115Loading,
                Id2 = id2Loading,
                IsRecovery = isrecoveryLoading,
                Unknown125 = unknown125Loading,
                Unknown126 = unknown126Loading,
                Unknown142 = unknown142Loading,
                Unknown143 = unknown143Loading,
                Unknown147 = unknown147Loading,
                Unknown148 = unknown148Loading,
                Unknown149 = unknown149Loading,
                Unknown153 = unknown153Loading,
                Unknown169 = unknown169Loading,
                Unknown170 = unknown170Loading,
                Unknown171 = unknown171Loading,
                Unknown187 = unknown187Loading,
                Unknown188 = unknown188Loading,
                Unknown204 = unknown204Loading,
                Unknown220 = unknown220Loading,
                Unknown236 = unknown236Loading,
                Unknown252 = unknown252Loading,
                Unknown253 = unknown253Loading,
                Unknown254 = unknown254Loading,
                Unknown255 = unknown255Loading,
                Unknown256 = unknown256Loading,
                Unknown257 = unknown257Loading,
                Unknown273 = unknown273Loading,
                Unknown289 = unknown289Loading,
                Unknown297 = unknown297Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
