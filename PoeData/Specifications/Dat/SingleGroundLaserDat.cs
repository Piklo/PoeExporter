// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing SingleGroundLaser.dat data.
/// </summary>
public sealed partial class SingleGroundLaserDat : ISpecificationFile<SingleGroundLaserDat>
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int? Unknown4 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required string Unknown40 { get; init; }

    /// <summary> Gets a value indicating whether Unknown48 is set.</summary>
    public required bool Unknown48 { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required int Unknown49 { get; init; }

    /// <summary> Gets a value indicating whether Unknown53 is set.</summary>
    public required bool Unknown53 { get; init; }

    /// <summary> Gets Unknown54.</summary>
    public required int Unknown54 { get; init; }

    /// <summary> Gets Unknown58.</summary>
    public required int Unknown58 { get; init; }

    /// <summary> Gets Unknown62.</summary>
    public required int? Unknown62 { get; init; }

    /// <summary> Gets Unknown78.</summary>
    public required int? Unknown78 { get; init; }

    /// <summary> Gets Unknown94.</summary>
    public required int Unknown94 { get; init; }

    /// <summary> Gets a value indicating whether Unknown98 is set.</summary>
    public required bool Unknown98 { get; init; }

    /// <inheritdoc/>
    public static SingleGroundLaserDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/SingleGroundLaser.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SingleGroundLaserDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown53
            (var unknown53Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown54
            (var unknown54Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown58
            (var unknown58Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown94
            (var unknown94Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown98
            (var unknown98Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SingleGroundLaserDat()
            {
                Id = idLoading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown48 = unknown48Loading,
                Unknown49 = unknown49Loading,
                Unknown53 = unknown53Loading,
                Unknown54 = unknown54Loading,
                Unknown58 = unknown58Loading,
                Unknown62 = unknown62Loading,
                Unknown78 = unknown78Loading,
                Unknown94 = unknown94Loading,
                Unknown98 = unknown98Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
