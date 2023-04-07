﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ItemVisualEffect.dat data.
/// </summary>
public sealed partial class ItemVisualEffectDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets DaggerEPKFile.</summary>
    public required string DaggerEPKFile { get; init; }

    /// <summary> Gets BowEPKFile.</summary>
    public required string BowEPKFile { get; init; }

    /// <summary> Gets OneHandedMaceEPKFile.</summary>
    public required string OneHandedMaceEPKFile { get; init; }

    /// <summary> Gets OneHandedSwordEPKFile.</summary>
    public required string OneHandedSwordEPKFile { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required string Unknown40 { get; init; }

    /// <summary> Gets TwoHandedSwordEPKFile.</summary>
    public required string TwoHandedSwordEPKFile { get; init; }

    /// <summary> Gets TwoHandedStaffEPKFile.</summary>
    public required string TwoHandedStaffEPKFile { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets TwoHandedMaceEPKFile.</summary>
    public required string TwoHandedMaceEPKFile { get; init; }

    /// <summary> Gets OneHandedAxeEPKFile.</summary>
    public required string OneHandedAxeEPKFile { get; init; }

    /// <summary> Gets TwoHandedAxeEPKFile.</summary>
    public required string TwoHandedAxeEPKFile { get; init; }

    /// <summary> Gets ClawEPKFile.</summary>
    public required string ClawEPKFile { get; init; }

    /// <summary> Gets PETFile.</summary>
    public required string PETFile { get; init; }

    /// <summary> Gets Shield.</summary>
    public required string Shield { get; init; }

    /// <summary>
    /// Gets ItemVisualEffectDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ItemVisualEffectDat.</returns>
    internal static ItemVisualEffectDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ItemVisualEffect.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemVisualEffectDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DaggerEPKFile
            (var daggerepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BowEPKFile
            (var bowepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OneHandedMaceEPKFile
            (var onehandedmaceepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OneHandedSwordEPKFile
            (var onehandedswordepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TwoHandedSwordEPKFile
            (var twohandedswordepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TwoHandedStaffEPKFile
            (var twohandedstaffepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TwoHandedMaceEPKFile
            (var twohandedmaceepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OneHandedAxeEPKFile
            (var onehandedaxeepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TwoHandedAxeEPKFile
            (var twohandedaxeepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ClawEPKFile
            (var clawepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PETFile
            (var petfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Shield
            (var shieldLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemVisualEffectDat()
            {
                Id = idLoading,
                DaggerEPKFile = daggerepkfileLoading,
                BowEPKFile = bowepkfileLoading,
                OneHandedMaceEPKFile = onehandedmaceepkfileLoading,
                OneHandedSwordEPKFile = onehandedswordepkfileLoading,
                Unknown40 = unknown40Loading,
                TwoHandedSwordEPKFile = twohandedswordepkfileLoading,
                TwoHandedStaffEPKFile = twohandedstaffepkfileLoading,
                Unknown64 = unknown64Loading,
                TwoHandedMaceEPKFile = twohandedmaceepkfileLoading,
                OneHandedAxeEPKFile = onehandedaxeepkfileLoading,
                TwoHandedAxeEPKFile = twohandedaxeepkfileLoading,
                ClawEPKFile = clawepkfileLoading,
                PETFile = petfileLoading,
                Shield = shieldLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
