// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing UniqueMaps.dat data.
/// </summary>
public sealed partial class UniqueMapsDat : ISpecificationFile<UniqueMapsDat>
{
    /// <summary> Gets ItemVisualIdentityKey.</summary>
    public required int? ItemVisualIdentityKey { get; init; }

    /// <summary> Gets WorldAreasKey.</summary>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets WordsKey.</summary>
    public required int? WordsKey { get; init; }

    /// <summary> Gets FlavourTextKey.</summary>
    public required int? FlavourTextKey { get; init; }

    /// <summary> Gets a value indicating whether HasGuildCharacter is set.</summary>
    public required bool HasGuildCharacter { get; init; }

    /// <summary> Gets GuildCharacter.</summary>
    public required string GuildCharacter { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets a value indicating whether IsDropDisabled is set.</summary>
    public required bool IsDropDisabled { get; init; }

    /// <inheritdoc/>
    public static UniqueMapsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/UniqueMaps.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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

            // loading referenced tables if any
            // specification.GetItemVisualIdentityDat();
            // specification.GetWorldAreasDat();
            // specification.GetWordsDat();
            // specification.GetFlavourTextDat();

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
