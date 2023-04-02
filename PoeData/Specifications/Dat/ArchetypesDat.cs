// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Archetypes.dat data.
/// </summary>
public sealed partial class ArchetypesDat : ISpecificationFile<ArchetypesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    public required int? CharactersKey { get; init; }

    /// <summary> Gets PassiveSkillTreeURL.</summary>
    public required string PassiveSkillTreeURL { get; init; }

    /// <summary> Gets AscendancyClassName.</summary>
    public required string AscendancyClassName { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets UIImageFile.</summary>
    public required string UIImageFile { get; init; }

    /// <summary> Gets TutorialVideo_BKFile.</summary>
    public required string TutorialVideo_BKFile { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required float Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required float Unknown72 { get; init; }

    /// <summary> Gets BackgroundImageFile.</summary>
    public required string BackgroundImageFile { get; init; }

    /// <summary> Gets a value indicating whether IsTemporary is set.</summary>
    public required bool IsTemporary { get; init; }

    /// <summary> Gets a value indicating whether Unknown85 is set.</summary>
    public required bool Unknown85 { get; init; }

    /// <summary> Gets ArchetypeImage.</summary>
    public required string ArchetypeImage { get; init; }

    /// <summary> Gets a value indicating whether Unknown94 is set.</summary>
    public required bool Unknown94 { get; init; }

    /// <summary> Gets a value indicating whether Unknown95 is set.</summary>
    public required bool Unknown95 { get; init; }

    /// <inheritdoc/>
    public static ArchetypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Archetypes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArchetypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetCharactersDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKey
            (var characterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PassiveSkillTreeURL
            (var passiveskilltreeurlLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AscendancyClassName
            (var ascendancyclassnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading UIImageFile
            (var uiimagefileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TutorialVideo_BKFile
            (var tutorialvideo_bkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading BackgroundImageFile
            (var backgroundimagefileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsTemporary
            (var istemporaryLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown85
            (var unknown85Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ArchetypeImage
            (var archetypeimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown94
            (var unknown94Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown95
            (var unknown95Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArchetypesDat()
            {
                Id = idLoading,
                CharactersKey = characterskeyLoading,
                PassiveSkillTreeURL = passiveskilltreeurlLoading,
                AscendancyClassName = ascendancyclassnameLoading,
                Description = descriptionLoading,
                UIImageFile = uiimagefileLoading,
                TutorialVideo_BKFile = tutorialvideo_bkfileLoading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                BackgroundImageFile = backgroundimagefileLoading,
                IsTemporary = istemporaryLoading,
                Unknown85 = unknown85Loading,
                ArchetypeImage = archetypeimageLoading,
                Unknown94 = unknown94Loading,
                Unknown95 = unknown95Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
