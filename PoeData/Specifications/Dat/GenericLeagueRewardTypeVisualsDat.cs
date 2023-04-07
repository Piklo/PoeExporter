// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing GenericLeagueRewardTypeVisuals.dat data.
/// </summary>
public sealed partial class GenericLeagueRewardTypeVisualsDat
{
    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="GenericLeagueRewardTypesDat"/> on <see cref="Specification.GetGenericLeagueRewardTypesDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required float Unknown48 { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets GenericLeagueRewardTypeVisualsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of GenericLeagueRewardTypeVisualsDat.</returns>
    internal static GenericLeagueRewardTypeVisualsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/GenericLeagueRewardTypeVisuals.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GenericLeagueRewardTypeVisualsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GenericLeagueRewardTypeVisualsDat()
            {
                Type = typeLoading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Icon = iconLoading,
                Name = nameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
