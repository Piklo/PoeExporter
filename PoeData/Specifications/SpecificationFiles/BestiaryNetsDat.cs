// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing BestiaryNets.dat data.
/// </summary>
public sealed partial class BestiaryNetsDat : ISpecificationFile<BestiaryNetsDat>
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets CaptureMinLevel.</summary>
    public required int CaptureMinLevel { get; init; }

    /// <summary> Gets CaptureMaxLevel.</summary>
    public required int CaptureMaxLevel { get; init; }

    /// <summary> Gets DropMinLevel.</summary>
    public required int DropMinLevel { get; init; }

    /// <summary> Gets DropMaxLevel.</summary>
    public required int DropMaxLevel { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }

    /// <inheritdoc/>
    public static BestiaryNetsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BestiaryNets.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryNetsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CaptureMinLevel
            (var captureminlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CaptureMaxLevel
            (var capturemaxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DropMinLevel
            (var dropminlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DropMaxLevel
            (var dropmaxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryNetsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Unknown16 = unknown16Loading,
                CaptureMinLevel = captureminlevelLoading,
                CaptureMaxLevel = capturemaxlevelLoading,
                DropMinLevel = dropminlevelLoading,
                DropMaxLevel = dropmaxlevelLoading,
                Unknown36 = unknown36Loading,
                IsEnabled = isenabledLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
