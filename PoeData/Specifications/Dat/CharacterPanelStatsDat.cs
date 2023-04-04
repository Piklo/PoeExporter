// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing CharacterPanelStats.dat data.
/// </summary>
public sealed partial class CharacterPanelStatsDat : IDat<CharacterPanelStatsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets StatsKeys1.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys1 { get; init; }

    /// <summary> Gets CharacterPanelDescriptionModesKey.</summary>
    /// <remarks> references <see cref="CharacterPanelDescriptionModesDat"/> on <see cref="Specification.GetCharacterPanelDescriptionModesDat"/> index.</remarks>
    public required int? CharacterPanelDescriptionModesKey { get; init; }

    /// <summary> Gets StatsKeys2.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys2 { get; init; }

    /// <summary> Gets StatsKeys3.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys3 { get; init; }

    /// <summary> Gets CharacterPanelTabsKey.</summary>
    /// <remarks> references <see cref="CharacterPanelTabsDat"/> on <see cref="Specification.GetCharacterPanelTabsDat"/> index.</remarks>
    public required int? CharacterPanelTabsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown96 is set.</summary>
    public required bool Unknown96 { get; init; }

    /// <summary> Gets Unknown97.</summary>
    public required ReadOnlyCollection<int> Unknown97 { get; init; }

    /// <summary> Gets Unknown113.</summary>
    public required int Unknown113 { get; init; }

    /// <inheritdoc/>
    public static CharacterPanelStatsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/CharacterPanelStats.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterPanelStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StatsKeys1
            (var tempstatskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeys1Loading = tempstatskeys1Loading.AsReadOnly();

            // loading CharacterPanelDescriptionModesKey
            (var characterpaneldescriptionmodeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatsKeys2
            (var tempstatskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeys2Loading = tempstatskeys2Loading.AsReadOnly();

            // loading StatsKeys3
            (var tempstatskeys3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeys3Loading = tempstatskeys3Loading.AsReadOnly();

            // loading CharacterPanelTabsKey
            (var characterpaneltabskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown97
            (var tempunknown97Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown97Loading = tempunknown97Loading.AsReadOnly();

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterPanelStatsDat()
            {
                Id = idLoading,
                Text = textLoading,
                StatsKeys1 = statskeys1Loading,
                CharacterPanelDescriptionModesKey = characterpaneldescriptionmodeskeyLoading,
                StatsKeys2 = statskeys2Loading,
                StatsKeys3 = statskeys3Loading,
                CharacterPanelTabsKey = characterpaneltabskeyLoading,
                Unknown96 = unknown96Loading,
                Unknown97 = unknown97Loading,
                Unknown113 = unknown113Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
