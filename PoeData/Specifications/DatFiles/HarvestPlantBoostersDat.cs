// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HarvestPlantBoosters.dat data.
/// </summary>
public sealed partial class HarvestPlantBoostersDat
{
    /// <summary> Gets HarvestObjectsKey.</summary>
    /// <remarks> references <see cref="HarvestObjectsDat"/> on <see cref="Specification.LoadHarvestObjectsDat"/> index.</remarks>
    public required int? HarvestObjectsKey { get; init; }

    /// <summary> Gets Radius.</summary>
    public required int Radius { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }

    /// <summary> Gets Lifeforce.</summary>
    public required int Lifeforce { get; init; }

    /// <summary> Gets AdditionalCraftingOptionsChance.</summary>
    public required int AdditionalCraftingOptionsChance { get; init; }

    /// <summary> Gets RareExtraChances.</summary>
    public required int RareExtraChances { get; init; }

    /// <summary> Gets HarvestPlantBoosterFamilies.</summary>
    public required int HarvestPlantBoosterFamilies { get; init; }

    /// <summary>
    /// Gets HarvestPlantBoostersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HarvestPlantBoostersDat.</returns>
    internal static HarvestPlantBoostersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HarvestPlantBoosters.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestPlantBoostersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HarvestObjectsKey
            (var harvestobjectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Radius
            (var radiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Lifeforce
            (var lifeforceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AdditionalCraftingOptionsChance
            (var additionalcraftingoptionschanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RareExtraChances
            (var rareextrachancesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HarvestPlantBoosterFamilies
            (var harvestplantboosterfamiliesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestPlantBoostersDat()
            {
                HarvestObjectsKey = harvestobjectskeyLoading,
                Radius = radiusLoading,
                Unknown20 = unknown20Loading,
                Lifeforce = lifeforceLoading,
                AdditionalCraftingOptionsChance = additionalcraftingoptionschanceLoading,
                RareExtraChances = rareextrachancesLoading,
                HarvestPlantBoosterFamilies = harvestplantboosterfamiliesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
