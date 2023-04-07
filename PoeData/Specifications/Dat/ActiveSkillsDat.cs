// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ActiveSkills.dat data.
/// </summary>
public sealed partial class ActiveSkillsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets DisplayedName.</summary>
    public required string DisplayedName { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required string Unknown24 { get; init; }

    /// <summary> Gets Icon_DDSFile.</summary>
    public required string Icon_DDSFile { get; init; }

    /// <summary> Gets ActiveSkillTargetTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTargetTypesDat"/> on <see cref="Specification.GetActiveSkillTargetTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ActiveSkillTargetTypes { get; init; }

    /// <summary> Gets ActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.GetActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ActiveSkillTypes { get; init; }

    /// <summary> Gets WeaponRestriction_ItemClassesKeys.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.GetItemClassesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WeaponRestriction_ItemClassesKeys { get; init; }

    /// <summary> Gets WebsiteDescription.</summary>
    public required string WebsiteDescription { get; init; }

    /// <summary> Gets WebsiteImage.</summary>
    public required string WebsiteImage { get; init; }

    /// <summary> Gets a value indicating whether Unknown104 is set.</summary>
    public required bool Unknown104 { get; init; }

    /// <summary> Gets Unknown105.</summary>
    public required string Unknown105 { get; init; }

    /// <summary> Gets a value indicating whether Unknown113 is set.</summary>
    public required bool Unknown113 { get; init; }

    /// <summary> Gets SkillTotemId.</summary>
    /// <remarks> references <see cref="SkillTotemsDat"/> on <see cref="Specification.GetSkillTotemsDat"/> index.</remarks>
    public required int SkillTotemId { get; init; }

    /// <summary> Gets a value indicating whether IsManuallyCasted is set.</summary>
    public required bool IsManuallyCasted { get; init; }

    /// <summary> Gets Input_StatKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Input_StatKeys { get; init; }

    /// <summary> Gets Output_StatKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Output_StatKeys { get; init; }

    /// <summary> Gets MinionActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.GetActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MinionActiveSkillTypes { get; init; }

    /// <summary> Gets a value indicating whether Unknown167 is set.</summary>
    public required bool Unknown167 { get; init; }

    /// <summary> Gets a value indicating whether Unknown168 is set.</summary>
    public required bool Unknown168 { get; init; }

    /// <summary> Gets Unknown169.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown169 { get; init; }

    /// <summary> Gets Unknown185.</summary>
    public required int Unknown185 { get; init; }

    /// <summary> Gets AlternateSkillTargetingBehavioursKey.</summary>
    /// <remarks> references <see cref="AlternateSkillTargetingBehavioursDat"/> on <see cref="Specification.GetAlternateSkillTargetingBehavioursDat"/> index.</remarks>
    public required int? AlternateSkillTargetingBehavioursKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown205 is set.</summary>
    public required bool Unknown205 { get; init; }

    /// <summary> Gets AIFile.</summary>
    public required string AIFile { get; init; }

    /// <summary> Gets Unknown214.</summary>
    public required ReadOnlyCollection<int> Unknown214 { get; init; }

    /// <summary> Gets a value indicating whether Unknown230 is set.</summary>
    public required bool Unknown230 { get; init; }

    /// <summary> Gets a value indicating whether Unknown231 is set.</summary>
    public required bool Unknown231 { get; init; }

    /// <summary> Gets a value indicating whether Unknown232 is set.</summary>
    public required bool Unknown232 { get; init; }

    /// <inheritdoc/>
    public static ActiveSkillsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/ActiveSkills.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ActiveSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DisplayedName
            (var displayednameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon_DDSFile
            (var icon_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ActiveSkillTargetTypes
            (var tempactiveskilltargettypesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var activeskilltargettypesLoading = tempactiveskilltargettypesLoading.AsReadOnly();

            // loading ActiveSkillTypes
            (var tempactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var activeskilltypesLoading = tempactiveskilltypesLoading.AsReadOnly();

            // loading WeaponRestriction_ItemClassesKeys
            (var tempweaponrestriction_itemclasseskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var weaponrestriction_itemclasseskeysLoading = tempweaponrestriction_itemclasseskeysLoading.AsReadOnly();

            // loading WebsiteDescription
            (var websitedescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WebsiteImage
            (var websiteimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SkillTotemId
            (var skilltotemidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsManuallyCasted
            (var ismanuallycastedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Input_StatKeys
            (var tempinput_statkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var input_statkeysLoading = tempinput_statkeysLoading.AsReadOnly();

            // loading Output_StatKeys
            (var tempoutput_statkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var output_statkeysLoading = tempoutput_statkeysLoading.AsReadOnly();

            // loading MinionActiveSkillTypes
            (var tempminionactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var minionactiveskilltypesLoading = tempminionactiveskilltypesLoading.AsReadOnly();

            // loading Unknown167
            (var unknown167Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown168
            (var unknown168Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown169
            (var tempunknown169Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown169Loading = tempunknown169Loading.AsReadOnly();

            // loading Unknown185
            (var unknown185Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AlternateSkillTargetingBehavioursKey
            (var alternateskilltargetingbehaviourskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown205
            (var unknown205Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AIFile
            (var aifileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown214
            (var tempunknown214Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown214Loading = tempunknown214Loading.AsReadOnly();

            // loading Unknown230
            (var unknown230Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown231
            (var unknown231Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown232
            (var unknown232Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ActiveSkillsDat()
            {
                Id = idLoading,
                DisplayedName = displayednameLoading,
                Description = descriptionLoading,
                Unknown24 = unknown24Loading,
                Icon_DDSFile = icon_ddsfileLoading,
                ActiveSkillTargetTypes = activeskilltargettypesLoading,
                ActiveSkillTypes = activeskilltypesLoading,
                WeaponRestriction_ItemClassesKeys = weaponrestriction_itemclasseskeysLoading,
                WebsiteDescription = websitedescriptionLoading,
                WebsiteImage = websiteimageLoading,
                Unknown104 = unknown104Loading,
                Unknown105 = unknown105Loading,
                Unknown113 = unknown113Loading,
                SkillTotemId = skilltotemidLoading,
                IsManuallyCasted = ismanuallycastedLoading,
                Input_StatKeys = input_statkeysLoading,
                Output_StatKeys = output_statkeysLoading,
                MinionActiveSkillTypes = minionactiveskilltypesLoading,
                Unknown167 = unknown167Loading,
                Unknown168 = unknown168Loading,
                Unknown169 = unknown169Loading,
                Unknown185 = unknown185Loading,
                AlternateSkillTargetingBehavioursKey = alternateskilltargetingbehaviourskeyLoading,
                Unknown205 = unknown205Loading,
                AIFile = aifileLoading,
                Unknown214 = unknown214Loading,
                Unknown230 = unknown230Loading,
                Unknown231 = unknown231Loading,
                Unknown232 = unknown232Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
