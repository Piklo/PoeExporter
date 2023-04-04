// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AdvancedSkillsTutorial.dat data.
/// </summary>
public sealed partial class AdvancedSkillsTutorialDat : IDat<AdvancedSkillsTutorialDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SkillGemInfoKey1.</summary>
    /// <remarks> references <see cref="SkillGemInfoDat"/> on <see cref="Specification.GetSkillGemInfoDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SkillGemInfoKey1 { get; init; }

    /// <summary> Gets SkillGemInfoKey2.</summary>
    /// <remarks> references <see cref="SkillGemInfoDat"/> on <see cref="Specification.GetSkillGemInfoDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SkillGemInfoKey2 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets International_BK2File.</summary>
    public required string International_BK2File { get; init; }

    /// <summary> Gets SkillGemsKey.</summary>
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.GetSkillGemsDat"/> index.</remarks>
    public required int? SkillGemsKey { get; init; }

    /// <summary> Gets China_BK2File.</summary>
    public required string China_BK2File { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.GetCharactersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CharactersKey { get; init; }

    /// <inheritdoc/>
    public static AdvancedSkillsTutorialDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AdvancedSkillsTutorial.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AdvancedSkillsTutorialDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SkillGemInfoKey1
            (var tempskillgeminfokey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var skillgeminfokey1Loading = tempskillgeminfokey1Loading.AsReadOnly();

            // loading SkillGemInfoKey2
            (var tempskillgeminfokey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var skillgeminfokey2Loading = tempskillgeminfokey2Loading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading International_BK2File
            (var international_bk2fileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SkillGemsKey
            (var skillgemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading China_BK2File
            (var china_bk2fileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKey
            (var tempcharacterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var characterskeyLoading = tempcharacterskeyLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AdvancedSkillsTutorialDat()
            {
                Id = idLoading,
                SkillGemInfoKey1 = skillgeminfokey1Loading,
                SkillGemInfoKey2 = skillgeminfokey2Loading,
                Description = descriptionLoading,
                International_BK2File = international_bk2fileLoading,
                SkillGemsKey = skillgemskeyLoading,
                China_BK2File = china_bk2fileLoading,
                CharactersKey = characterskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
