// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing FixedHideoutDoodadTypes.dat data.
/// </summary>
public sealed partial class FixedHideoutDoodadTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HideoutDoodadsKeys.</summary>
    /// <remarks> references <see cref="HideoutDoodadsDat"/> on <see cref="Specification.LoadHideoutDoodadsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HideoutDoodadsKeys { get; init; }

    /// <summary> Gets BaseTypeHideoutDoodadsKey.</summary>
    /// <remarks> references <see cref="HideoutDoodadsDat"/> on <see cref="Specification.LoadHideoutDoodadsDat"/> index.</remarks>
    public required int? BaseTypeHideoutDoodadsKey { get; init; }

    /// <summary>
    /// Gets FixedHideoutDoodadTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of FixedHideoutDoodadTypesDat.</returns>
    internal static FixedHideoutDoodadTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/FixedHideoutDoodadTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new FixedHideoutDoodadTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HideoutDoodadsKeys
            (var temphideoutdoodadskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var hideoutdoodadskeysLoading = temphideoutdoodadskeysLoading.AsReadOnly();

            // loading BaseTypeHideoutDoodadsKey
            (var basetypehideoutdoodadskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new FixedHideoutDoodadTypesDat()
            {
                Id = idLoading,
                HideoutDoodadsKeys = hideoutdoodadskeysLoading,
                BaseTypeHideoutDoodadsKey = basetypehideoutdoodadskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
