// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing MapStatConditions.dat data.
/// </summary>
public sealed partial class MapStatConditionsDat : ISpecificationFile<MapStatConditionsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets StatsKey.</summary>
    public required int? StatsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown24 is set.</summary>
    public required bool Unknown24 { get; init; }

    /// <summary> Gets StatMin.</summary>
    public required int StatMin { get; init; }

    /// <summary> Gets StatMax.</summary>
    public required int StatMax { get; init; }

    /// <summary> Gets a value indicating whether Unknown33 is set.</summary>
    public required bool Unknown33 { get; init; }

    /// <inheritdoc/>
    public static MapStatConditionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MapStatConditions.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapStatConditionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetStatsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading StatMin
            (var statminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatMax
            (var statmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapStatConditionsDat()
            {
                Id = idLoading,
                StatsKey = statskeyLoading,
                Unknown24 = unknown24Loading,
                StatMin = statminLoading,
                StatMax = statmaxLoading,
                Unknown33 = unknown33Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
