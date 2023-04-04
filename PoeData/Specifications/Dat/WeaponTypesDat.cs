// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing WeaponTypes.dat data.
/// </summary>
public sealed partial class WeaponTypesDat : ISpecificationFile<WeaponTypesDat>
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Critical.</summary>
    public required int Critical { get; init; }

    /// <summary> Gets Speed.</summary>
    public required int Speed { get; init; }

    /// <summary> Gets DamageMin.</summary>
    public required int DamageMin { get; init; }

    /// <summary> Gets DamageMax.</summary>
    public required int DamageMax { get; init; }

    /// <summary> Gets RangeMax.</summary>
    public required int RangeMax { get; init; }

    /// <summary> Gets Null6.</summary>
    public required int Null6 { get; init; }

    /// <inheritdoc/>
    public static WeaponTypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/WeaponTypes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WeaponTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Critical
            (var criticalLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Speed
            (var speedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DamageMin
            (var damageminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DamageMax
            (var damagemaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RangeMax
            (var rangemaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Null6
            (var null6Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WeaponTypesDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Critical = criticalLoading,
                Speed = speedLoading,
                DamageMin = damageminLoading,
                DamageMax = damagemaxLoading,
                RangeMax = rangemaxLoading,
                Null6 = null6Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
