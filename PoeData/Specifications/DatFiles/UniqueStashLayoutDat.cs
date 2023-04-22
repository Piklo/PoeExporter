// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UniqueStashLayout.dat data.
/// </summary>
public sealed partial class UniqueStashLayoutDat
{
    /// <summary> Gets WordsKey.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.LoadWordsDat"/> index.</remarks>
    public required int? WordsKey { get; init; }

    /// <summary> Gets ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey { get; init; }

    /// <summary> Gets UniqueStashTypesKey.</summary>
    /// <remarks> references <see cref="UniqueStashTypesDat"/> on <see cref="Specification.LoadUniqueStashTypesDat"/> index.</remarks>
    public required int? UniqueStashTypesKey { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets OverrideWidth.</summary>
    public required int OverrideWidth { get; init; }

    /// <summary> Gets OverrideHeight.</summary>
    public required int OverrideHeight { get; init; }

    /// <summary> Gets a value indicating whether ShowIfEmptyChallengeLeague is set.</summary>
    public required bool ShowIfEmptyChallengeLeague { get; init; }

    /// <summary> Gets a value indicating whether ShowIfEmptyStandard is set.</summary>
    public required bool ShowIfEmptyStandard { get; init; }

    /// <summary> Gets RenamedVersion.</summary>
    /// <remarks> references <see cref="UniqueStashLayoutDat"/> on <see cref="Specification.LoadUniqueStashLayoutDat"/> index.</remarks>
    public required int? RenamedVersion { get; init; }

    /// <summary> Gets BaseVersion.</summary>
    /// <remarks> references <see cref="UniqueStashLayoutDat"/> on <see cref="Specification.LoadUniqueStashLayoutDat"/> index.</remarks>
    public required int? BaseVersion { get; init; }

    /// <summary> Gets a value indicating whether IsAlternateArt is set.</summary>
    public required bool IsAlternateArt { get; init; }

    /// <summary>
    /// Gets UniqueStashLayoutDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of UniqueStashLayoutDat.</returns>
    internal static UniqueStashLayoutDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/UniqueStashLayout.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueStashLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WordsKey
            (var wordskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemVisualIdentityKey
            (var itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading UniqueStashTypesKey
            (var uniquestashtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading OverrideWidth
            (var overridewidthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading OverrideHeight
            (var overrideheightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ShowIfEmptyChallengeLeague
            (var showifemptychallengeleagueLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ShowIfEmptyStandard
            (var showifemptystandardLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading RenamedVersion
            (var renamedversionLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading BaseVersion
            (var baseversionLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading IsAlternateArt
            (var isalternateartLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueStashLayoutDat()
            {
                WordsKey = wordskeyLoading,
                ItemVisualIdentityKey = itemvisualidentitykeyLoading,
                UniqueStashTypesKey = uniquestashtypeskeyLoading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                OverrideWidth = overridewidthLoading,
                OverrideHeight = overrideheightLoading,
                ShowIfEmptyChallengeLeague = showifemptychallengeleagueLoading,
                ShowIfEmptyStandard = showifemptystandardLoading,
                RenamedVersion = renamedversionLoading,
                BaseVersion = baseversionLoading,
                IsAlternateArt = isalternateartLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
