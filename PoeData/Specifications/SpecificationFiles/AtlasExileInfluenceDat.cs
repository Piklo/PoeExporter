// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing AtlasExileInfluence.dat data.
/// </summary>
public sealed partial class AtlasExileInfluenceDat : ISpecificationFile<AtlasExileInfluenceDat>
{
    /// <summary> Gets Conqueror.</summary>
    public required int? Conqueror { get; init; }

    /// <summary> Gets Sets.</summary>
    public required ReadOnlyCollection<int> Sets { get; init; }

    /// <inheritdoc/>
    public static AtlasExileInfluenceDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AtlasExileInfluence.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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

            // loading referenced tables if any
            // specification.GetAtlasExilesDat();
            // specification.GetAtlasInfluenceSetsDat();

            // loading Conqueror
            (var conquerorLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Sets
            (var tempsetsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var setsLoading = tempsetsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
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
