// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing RunicCircles.dat data.
/// </summary>
public sealed partial class RunicCirclesDat : IDat<RunicCirclesDat>
{
    /// <summary> Gets Unknown0.</summary>
    public required string Unknown0 { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required ReadOnlyCollection<int> Unknown12 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <inheritdoc/>
    public static RunicCirclesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/RunicCircles.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RunicCirclesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var tempunknown12Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown12Loading = tempunknown12Loading.AsReadOnly();

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RunicCirclesDat()
            {
                Unknown0 = unknown0Loading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown28 = unknown28Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
