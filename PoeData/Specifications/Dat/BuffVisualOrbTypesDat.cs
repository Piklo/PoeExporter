﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BuffVisualOrbTypes.dat data.
/// </summary>
public sealed partial class BuffVisualOrbTypesDat : ISpecificationFile<BuffVisualOrbTypesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required float Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required float Unknown12 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required float Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required float Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets a value indicating whether Unknown28 is set.</summary>
    public required bool Unknown28 { get; init; }

    /// <summary> Gets Unknown29.</summary>
    public required int? Unknown29 { get; init; }

    /// <summary> Gets Unknown45.</summary>
    public required int? Unknown45 { get; init; }

    /// <summary> Gets Unknown61.</summary>
    public required int Unknown61 { get; init; }

    /// <summary> Gets a value indicating whether Unknown65 is set.</summary>
    public required bool Unknown65 { get; init; }

    /// <summary> Gets Unknown66.</summary>
    public required float Unknown66 { get; init; }

    /// <inheritdoc/>
    public static BuffVisualOrbTypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BuffVisualOrbTypes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BuffVisualOrbTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BuffVisualOrbTypesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown29 = unknown29Loading,
                Unknown45 = unknown45Loading,
                Unknown61 = unknown61Loading,
                Unknown65 = unknown65Loading,
                Unknown66 = unknown66Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}