// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapSeriesTiers.dat data.
/// </summary>
public sealed partial class MapSeriesTiersDat
{
    /// <summary> Gets MapsKey.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.LoadMapsDat"/> index.</remarks>
    public required int? MapsKey { get; init; }

    /// <summary> Gets MapWorldsTier.</summary>
    public required int MapWorldsTier { get; init; }

    /// <summary> Gets BetrayalTier.</summary>
    public required int BetrayalTier { get; init; }

    /// <summary> Gets SynthesisTier.</summary>
    public required int SynthesisTier { get; init; }

    /// <summary> Gets LegionTier.</summary>
    public required int LegionTier { get; init; }

    /// <summary> Gets BlightTier.</summary>
    public required int BlightTier { get; init; }

    /// <summary> Gets MetamorphosisTier.</summary>
    public required int MetamorphosisTier { get; init; }

    /// <summary> Gets DeliriumTier.</summary>
    public required int DeliriumTier { get; init; }

    /// <summary> Gets HarvestTier.</summary>
    public required int HarvestTier { get; init; }

    /// <summary> Gets HeistTier.</summary>
    public required int HeistTier { get; init; }

    /// <summary> Gets RitualTier.</summary>
    public required int RitualTier { get; init; }

    /// <summary> Gets ExpeditionTier.</summary>
    public required int ExpeditionTier { get; init; }

    /// <summary> Gets ScourgeTier.</summary>
    public required int ScourgeTier { get; init; }

    /// <summary> Gets ArchnemesisTier.</summary>
    public required int ArchnemesisTier { get; init; }

    /// <summary> Gets SentinelTier.</summary>
    public required int SentinelTier { get; init; }

    /// <summary> Gets KalandraTier.</summary>
    public required int KalandraTier { get; init; }

    /// <summary> Gets SanctumTier.</summary>
    public required int SanctumTier { get; init; }

    /// <summary>
    /// Gets MapSeriesTiersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MapSeriesTiersDat.</returns>
    internal static MapSeriesTiersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MapSeriesTiers.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapSeriesTiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MapsKey
            (var mapskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MapWorldsTier
            (var mapworldstierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BetrayalTier
            (var betrayaltierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SynthesisTier
            (var synthesistierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LegionTier
            (var legiontierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BlightTier
            (var blighttierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MetamorphosisTier
            (var metamorphosistierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DeliriumTier
            (var deliriumtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HarvestTier
            (var harvesttierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HeistTier
            (var heisttierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RitualTier
            (var ritualtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ExpeditionTier
            (var expeditiontierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ScourgeTier
            (var scourgetierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ArchnemesisTier
            (var archnemesistierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SentinelTier
            (var sentineltierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading KalandraTier
            (var kalandratierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SanctumTier
            (var sanctumtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapSeriesTiersDat()
            {
                MapsKey = mapskeyLoading,
                MapWorldsTier = mapworldstierLoading,
                BetrayalTier = betrayaltierLoading,
                SynthesisTier = synthesistierLoading,
                LegionTier = legiontierLoading,
                BlightTier = blighttierLoading,
                MetamorphosisTier = metamorphosistierLoading,
                DeliriumTier = deliriumtierLoading,
                HarvestTier = harvesttierLoading,
                HeistTier = heisttierLoading,
                RitualTier = ritualtierLoading,
                ExpeditionTier = expeditiontierLoading,
                ScourgeTier = scourgetierLoading,
                ArchnemesisTier = archnemesistierLoading,
                SentinelTier = sentineltierLoading,
                KalandraTier = kalandratierLoading,
                SanctumTier = sanctumtierLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
