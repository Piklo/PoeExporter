// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing WeaponTypes.dat data.
/// </summary>
public sealed partial class WeaponTypesDat
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

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary>
    /// Gets WeaponTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of WeaponTypesDat.</returns>
    internal static WeaponTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/WeaponTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WeaponTypesDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Critical = criticalLoading,
                Speed = speedLoading,
                DamageMin = damageminLoading,
                DamageMax = damagemaxLoading,
                RangeMax = rangemaxLoading,
                Unknown36 = unknown36Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
