// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AtlasAwakeningStats.dat data.
/// </summary>
public sealed partial class AtlasAwakeningStatsDat : IDat<AtlasAwakeningStatsDat>
{
    /// <summary> Gets AwakeningLevel.</summary>
    public required int AwakeningLevel { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets Values.</summary>
    public required ReadOnlyCollection<int> Values { get; init; }

    /// <inheritdoc/>
    public static AtlasAwakeningStatsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/AtlasAwakeningStats.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasAwakeningStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading AwakeningLevel
            (var awakeninglevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading Values
            (var tempvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var valuesLoading = tempvaluesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasAwakeningStatsDat()
            {
                AwakeningLevel = awakeninglevelLoading,
                Stats = statsLoading,
                Values = valuesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
