// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing PassiveSkillTreeUIArt.dat data.
/// </summary>
public sealed partial class PassiveSkillTreeUIArtDat : ISpecificationFile<PassiveSkillTreeUIArtDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets GroupBackgroundSmall.</summary>
    public required string GroupBackgroundSmall { get; init; }

    /// <summary> Gets GroupBackgroundMedium.</summary>
    public required string GroupBackgroundMedium { get; init; }

    /// <summary> Gets GroupBackgroundLarge.</summary>
    public required string GroupBackgroundLarge { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <summary> Gets PassiveFrameNormal.</summary>
    public required string PassiveFrameNormal { get; init; }

    /// <summary> Gets NotableFrameNormal.</summary>
    public required string NotableFrameNormal { get; init; }

    /// <summary> Gets KeystoneFrameNormal.</summary>
    public required string KeystoneFrameNormal { get; init; }

    /// <summary> Gets PassiveFrameActive.</summary>
    public required string PassiveFrameActive { get; init; }

    /// <summary> Gets NotableFrameActive.</summary>
    public required string NotableFrameActive { get; init; }

    /// <summary> Gets KeystoneFrameActive.</summary>
    public required string KeystoneFrameActive { get; init; }

    /// <summary> Gets PassiveFrameCanAllocate.</summary>
    public required string PassiveFrameCanAllocate { get; init; }

    /// <summary> Gets NotableFrameCanAllocate.</summary>
    public required string NotableFrameCanAllocate { get; init; }

    /// <summary> Gets KeystoneCanAllocate.</summary>
    public required string KeystoneCanAllocate { get; init; }

    /// <summary> Gets Ornament.</summary>
    public required string Ornament { get; init; }

    /// <summary> Gets GroupBackgroundSmallBlank.</summary>
    public required string GroupBackgroundSmallBlank { get; init; }

    /// <summary> Gets GroupBackgroundMediumBlank.</summary>
    public required string GroupBackgroundMediumBlank { get; init; }

    /// <summary> Gets GroupBackgroundLargeBlank.</summary>
    public required string GroupBackgroundLargeBlank { get; init; }

    /// <inheritdoc/>
    public static PassiveSkillTreeUIArtDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/PassiveSkillTreeUIArt.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillTreeUIArtDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundSmall
            (var groupbackgroundsmallLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundMedium
            (var groupbackgroundmediumLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundLarge
            (var groupbackgroundlargeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading PassiveFrameNormal
            (var passiveframenormalLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NotableFrameNormal
            (var notableframenormalLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading KeystoneFrameNormal
            (var keystoneframenormalLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveFrameActive
            (var passiveframeactiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NotableFrameActive
            (var notableframeactiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading KeystoneFrameActive
            (var keystoneframeactiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveFrameCanAllocate
            (var passiveframecanallocateLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NotableFrameCanAllocate
            (var notableframecanallocateLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading KeystoneCanAllocate
            (var keystonecanallocateLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Ornament
            (var ornamentLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundSmallBlank
            (var groupbackgroundsmallblankLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundMediumBlank
            (var groupbackgroundmediumblankLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundLargeBlank
            (var groupbackgroundlargeblankLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillTreeUIArtDat()
            {
                Id = idLoading,
                GroupBackgroundSmall = groupbackgroundsmallLoading,
                GroupBackgroundMedium = groupbackgroundmediumLoading,
                GroupBackgroundLarge = groupbackgroundlargeLoading,
                Unknown32 = unknown32Loading,
                PassiveFrameNormal = passiveframenormalLoading,
                NotableFrameNormal = notableframenormalLoading,
                KeystoneFrameNormal = keystoneframenormalLoading,
                PassiveFrameActive = passiveframeactiveLoading,
                NotableFrameActive = notableframeactiveLoading,
                KeystoneFrameActive = keystoneframeactiveLoading,
                PassiveFrameCanAllocate = passiveframecanallocateLoading,
                NotableFrameCanAllocate = notableframecanallocateLoading,
                KeystoneCanAllocate = keystonecanallocateLoading,
                Ornament = ornamentLoading,
                GroupBackgroundSmallBlank = groupbackgroundsmallblankLoading,
                GroupBackgroundMediumBlank = groupbackgroundmediumblankLoading,
                GroupBackgroundLargeBlank = groupbackgroundlargeblankLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
