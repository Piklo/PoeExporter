// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasNodeDefinition.dat data.
/// </summary>
public sealed partial class AtlasNodeDefinitionDat
{
    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.GetItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Unknown37.</summary>
    public required int Unknown37 { get; init; }

    /// <summary> Gets Unknown41.</summary>
    public required int Unknown41 { get; init; }

    /// <summary>
    /// Gets AtlasNodeDefinitionDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AtlasNodeDefinitionDat.</returns>
    internal static AtlasNodeDefinitionDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AtlasNodeDefinition.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasNodeDefinitionDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ItemVisualIdentityKey
            (var itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasNodeDefinitionDat()
            {
                WorldAreasKey = worldareaskeyLoading,
                ItemVisualIdentityKey = itemvisualidentitykeyLoading,
                Unknown32 = unknown32Loading,
                Tier = tierLoading,
                Unknown37 = unknown37Loading,
                Unknown41 = unknown41Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
