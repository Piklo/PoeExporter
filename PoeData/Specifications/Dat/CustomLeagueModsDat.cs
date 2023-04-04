// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing CustomLeagueMods.dat data.
/// </summary>
public sealed partial class CustomLeagueModsDat : ISpecificationFile<CustomLeagueModsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required ReadOnlyCollection<int> Unknown8 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required ReadOnlyCollection<int> Unknown24 { get; init; }

    /// <summary> Gets a value indicating whether Unknown40 is set.</summary>
    public required bool Unknown40 { get; init; }

    /// <summary> Gets a value indicating whether Unknown41 is set.</summary>
    public required bool Unknown41 { get; init; }

    /// <summary> Gets Unknown42.</summary>
    public required int Unknown42 { get; init; }

    /// <summary> Gets Unknown46.</summary>
    public required int? Unknown46 { get; init; }

    /// <summary> Gets Unknown62.</summary>
    public required int Unknown62 { get; init; }

    /// <summary> Gets Unknown66.</summary>
    public required int? Unknown66 { get; init; }

    /// <summary> Gets Unknown82.</summary>
    public required int Unknown82 { get; init; }

    /// <inheritdoc/>
    public static CustomLeagueModsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/CustomLeagueMods.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CustomLeagueModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var tempunknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown8Loading = tempunknown8Loading.AsReadOnly();

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CustomLeagueModsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown41 = unknown41Loading,
                Unknown42 = unknown42Loading,
                Unknown46 = unknown46Loading,
                Unknown62 = unknown62Loading,
                Unknown66 = unknown66Loading,
                Unknown82 = unknown82Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
