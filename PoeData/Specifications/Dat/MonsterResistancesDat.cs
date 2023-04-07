// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MonsterResistances.dat data.
/// </summary>
public sealed partial class MonsterResistancesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets FireNormal.</summary>
    public required int FireNormal { get; init; }

    /// <summary> Gets ColdNormal.</summary>
    public required int ColdNormal { get; init; }

    /// <summary> Gets LightningNormal.</summary>
    public required int LightningNormal { get; init; }

    /// <summary> Gets ChaosNormal.</summary>
    public required int ChaosNormal { get; init; }

    /// <summary> Gets FireCruel.</summary>
    public required int FireCruel { get; init; }

    /// <summary> Gets ColdCruel.</summary>
    public required int ColdCruel { get; init; }

    /// <summary> Gets LightningCruel.</summary>
    public required int LightningCruel { get; init; }

    /// <summary> Gets ChaosCruel.</summary>
    public required int ChaosCruel { get; init; }

    /// <summary> Gets FireMerciless.</summary>
    public required int FireMerciless { get; init; }

    /// <summary> Gets ColdMerciless.</summary>
    public required int ColdMerciless { get; init; }

    /// <summary> Gets LightningMerciless.</summary>
    public required int LightningMerciless { get; init; }

    /// <summary> Gets ChaosMerciless.</summary>
    public required int ChaosMerciless { get; init; }

    /// <summary>
    /// Gets MonsterResistancesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MonsterResistancesDat.</returns>
    internal static MonsterResistancesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MonsterResistances.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterResistancesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FireNormal
            (var firenormalLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ColdNormal
            (var coldnormalLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LightningNormal
            (var lightningnormalLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ChaosNormal
            (var chaosnormalLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FireCruel
            (var firecruelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ColdCruel
            (var coldcruelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LightningCruel
            (var lightningcruelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ChaosCruel
            (var chaoscruelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FireMerciless
            (var firemercilessLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ColdMerciless
            (var coldmercilessLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LightningMerciless
            (var lightningmercilessLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ChaosMerciless
            (var chaosmercilessLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterResistancesDat()
            {
                Id = idLoading,
                FireNormal = firenormalLoading,
                ColdNormal = coldnormalLoading,
                LightningNormal = lightningnormalLoading,
                ChaosNormal = chaosnormalLoading,
                FireCruel = firecruelLoading,
                ColdCruel = coldcruelLoading,
                LightningCruel = lightningcruelLoading,
                ChaosCruel = chaoscruelLoading,
                FireMerciless = firemercilessLoading,
                ColdMerciless = coldmercilessLoading,
                LightningMerciless = lightningmercilessLoading,
                ChaosMerciless = chaosmercilessLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
