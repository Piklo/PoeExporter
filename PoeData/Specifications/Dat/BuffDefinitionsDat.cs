// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BuffDefinitions.dat data.
/// </summary>
public sealed partial class BuffDefinitionsDat : ISpecificationFile<BuffDefinitionsDat>
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
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown42 is set.</summary>
    public required bool Unknown42 { get; init; }

    /// <summary> Gets Unknown43.</summary>
    public required int Unknown43 { get; init; }

    /// <summary> Gets a value indicating whether Unknown47 is set.</summary>
    public required bool Unknown47 { get; init; }

    /// <summary> Gets Maximum_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Maximum_StatsKey { get; init; }

    /// <summary> Gets Current_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Current_StatsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown80 is set.</summary>
    public required bool Unknown80 { get; init; }

    /// <summary> Gets Unknown81.</summary>
    public required int Unknown81 { get; init; }

    /// <summary> Gets BuffVisualsKey.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.GetBuffVisualsDat"/> index.</remarks>
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

    /// <summary> Gets a value indicating whether Unknown126 is set.</summary>
    public required bool Unknown126 { get; init; }

    /// <summary> Gets Unknown127.</summary>
    public required int? Unknown127 { get; init; }

    /// <summary> Gets a value indicating whether Unknown143 is set.</summary>
    public required bool Unknown143 { get; init; }

    /// <summary> Gets Unknown144.</summary>
    public required int Unknown144 { get; init; }

    /// <summary> Gets a value indicating whether Unknown148 is set.</summary>
    public required bool Unknown148 { get; init; }

    /// <summary> Gets a value indicating whether Unknown149 is set.</summary>
    public required bool Unknown149 { get; init; }

    /// <summary> Gets Unknown150.</summary>
    public required int Unknown150 { get; init; }

    /// <summary> Gets Unknown154.</summary>
    public required ReadOnlyCollection<int> Unknown154 { get; init; }

    /// <summary> Gets a value indicating whether Unknown170 is set.</summary>
    public required bool Unknown170 { get; init; }

    /// <summary> Gets a value indicating whether Unknown171 is set.</summary>
    public required bool Unknown171 { get; init; }

    /// <summary> Gets Unknown172.</summary>
    public required ReadOnlyCollection<int> Unknown172 { get; init; }

    /// <summary> Gets a value indicating whether Unknown188 is set.</summary>
    public required bool Unknown188 { get; init; }

    /// <summary> Gets Unknown189.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown189 { get; init; }

    /// <summary> Gets Unknown205.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown205 { get; init; }

    /// <summary> Gets Unknown221.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown221 { get; init; }

    /// <summary> Gets Unknown237.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown237 { get; init; }

    /// <summary> Gets a value indicating whether Unknown253 is set.</summary>
    public required bool Unknown253 { get; init; }

    /// <summary> Gets a value indicating whether Unknown254 is set.</summary>
    public required bool Unknown254 { get; init; }

    /// <summary> Gets a value indicating whether Unknown255 is set.</summary>
    public required bool Unknown255 { get; init; }

    /// <summary> Gets a value indicating whether Unknown256 is set.</summary>
    public required bool Unknown256 { get; init; }

    /// <summary> Gets a value indicating whether Unknown257 is set.</summary>
    public required bool Unknown257 { get; init; }

    /// <summary> Gets Unknown258.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown258 { get; init; }

    /// <summary> Gets Unknown274.</summary>
    public required ReadOnlyCollection<int> Unknown274 { get; init; }

    /// <summary> Gets Unknown290.</summary>
    public required string Unknown290 { get; init; }

    /// <inheritdoc/>
    public static BuffDefinitionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BuffDefinitions.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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
            (var maximum_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Current_StatsKey
            (var current_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown81
            (var unknown81Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffVisualsKey
            (var buffvisualskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

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
            (var unknown126Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown127
            (var unknown127Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown143
            (var unknown143Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown150
            (var unknown150Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown154
            (var tempunknown154Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown154Loading = tempunknown154Loading.AsReadOnly();

            // loading Unknown170
            (var unknown170Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown171
            (var unknown171Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown172
            (var tempunknown172Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown172Loading = tempunknown172Loading.AsReadOnly();

            // loading Unknown188
            (var unknown188Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown189
            (var tempunknown189Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown189Loading = tempunknown189Loading.AsReadOnly();

            // loading Unknown205
            (var tempunknown205Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown205Loading = tempunknown205Loading.AsReadOnly();

            // loading Unknown221
            (var tempunknown221Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown221Loading = tempunknown221Loading.AsReadOnly();

            // loading Unknown237
            (var tempunknown237Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown237Loading = tempunknown237Loading.AsReadOnly();

            // loading Unknown253
            (var unknown253Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown254
            (var unknown254Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown255
            (var unknown255Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown256
            (var unknown256Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown257
            (var unknown257Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown258
            (var unknown258Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown274
            (var tempunknown274Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown274Loading = tempunknown274Loading.AsReadOnly();

            // loading Unknown290
            (var unknown290Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
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
                Unknown127 = unknown127Loading,
                Unknown143 = unknown143Loading,
                Unknown144 = unknown144Loading,
                Unknown148 = unknown148Loading,
                Unknown149 = unknown149Loading,
                Unknown150 = unknown150Loading,
                Unknown154 = unknown154Loading,
                Unknown170 = unknown170Loading,
                Unknown171 = unknown171Loading,
                Unknown172 = unknown172Loading,
                Unknown188 = unknown188Loading,
                Unknown189 = unknown189Loading,
                Unknown205 = unknown205Loading,
                Unknown221 = unknown221Loading,
                Unknown237 = unknown237Loading,
                Unknown253 = unknown253Loading,
                Unknown254 = unknown254Loading,
                Unknown255 = unknown255Loading,
                Unknown256 = unknown256Loading,
                Unknown257 = unknown257Loading,
                Unknown258 = unknown258Loading,
                Unknown274 = unknown274Loading,
                Unknown290 = unknown290Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
