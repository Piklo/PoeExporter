﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BlightedSporeAuras.dat data.
/// </summary>
public sealed partial class BlightedSporeAurasDat
{
    /// <summary> Gets BuffDefinitionsKey.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.GetBuffDefinitionsDat"/> index.</remarks>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required ReadOnlyCollection<int> Unknown16 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required ReadOnlyCollection<int> Unknown36 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary>
    /// Gets BlightedSporeAurasDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BlightedSporeAurasDat.</returns>
    internal static BlightedSporeAurasDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BlightedSporeAuras.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightedSporeAurasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var tempunknown16Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown16Loading = tempunknown16Loading.AsReadOnly();

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var tempunknown36Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown36Loading = tempunknown36Loading.AsReadOnly();

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightedSporeAurasDat()
            {
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}