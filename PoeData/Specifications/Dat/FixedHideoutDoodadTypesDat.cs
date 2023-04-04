// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing FixedHideoutDoodadTypes.dat data.
/// </summary>
public sealed partial class FixedHideoutDoodadTypesDat : ISpecificationFile<FixedHideoutDoodadTypesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HideoutDoodadsKeys.</summary>
    /// <remarks> references <see cref="HideoutDoodadsDat"/> on <see cref="Specification.GetHideoutDoodadsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HideoutDoodadsKeys { get; init; }

    /// <summary> Gets BaseTypeHideoutDoodadsKey.</summary>
    /// <remarks> references <see cref="HideoutDoodadsDat"/> on <see cref="Specification.GetHideoutDoodadsDat"/> index.</remarks>
    public required int? BaseTypeHideoutDoodadsKey { get; init; }

    /// <inheritdoc/>
    public static FixedHideoutDoodadTypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/FixedHideoutDoodadTypes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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
            (var basetypehideoutdoodadskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
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
