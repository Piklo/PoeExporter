// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing AreaTransitionInfo.dat data.
/// </summary>
public sealed partial class AreaTransitionInfoDat : ISpecificationFile<AreaTransitionInfoDat>
{
    /// <summary> Gets Unknown0.</summary>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int? Unknown48 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int? Unknown64 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int? Unknown80 { get; init; }

    /// <summary> Gets Unknown96.</summary>
    public required int? Unknown96 { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required int? Unknown112 { get; init; }

    /// <summary> Gets Unknown128.</summary>
    public required int? Unknown128 { get; init; }

    /// <summary> Gets Unknown144.</summary>
    public required int? Unknown144 { get; init; }

    /// <summary> Gets Unknown160.</summary>
    public required int? Unknown160 { get; init; }

    /// <summary> Gets Unknown176.</summary>
    public required ReadOnlyCollection<int> Unknown176 { get; init; }

    /// <summary> Gets Unknown192.</summary>
    public required int Unknown192 { get; init; }

    /// <summary> Gets Unknown196.</summary>
    public required ReadOnlyCollection<int> Unknown196 { get; init; }

    /// <inheritdoc/>
    public static AreaTransitionInfoDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AreaTransitionInfo.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AreaTransitionInfoDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown128
            (var unknown128Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown160
            (var unknown160Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown176
            (var tempunknown176Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown176Loading = tempunknown176Loading.AsReadOnly();

            // loading Unknown192
            (var unknown192Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown196
            (var tempunknown196Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown196Loading = tempunknown196Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AreaTransitionInfoDat()
            {
                Unknown0 = unknown0Loading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Unknown80 = unknown80Loading,
                Unknown96 = unknown96Loading,
                Unknown112 = unknown112Loading,
                Unknown128 = unknown128Loading,
                Unknown144 = unknown144Loading,
                Unknown160 = unknown160Loading,
                Unknown176 = unknown176Loading,
                Unknown192 = unknown192Loading,
                Unknown196 = unknown196Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
