// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasExileInfluence.dat data.
/// </summary>
public sealed partial class AtlasExileInfluenceDat
{
    /// <summary> Gets Conqueror.</summary>
    /// <remarks> references <see cref="AtlasExilesDat"/> on <see cref="Specification.GetAtlasExilesDat"/> index.</remarks>
    public required int? Conqueror { get; init; }

    /// <summary> Gets Sets.</summary>
    /// <remarks> references <see cref="AtlasInfluenceSetsDat"/> on <see cref="Specification.GetAtlasInfluenceSetsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Sets { get; init; }

    /// <summary>
    /// Gets AtlasExileInfluenceDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AtlasExileInfluenceDat.</returns>
    internal static AtlasExileInfluenceDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AtlasExileInfluence.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasExileInfluenceDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Conqueror
            (var conquerorLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Sets
            (var tempsetsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var setsLoading = tempsetsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasExileInfluenceDat()
            {
                Conqueror = conquerorLoading,
                Sets = setsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
