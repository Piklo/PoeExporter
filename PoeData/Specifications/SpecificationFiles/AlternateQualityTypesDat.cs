﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing AlternateQualityTypes.dat data.
/// </summary>
public sealed partial class AlternateQualityTypesDat : ISpecificationFile<AlternateQualityTypesDat>
{
    /// <summary> Gets StatsKey.</summary>
    public required int? StatsKey { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets BaseItemTypesKey.</summary>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets ModsKey.</summary>
    public required int? ModsKey { get; init; }

    /// <inheritdoc/>
    public static AlternateQualityTypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AlternateQualityTypes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AlternateQualityTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetStatsDat();
            // specification.GetBaseItemTypesDat();
            // specification.GetModsDat();

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AlternateQualityTypesDat()
            {
                StatsKey = statskeyLoading,
                Description = descriptionLoading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
                ModsKey = modskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}