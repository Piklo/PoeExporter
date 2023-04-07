// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BestiaryFamilies.dat data.
/// </summary>
public sealed partial class BestiaryFamiliesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets IconSmall.</summary>
    public required string IconSmall { get; init; }

    /// <summary> Gets Illustration.</summary>
    public required string Illustration { get; init; }

    /// <summary> Gets PageArt.</summary>
    public required string PageArt { get; init; }

    /// <summary> Gets FlavourText.</summary>
    public required string FlavourText { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets TagsKey.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required int? TagsKey { get; init; }

    /// <summary> Gets Unknown73.</summary>
    public required int Unknown73 { get; init; }

    /// <summary> Gets ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys { get; init; }

    /// <summary> Gets CurrencyItemsKey.</summary>
    /// <remarks> references <see cref="CurrencyItemsDat"/> on <see cref="Specification.GetCurrencyItemsDat"/> index.</remarks>
    public required int? CurrencyItemsKey { get; init; }

    /// <inheritdoc/>
    public static BestiaryFamiliesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/BestiaryFamilies.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryFamiliesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IconSmall
            (var iconsmallLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Illustration
            (var illustrationLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PageArt
            (var pageartLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TagsKey
            (var tagskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading CurrencyItemsKey
            (var currencyitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryFamiliesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Icon = iconLoading,
                IconSmall = iconsmallLoading,
                Illustration = illustrationLoading,
                PageArt = pageartLoading,
                FlavourText = flavourtextLoading,
                Unknown56 = unknown56Loading,
                TagsKey = tagskeyLoading,
                Unknown73 = unknown73Loading,
                ModsKeys = modskeysLoading,
                CurrencyItemsKey = currencyitemskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
