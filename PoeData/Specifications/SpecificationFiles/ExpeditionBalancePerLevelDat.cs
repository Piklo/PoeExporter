// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing ExpeditionBalancePerLevel.dat data.
/// </summary>
public sealed partial class ExpeditionBalancePerLevelDat : ISpecificationFile<ExpeditionBalancePerLevelDat>
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets a value indicating whether Unknown4 is set.</summary>
    public required bool Unknown4 { get; init; }

    /// <summary> Gets Unknown5.</summary>
    public required int Unknown5 { get; init; }

    /// <summary> Gets Unknown9.</summary>
    public required int Unknown9 { get; init; }

    /// <summary> Gets Unknown13.</summary>
    public required int Unknown13 { get; init; }

    /// <summary> Gets Unknown17.</summary>
    public required int Unknown17 { get; init; }

    /// <summary> Gets Unknown21.</summary>
    public required int Unknown21 { get; init; }

    /// <summary> Gets Unknown25.</summary>
    public required int Unknown25 { get; init; }

    /// <summary> Gets Unknown29.</summary>
    public required int Unknown29 { get; init; }

    /// <summary> Gets Unknown33.</summary>
    public required int Unknown33 { get; init; }

    /// <summary> Gets Unknown37.</summary>
    public required int Unknown37 { get; init; }

    /// <summary> Gets Unknown41.</summary>
    public required int Unknown41 { get; init; }

    /// <summary> Gets Unknown45.</summary>
    public required int Unknown45 { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required int Unknown49 { get; init; }

    /// <summary> Gets Unknown53.</summary>
    public required int Unknown53 { get; init; }

    /// <inheritdoc/>
    public static ExpeditionBalancePerLevelDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ExpeditionBalancePerLevel.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionBalancePerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown5
            (var unknown5Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown9
            (var unknown9Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown13
            (var unknown13Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown17
            (var unknown17Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown21
            (var unknown21Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown53
            (var unknown53Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionBalancePerLevelDat()
            {
                Level = levelLoading,
                Unknown4 = unknown4Loading,
                Unknown5 = unknown5Loading,
                Unknown9 = unknown9Loading,
                Unknown13 = unknown13Loading,
                Unknown17 = unknown17Loading,
                Unknown21 = unknown21Loading,
                Unknown25 = unknown25Loading,
                Unknown29 = unknown29Loading,
                Unknown33 = unknown33Loading,
                Unknown37 = unknown37Loading,
                Unknown41 = unknown41Loading,
                Unknown45 = unknown45Loading,
                Unknown49 = unknown49Loading,
                Unknown53 = unknown53Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
