// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PlayerConditions.dat data.
/// </summary>
public sealed partial class PlayerConditionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BuffDefinitionsKeys.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.LoadBuffDefinitionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BuffDefinitionsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown24 is set.</summary>
    public required bool Unknown24 { get; init; }

    /// <summary> Gets BuffStacks.</summary>
    public required int BuffStacks { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required int? CharactersKey { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown61 is set.</summary>
    public required bool Unknown61 { get; init; }

    /// <summary> Gets StatValue.</summary>
    public required int StatValue { get; init; }

    /// <summary> Gets Unknown66.</summary>
    public required ReadOnlyCollection<int> Unknown66 { get; init; }

    /// <summary> Gets a value indicating whether Unknown82 is set.</summary>
    public required bool Unknown82 { get; init; }

    /// <summary>
    /// Gets PlayerConditionsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PlayerConditionsDat.</returns>
    internal static PlayerConditionsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PlayerConditions.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PlayerConditionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffDefinitionsKeys
            (var tempbuffdefinitionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var buffdefinitionskeysLoading = tempbuffdefinitionskeysLoading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading BuffStacks
            (var buffstacksLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CharactersKey
            (var characterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading StatValue
            (var statvalueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown66
            (var tempunknown66Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown66Loading = tempunknown66Loading.AsReadOnly();

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PlayerConditionsDat()
            {
                Id = idLoading,
                BuffDefinitionsKeys = buffdefinitionskeysLoading,
                Unknown24 = unknown24Loading,
                BuffStacks = buffstacksLoading,
                CharactersKey = characterskeyLoading,
                StatsKeys = statskeysLoading,
                Unknown61 = unknown61Loading,
                StatValue = statvalueLoading,
                Unknown66 = unknown66Loading,
                Unknown82 = unknown82Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
