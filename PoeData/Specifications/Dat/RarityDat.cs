// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Rarity.dat data.
/// </summary>
public sealed partial class RarityDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MinMods.</summary>
    public required int MinMods { get; init; }

    /// <summary> Gets MaxMods.</summary>
    public required int MaxMods { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets MaxPrefix.</summary>
    public required int MaxPrefix { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets MaxSuffix.</summary>
    public required int MaxSuffix { get; init; }

    /// <summary> Gets Color.</summary>
    public required string Color { get; init; }

    /// <summary>
    /// Gets RarityDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of RarityDat.</returns>
    internal static RarityDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Rarity.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RarityDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinMods
            (var minmodsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxMods
            (var maxmodsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxPrefix
            (var maxprefixLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxSuffix
            (var maxsuffixLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Color
            (var colorLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RarityDat()
            {
                Id = idLoading,
                MinMods = minmodsLoading,
                MaxMods = maxmodsLoading,
                Unknown16 = unknown16Loading,
                MaxPrefix = maxprefixLoading,
                Unknown24 = unknown24Loading,
                MaxSuffix = maxsuffixLoading,
                Color = colorLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
