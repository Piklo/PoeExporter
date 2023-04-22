// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing InfluenceTags.dat data.
/// </summary>
public sealed partial class InfluenceTagsDat
{
    /// <summary> Gets ItemClass.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required int? ItemClass { get; init; }

    /// <summary> Gets Influence.</summary>
    /// <remarks> references <see cref="InfluenceTypesDat"/> on <see cref="Specification.LoadInfluenceTypesDat"/> index.</remarks>
    public required int Influence { get; init; }

    /// <summary> Gets Tag.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required int? Tag { get; init; }

    /// <summary>
    /// Gets InfluenceTagsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of InfluenceTagsDat.</returns>
    internal static InfluenceTagsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/InfluenceTags.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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

            // loading ItemClass
            (var itemclassLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Influence
            (var influenceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tag
            (var tagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
