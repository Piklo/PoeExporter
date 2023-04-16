﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UniqueMaps.dat data.
/// </summary>
public sealed partial class UniqueMapsDat
{
    /// <summary> Gets ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.GetItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey { get; init; }

    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets WordsKey.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.GetWordsDat"/> index.</remarks>
    public required int? WordsKey { get; init; }

    /// <summary> Gets FlavourTextKey.</summary>
    /// <remarks> references <see cref="FlavourTextDat"/> on <see cref="Specification.GetFlavourTextDat"/> index.</remarks>
    public required int? FlavourTextKey { get; init; }

    /// <summary> Gets a value indicating whether HasGuildCharacter is set.</summary>
    public required bool HasGuildCharacter { get; init; }

    /// <summary> Gets GuildCharacter.</summary>
    public required string GuildCharacter { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets a value indicating whether IsDropDisabled is set.</summary>
    public required bool IsDropDisabled { get; init; }

    /// <summary>
    /// Gets UniqueMapsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of UniqueMapsDat.</returns>
    internal static UniqueMapsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/UniqueMaps.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueMapsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ItemVisualIdentityKey
            (var itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading WordsKey
            (var wordskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading FlavourTextKey
            (var flavourtextkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HasGuildCharacter
            (var hasguildcharacterLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading GuildCharacter
            (var guildcharacterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsDropDisabled
            (var isdropdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueMapsDat()
            {
                ItemVisualIdentityKey = itemvisualidentitykeyLoading,
                WorldAreasKey = worldareaskeyLoading,
                WordsKey = wordskeyLoading,
                FlavourTextKey = flavourtextkeyLoading,
                HasGuildCharacter = hasguildcharacterLoading,
                GuildCharacter = guildcharacterLoading,
                Name = nameLoading,
                IsDropDisabled = isdropdisabledLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}