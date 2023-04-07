// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing IncursionChests.dat data.
/// </summary>
public sealed partial class IncursionChestsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ChestsKey.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.GetChestsDat"/> index.</remarks>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets UniqueChestsKey.</summary>
    /// <remarks> references <see cref="UniqueChestsDat"/> on <see cref="Specification.GetUniqueChestsDat"/> index.</remarks>
    public required int? UniqueChestsKey { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary>
    /// Gets IncursionChestsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of IncursionChestsDat.</returns>
    internal static IncursionChestsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/IncursionChests.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading UniqueChestsKey
            (var uniquechestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionChestsDat()
            {
                Id = idLoading,
                ChestsKey = chestskeyLoading,
                UniqueChestsKey = uniquechestskeyLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                Weight = weightLoading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
