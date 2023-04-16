﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ComponentCharges.dat data.
/// </summary>
public sealed partial class ComponentChargesDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="BaseItemTypesDat.Id"/>.</remarks>
    public required string BaseItemTypesKey { get; init; }

    /// <summary> Gets MaxCharges.</summary>
    public required int MaxCharges { get; init; }

    /// <summary> Gets PerCharge.</summary>
    public required int PerCharge { get; init; }

    /// <summary> Gets MaxCharges2.</summary>
    public required int MaxCharges2 { get; init; }

    /// <summary> Gets PerCharge2.</summary>
    public required int PerCharge2 { get; init; }

    /// <summary>
    /// Gets ComponentChargesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ComponentChargesDat.</returns>
    internal static ComponentChargesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ComponentCharges.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ComponentChargesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MaxCharges
            (var maxchargesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PerCharge
            (var perchargeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxCharges2
            (var maxcharges2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PerCharge2
            (var percharge2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ComponentChargesDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                MaxCharges = maxchargesLoading,
                PerCharge = perchargeLoading,
                MaxCharges2 = maxcharges2Loading,
                PerCharge2 = percharge2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
