// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing DroneBaseTypes.dat data.
/// </summary>
public sealed partial class DroneBaseTypesDat : ISpecificationFile<DroneBaseTypesDat>
{
    /// <summary> Gets BaseType.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseType { get; init; }

    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="DroneTypesDat"/> on <see cref="Specification.GetDroneTypesDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Visual.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.GetBuffVisualsDat"/> index.</remarks>
    public required int? Visual { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int Unknown60 { get; init; }

    /// <summary> Gets UseAchievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? UseAchievement { get; init; }

    /// <summary> Gets a value indicating whether Unknown80 is set.</summary>
    public required bool Unknown80 { get; init; }

    /// <inheritdoc/>
    public static DroneBaseTypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/DroneBaseTypes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DroneBaseTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseType
            (var basetypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Visual
            (var visualLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UseAchievement
            (var useachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DroneBaseTypesDat()
            {
                BaseType = basetypeLoading,
                Type = typeLoading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Visual = visualLoading,
                Unknown60 = unknown60Loading,
                UseAchievement = useachievementLoading,
                Unknown80 = unknown80Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
