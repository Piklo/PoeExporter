// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveSkillTreeTutorial.dat data.
/// </summary>
public sealed partial class PassiveSkillTreeTutorialDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required int? CharactersKey { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets ChoiceA_Description.</summary>
    public required string ChoiceA_Description { get; init; }

    /// <summary> Gets ChoiceB_Description.</summary>
    public required string ChoiceB_Description { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int? Unknown56 { get; init; }

    /// <summary> Gets ChoiceA_PassiveTreeURL.</summary>
    public required string ChoiceA_PassiveTreeURL { get; init; }

    /// <summary> Gets ChoiceB_PassiveTreeURL.</summary>
    public required string ChoiceB_PassiveTreeURL { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int? Unknown88 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required int? Unknown104 { get; init; }

    /// <summary>
    /// Gets PassiveSkillTreeTutorialDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PassiveSkillTreeTutorialDat.</returns>
    internal static PassiveSkillTreeTutorialDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PassiveSkillTreeTutorial.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillTreeTutorialDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKey
            (var characterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ChoiceA_Description
            (var choicea_descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ChoiceB_Description
            (var choiceb_descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ChoiceA_PassiveTreeURL
            (var choicea_passivetreeurlLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ChoiceB_PassiveTreeURL
            (var choiceb_passivetreeurlLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillTreeTutorialDat()
            {
                Id = idLoading,
                CharactersKey = characterskeyLoading,
                Unknown24 = unknown24Loading,
                ChoiceA_Description = choicea_descriptionLoading,
                ChoiceB_Description = choiceb_descriptionLoading,
                Unknown56 = unknown56Loading,
                ChoiceA_PassiveTreeURL = choicea_passivetreeurlLoading,
                ChoiceB_PassiveTreeURL = choiceb_passivetreeurlLoading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
