// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BestiaryRecipeComponent.dat data.
/// </summary>
public sealed partial class BestiaryRecipeComponentDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets BestiaryFamiliesKey.</summary>
    /// <remarks> references <see cref="BestiaryFamiliesDat"/> on <see cref="Specification.GetBestiaryFamiliesDat"/> index.</remarks>
    public required int? BestiaryFamiliesKey { get; init; }

    /// <summary> Gets BestiaryGroupsKey.</summary>
    /// <remarks> references <see cref="BestiaryGroupsDat"/> on <see cref="Specification.GetBestiaryGroupsDat"/> index.</remarks>
    public required int? BestiaryGroupsKey { get; init; }

    /// <summary> Gets ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? ModsKey { get; init; }

    /// <summary> Gets BestiaryCapturableMonstersKey.</summary>
    /// <remarks> references <see cref="BestiaryCapturableMonstersDat"/> on <see cref="Specification.GetBestiaryCapturableMonstersDat"/> index.</remarks>
    public required int? BestiaryCapturableMonstersKey { get; init; }

    /// <summary> Gets BeastRarity.</summary>
    /// <remarks> references <see cref="RarityDat"/> on <see cref="Specification.GetRarityDat"/> index.</remarks>
    public required int? BeastRarity { get; init; }

    /// <summary> Gets BestiaryGenusKey.</summary>
    /// <remarks> references <see cref="BestiaryGenusDat"/> on <see cref="Specification.GetBestiaryGenusDat"/> index.</remarks>
    public required int? BestiaryGenusKey { get; init; }

    /// <summary>
    /// Gets BestiaryRecipeComponentDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BestiaryRecipeComponentDat.</returns>
    internal static BestiaryRecipeComponentDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BestiaryRecipeComponent.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryRecipeComponentDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BestiaryFamiliesKey
            (var bestiaryfamilieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BestiaryGroupsKey
            (var bestiarygroupskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BestiaryCapturableMonstersKey
            (var bestiarycapturablemonsterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BeastRarity
            (var beastrarityLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BestiaryGenusKey
            (var bestiarygenuskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryRecipeComponentDat()
            {
                Id = idLoading,
                MinLevel = minlevelLoading,
                BestiaryFamiliesKey = bestiaryfamilieskeyLoading,
                BestiaryGroupsKey = bestiarygroupskeyLoading,
                ModsKey = modskeyLoading,
                BestiaryCapturableMonstersKey = bestiarycapturablemonsterskeyLoading,
                BeastRarity = beastrarityLoading,
                BestiaryGenusKey = bestiarygenuskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
