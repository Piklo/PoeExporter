// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HeistNPCStats.dat data.
/// </summary>
public sealed partial class HeistNPCStatsDat : IDat<HeistNPCStatsDat>
{
    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown16 is set.</summary>
    public required bool Unknown16 { get; init; }

    /// <summary> Gets a value indicating whether Unknown17 is set.</summary>
    public required bool Unknown17 { get; init; }

    /// <summary> Gets a value indicating whether Unknown18 is set.</summary>
    public required bool Unknown18 { get; init; }

    /// <summary> Gets a value indicating whether Unknown19 is set.</summary>
    public required bool Unknown19 { get; init; }

    /// <inheritdoc/>
    public static HeistNPCStatsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/HeistNPCStats.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistNPCStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown17
            (var unknown17Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown18
            (var unknown18Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown19
            (var unknown19Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistNPCStatsDat()
            {
                StatsKey = statskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown17 = unknown17Loading,
                Unknown18 = unknown18Loading,
                Unknown19 = unknown19Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
