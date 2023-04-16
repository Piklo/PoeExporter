// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ArmourTypes.dat data.
/// </summary>
public sealed partial class ArmourTypesDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets ArmourMin.</summary>
    public required int ArmourMin { get; init; }

    /// <summary> Gets ArmourMax.</summary>
    public required int ArmourMax { get; init; }

    /// <summary> Gets EvasionMin.</summary>
    public required int EvasionMin { get; init; }

    /// <summary> Gets EvasionMax.</summary>
    public required int EvasionMax { get; init; }

    /// <summary> Gets EnergyShieldMin.</summary>
    public required int EnergyShieldMin { get; init; }

    /// <summary> Gets EnergyShieldMax.</summary>
    public required int EnergyShieldMax { get; init; }

    /// <summary> Gets IncreasedMovementSpeed.</summary>
    public required int IncreasedMovementSpeed { get; init; }

    /// <summary> Gets WardMin.</summary>
    public required int WardMin { get; init; }

    /// <summary> Gets WardMax.</summary>
    public required int WardMax { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary>
    /// Gets ArmourTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ArmourTypesDat.</returns>
    internal static ArmourTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ArmourTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArmourTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ArmourMin
            (var armourminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ArmourMax
            (var armourmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EvasionMin
            (var evasionminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EvasionMax
            (var evasionmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EnergyShieldMin
            (var energyshieldminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EnergyShieldMax
            (var energyshieldmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IncreasedMovementSpeed
            (var increasedmovementspeedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WardMin
            (var wardminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WardMax
            (var wardmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArmourTypesDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                ArmourMin = armourminLoading,
                ArmourMax = armourmaxLoading,
                EvasionMin = evasionminLoading,
                EvasionMax = evasionmaxLoading,
                EnergyShieldMin = energyshieldminLoading,
                EnergyShieldMax = energyshieldmaxLoading,
                IncreasedMovementSpeed = increasedmovementspeedLoading,
                WardMin = wardminLoading,
                WardMax = wardmaxLoading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
