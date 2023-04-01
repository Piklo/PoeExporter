// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing InfluenceTags.dat data.
/// </summary>
public sealed partial class InfluenceTagsDat : ISpecificationFile<InfluenceTagsDat>
{
    /// <summary> Gets ItemClass.</summary>
    public required int? ItemClass { get; init; }

    /// <summary> Gets Influence.</summary>
    public required int Influence { get; init; }

    /// <summary> Gets Tag.</summary>
    public required int? Tag { get; init; }

    /// <inheritdoc/>
    public static InfluenceTagsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/InfluenceTags.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new InfluenceTagsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetItemClassesDat();
            // specification.GetTagsDat();

            // loading ItemClass
            (var itemclassLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Influence
            (var influenceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tag
            (var tagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new InfluenceTagsDat()
            {
                ItemClass = itemclassLoading,
                Influence = influenceLoading,
                Tag = tagLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
