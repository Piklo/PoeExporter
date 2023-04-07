// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LegionRewardTypeVisuals.dat data.
/// </summary>
public sealed partial class LegionRewardTypeVisualsDat
{
    /// <summary> Gets IntId.</summary>
    public required int IntId { get; init; }

    /// <summary> Gets MinimapIconsKey.</summary>
    /// <remarks> references <see cref="MinimapIconsDat"/> on <see cref="Specification.GetMinimapIconsDat"/> index.</remarks>
    public required int? MinimapIconsKey { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required string Unknown20 { get; init; }

    /// <summary> Gets MiscAnimatedKey.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required float Unknown44 { get; init; }

    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets LegionRewardTypeVisualsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of LegionRewardTypeVisualsDat.</returns>
    internal static LegionRewardTypeVisualsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/LegionRewardTypeVisuals.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LegionRewardTypeVisualsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinimapIconsKey
            (var minimapiconskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscAnimatedKey
            (var miscanimatedkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LegionRewardTypeVisualsDat()
            {
                IntId = intidLoading,
                MinimapIconsKey = minimapiconskeyLoading,
                Unknown20 = unknown20Loading,
                MiscAnimatedKey = miscanimatedkeyLoading,
                Unknown44 = unknown44Loading,
                Id = idLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
