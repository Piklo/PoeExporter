﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing LabyrinthSecretEffects.dat data.
/// </summary>
public sealed partial class LabyrinthSecretEffectsDat : ISpecificationFile<LabyrinthSecretEffectsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets Buff_BuffDefinitionsKey.</summary>
    public required int? Buff_BuffDefinitionsKey { get; init; }

    /// <summary> Gets Buff_StatValues.</summary>
    public required ReadOnlyCollection<int> Buff_StatValues { get; init; }

    /// <summary> Gets OTFile.</summary>
    public required string OTFile { get; init; }

    /// <inheritdoc/>
    public static LabyrinthSecretEffectsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/LabyrinthSecretEffects.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthSecretEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetMonsterVarietiesDat();
            // specification.GetBuffDefinitionsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Buff_BuffDefinitionsKey
            (var buff_buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Buff_StatValues
            (var tempbuff_statvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var buff_statvaluesLoading = tempbuff_statvaluesLoading.AsReadOnly();

            // loading OTFile
            (var otfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthSecretEffectsDat()
            {
                Id = idLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                Buff_BuffDefinitionsKey = buff_buffdefinitionskeyLoading,
                Buff_StatValues = buff_statvaluesLoading,
                OTFile = otfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
