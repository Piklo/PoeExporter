// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MultiPartAchievementConditions.dat data.
/// </summary>
public sealed partial class MultiPartAchievementConditionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MultiPartAchievementsKey1.</summary>
    /// <remarks> references <see cref="MultiPartAchievementsDat"/> on <see cref="Specification.GetMultiPartAchievementsDat"/> index.</remarks>
    public required int? MultiPartAchievementsKey1 { get; init; }

    /// <summary> Gets MultiPartAchievementsKey2.</summary>
    /// <remarks> references <see cref="MultiPartAchievementsDat"/> on <see cref="Specification.GetMultiPartAchievementsDat"/> index.</remarks>
    public required int? MultiPartAchievementsKey2 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary>
    /// Gets MultiPartAchievementConditionsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MultiPartAchievementConditionsDat.</returns>
    internal static MultiPartAchievementConditionsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MultiPartAchievementConditions.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MultiPartAchievementConditionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MultiPartAchievementsKey1
            (var multipartachievementskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MultiPartAchievementsKey2
            (var multipartachievementskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MultiPartAchievementConditionsDat()
            {
                Id = idLoading,
                MultiPartAchievementsKey1 = multipartachievementskey1Loading,
                MultiPartAchievementsKey2 = multipartachievementskey2Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
