﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing AddBuffToTargetVarieties.dat data.
/// </summary>
public sealed partial class AddBuffToTargetVarietiesDat : ISpecificationFile<AddBuffToTargetVarietiesDat>
{
    /// <summary> Gets BuffDefinitions.</summary>
    public required int? BuffDefinitions { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required ReadOnlyCollection<int> Unknown16 { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required ReadOnlyCollection<int> Unknown52 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required ReadOnlyCollection<int> Unknown76 { get; init; }

    /// <inheritdoc/>
    public static AddBuffToTargetVarietiesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AddBuffToTargetVarieties.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AddBuffToTargetVarietiesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBuffDefinitionsDat();
            // specification.GetStatsDat();

            // loading BuffDefinitions
            (var buffdefinitionsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var tempunknown16Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown16Loading = tempunknown16Loading.AsReadOnly();

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var tempunknown52Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown52Loading = tempunknown52Loading.AsReadOnly();

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var tempunknown76Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown76Loading = tempunknown76Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AddBuffToTargetVarietiesDat()
            {
                BuffDefinitions = buffdefinitionsLoading,
                Unknown16 = unknown16Loading,
                StatsKeys = statskeysLoading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
