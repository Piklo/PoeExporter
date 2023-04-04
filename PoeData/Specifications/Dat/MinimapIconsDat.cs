// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MinimapIcons.dat data.
/// </summary>
public sealed partial class MinimapIconsDat : IDat<MinimapIconsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MinimapIconRadius.</summary>
    public required int MinimapIconRadius { get; init; }

    /// <summary> Gets LargemapIconRadius.</summary>
    public required int LargemapIconRadius { get; init; }

    /// <summary> Gets a value indicating whether Unknown16 is set.</summary>
    public required bool Unknown16 { get; init; }

    /// <summary> Gets a value indicating whether Unknown17 is set.</summary>
    public required bool Unknown17 { get; init; }

    /// <summary> Gets a value indicating whether Unknown18 is set.</summary>
    public required bool Unknown18 { get; init; }

    /// <summary> Gets MinimapIconPointerMaxDistance.</summary>
    public required int MinimapIconPointerMaxDistance { get; init; }

    /// <summary> Gets Unknown23.</summary>
    public required int Unknown23 { get; init; }

    /// <inheritdoc/>
    public static MinimapIconsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MinimapIcons.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MinimapIconsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinimapIconRadius
            (var minimapiconradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LargemapIconRadius
            (var largemapiconradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown17
            (var unknown17Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown18
            (var unknown18Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MinimapIconPointerMaxDistance
            (var minimapiconpointermaxdistanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown23
            (var unknown23Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MinimapIconsDat()
            {
                Id = idLoading,
                MinimapIconRadius = minimapiconradiusLoading,
                LargemapIconRadius = largemapiconradiusLoading,
                Unknown16 = unknown16Loading,
                Unknown17 = unknown17Loading,
                Unknown18 = unknown18Loading,
                MinimapIconPointerMaxDistance = minimapiconpointermaxdistanceLoading,
                Unknown23 = unknown23Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
