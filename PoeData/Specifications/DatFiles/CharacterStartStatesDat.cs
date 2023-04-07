// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing CharacterStartStates.dat data.
/// </summary>
public sealed partial class CharacterStartStatesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.GetCharactersDat"/> index.</remarks>
    public required int? CharactersKey { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets PassiveSkillsKeys.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.GetPassiveSkillsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PassiveSkillsKeys { get; init; }

    /// <summary> Gets CharacterStartStateSetKey.</summary>
    /// <remarks> references <see cref="CharacterStartStateSetDat"/> on <see cref="Specification.GetCharacterStartStateSetDat"/> index.</remarks>
    public required int? CharacterStartStateSetKey { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int? Unknown68 { get; init; }

    /// <summary> Gets CharacterStartQuestStateKeys.</summary>
    /// <remarks> references <see cref="CharacterStartQuestStateDat"/> on <see cref="Specification.GetCharacterStartQuestStateDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CharacterStartQuestStateKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown100 is set.</summary>
    public required bool Unknown100 { get; init; }

    /// <summary> Gets InfoText.</summary>
    public required string InfoText { get; init; }

    /// <summary> Gets Unknown109.</summary>
    public required int? Unknown109 { get; init; }

    /// <summary>
    /// Gets CharacterStartStatesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of CharacterStartStatesDat.</returns>
    internal static CharacterStartStatesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/CharacterStartStates.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterStartStatesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKey
            (var characterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PassiveSkillsKeys
            (var temppassiveskillskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var passiveskillskeysLoading = temppassiveskillskeysLoading.AsReadOnly();

            // loading CharacterStartStateSetKey
            (var characterstartstatesetkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading CharacterStartQuestStateKeys
            (var tempcharacterstartqueststatekeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var characterstartqueststatekeysLoading = tempcharacterstartqueststatekeysLoading.AsReadOnly();

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading InfoText
            (var infotextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterStartStatesDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                CharactersKey = characterskeyLoading,
                Level = levelLoading,
                PassiveSkillsKeys = passiveskillskeysLoading,
                CharacterStartStateSetKey = characterstartstatesetkeyLoading,
                Unknown68 = unknown68Loading,
                CharacterStartQuestStateKeys = characterstartqueststatekeysLoading,
                Unknown100 = unknown100Loading,
                InfoText = infotextLoading,
                Unknown109 = unknown109Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
