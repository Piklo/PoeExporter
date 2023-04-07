// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AtlasExileBossArenas.dat data.
/// </summary>
public sealed partial class AtlasExileBossArenasDat
{
    /// <summary> Gets Conqueror.</summary>
    /// <remarks> references <see cref="AtlasExilesDat"/> on <see cref="Specification.GetAtlasExilesDat"/> index.</remarks>
    public required int? Conqueror { get; init; }

    /// <summary> Gets WorldArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldArea { get; init; }

    /// <summary>
    /// Gets AtlasExileBossArenasDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AtlasExileBossArenasDat.</returns>
    internal static AtlasExileBossArenasDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AtlasExileBossArenas.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasExileBossArenasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Conqueror
            (var conquerorLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading WorldArea
            (var worldareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasExileBossArenasDat()
            {
                Conqueror = conquerorLoading,
                WorldArea = worldareaLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
