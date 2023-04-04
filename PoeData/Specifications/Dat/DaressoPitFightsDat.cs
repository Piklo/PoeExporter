// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing DaressoPitFights.dat data.
/// </summary>
public sealed partial class DaressoPitFightsDat : IDat<DaressoPitFightsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int? Unknown8 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required ReadOnlyCollection<int> Unknown28 { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether Unknown45 is set.</summary>
    public required bool Unknown45 { get; init; }

    /// <summary> Gets Unknown46.</summary>
    public required int Unknown46 { get; init; }

    /// <summary> Gets Unknown50.</summary>
    public required int Unknown50 { get; init; }

    /// <summary> Gets Unknown54.</summary>
    public required int Unknown54 { get; init; }

    /// <summary> Gets a value indicating whether Unknown58 is set.</summary>
    public required bool Unknown58 { get; init; }

    /// <inheritdoc/>
    public static DaressoPitFightsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/DaressoPitFights.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DaressoPitFightsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var tempunknown28Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown28Loading = tempunknown28Loading.AsReadOnly();

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown54
            (var unknown54Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown58
            (var unknown58Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DaressoPitFightsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown44 = unknown44Loading,
                Unknown45 = unknown45Loading,
                Unknown46 = unknown46Loading,
                Unknown50 = unknown50Loading,
                Unknown54 = unknown54Loading,
                Unknown58 = unknown58Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
