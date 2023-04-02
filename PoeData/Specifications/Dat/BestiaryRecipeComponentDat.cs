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
public sealed partial class BestiaryRecipeComponentDat : ISpecificationFile<BestiaryRecipeComponentDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets BestiaryFamiliesKey.</summary>
    public required int? BestiaryFamiliesKey { get; init; }

    /// <summary> Gets BestiaryGroupsKey.</summary>
    public required int? BestiaryGroupsKey { get; init; }

    /// <summary> Gets ModsKey.</summary>
    public required int? ModsKey { get; init; }

    /// <summary> Gets BestiaryCapturableMonstersKey.</summary>
    public required int? BestiaryCapturableMonstersKey { get; init; }

    /// <summary> Gets BeastRarity.</summary>
    public required int? BeastRarity { get; init; }

    /// <summary> Gets BestiaryGenusKey.</summary>
    public required int? BestiaryGenusKey { get; init; }

    /// <inheritdoc/>
    public static BestiaryRecipeComponentDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BestiaryRecipeComponent.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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

            // loading referenced tables if any
            // specification.GetBestiaryFamiliesDat();
            // specification.GetBestiaryGroupsDat();
            // specification.GetModsDat();
            // specification.GetBestiaryCapturableMonstersDat();
            // specification.GetRarityDat();
            // specification.GetBestiaryGenusDat();

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
