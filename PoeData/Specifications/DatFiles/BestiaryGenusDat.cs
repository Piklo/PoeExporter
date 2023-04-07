// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BestiaryGenus.dat data.
/// </summary>
public sealed partial class BestiaryGenusDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets BestiaryGroupsKey.</summary>
    /// <remarks> references <see cref="BestiaryGroupsDat"/> on <see cref="Specification.GetBestiaryGroupsDat"/> index.</remarks>
    public required int? BestiaryGroupsKey { get; init; }

    /// <summary> Gets Name2.</summary>
    public required string Name2 { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary>
    /// Gets BestiaryGenusDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BestiaryGenusDat.</returns>
    internal static BestiaryGenusDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BestiaryGenus.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryGenusDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BestiaryGroupsKey
            (var bestiarygroupskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Name2
            (var name2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryGenusDat()
            {
                Id = idLoading,
                Name = nameLoading,
                BestiaryGroupsKey = bestiarygroupskeyLoading,
                Name2 = name2Loading,
                Icon = iconLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
