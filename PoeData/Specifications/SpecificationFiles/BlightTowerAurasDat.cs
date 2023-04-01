﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing BlightTowerAuras.dat data.
/// </summary>
public sealed partial class BlightTowerAurasDat : ISpecificationFile<BlightTowerAurasDat>
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets BuffDefinitionsKey.</summary>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets MiscAnimatedKey.</summary>
    public required int? MiscAnimatedKey { get; init; }

    /// <inheritdoc/>
    public static BlightTowerAurasDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BlightTowerAuras.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightTowerAurasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBuffDefinitionsDat();
            // specification.GetMiscAnimatedDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscAnimatedKey
            (var miscanimatedkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightTowerAurasDat()
            {
                Id = idLoading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                Unknown20 = unknown20Loading,
                MiscAnimatedKey = miscanimatedkeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}