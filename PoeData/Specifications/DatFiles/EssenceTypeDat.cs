// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing EssenceType.dat data.
/// </summary>
public sealed partial class EssenceTypeDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets EssenceType.</summary>
    public required int EssenceType { get; init; }

    /// <summary> Gets a value indicating whether IsCorruptedEssence is set.</summary>
    public required bool IsCorruptedEssence { get; init; }

    /// <summary> Gets WordsKey.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.LoadWordsDat"/> index.</remarks>
    public required int? WordsKey { get; init; }

    /// <summary>
    /// Gets EssenceTypeDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of EssenceTypeDat.</returns>
    internal static EssenceTypeDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/EssenceType.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EssenceTypeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EssenceType
            (var essencetypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsCorruptedEssence
            (var iscorruptedessenceLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading WordsKey
            (var wordskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EssenceTypeDat()
            {
                Id = idLoading,
                EssenceType = essencetypeLoading,
                IsCorruptedEssence = iscorruptedessenceLoading,
                WordsKey = wordskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
