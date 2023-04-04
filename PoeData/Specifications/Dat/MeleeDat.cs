// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Melee.dat data.
/// </summary>
public sealed partial class MeleeDat : ISpecificationFile<MeleeDat>
{
    /// <summary> Gets ActiveSkill.</summary>
    /// <remarks> references <see cref="ActiveSkillsDat"/> on <see cref="Specification.GetActiveSkillsDat"/> index.</remarks>
    public required int? ActiveSkill { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets MiscAnimated.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimated { get; init; }

    /// <summary> Gets MeleeTrailsKey1.</summary>
    /// <remarks> references <see cref="MeleeTrailsDat"/> on <see cref="Specification.GetMeleeTrailsDat"/> index.</remarks>
    public required int? MeleeTrailsKey1 { get; init; }

    /// <summary> Gets MeleeTrailsKey2.</summary>
    /// <remarks> references <see cref="MeleeTrailsDat"/> on <see cref="Specification.GetMeleeTrailsDat"/> index.</remarks>
    public required int? MeleeTrailsKey2 { get; init; }

    /// <summary> Gets MeleeTrailsKey3.</summary>
    /// <remarks> references <see cref="MeleeTrailsDat"/> on <see cref="Specification.GetMeleeTrailsDat"/> index.</remarks>
    public required int? MeleeTrailsKey3 { get; init; }

    /// <summary> Gets MeleeTrailsKey4.</summary>
    /// <remarks> references <see cref="MeleeTrailsDat"/> on <see cref="Specification.GetMeleeTrailsDat"/> index.</remarks>
    public required int? MeleeTrailsKey4 { get; init; }

    /// <summary> Gets MeleeTrailsKey5.</summary>
    /// <remarks> references <see cref="MeleeTrailsDat"/> on <see cref="Specification.GetMeleeTrailsDat"/> index.</remarks>
    public required int? MeleeTrailsKey5 { get; init; }

    /// <summary> Gets MeleeTrailsKey6.</summary>
    /// <remarks> references <see cref="MeleeTrailsDat"/> on <see cref="Specification.GetMeleeTrailsDat"/> index.</remarks>
    public required int? MeleeTrailsKey6 { get; init; }

    /// <summary> Gets MeleeTrailsKey7.</summary>
    /// <remarks> references <see cref="MeleeTrailsDat"/> on <see cref="Specification.GetMeleeTrailsDat"/> index.</remarks>
    public required int? MeleeTrailsKey7 { get; init; }

    /// <summary> Gets a value indicating whether Unknown148 is set.</summary>
    public required bool Unknown148 { get; init; }

    /// <summary> Gets SurgeEffect_EPKFile.</summary>
    public required string SurgeEffect_EPKFile { get; init; }

    /// <summary> Gets Unknown157.</summary>
    public required string Unknown157 { get; init; }

    /// <summary> Gets Unknown165.</summary>
    public required string Unknown165 { get; init; }

    /// <inheritdoc/>
    public static MeleeDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Melee.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MeleeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ActiveSkill
            (var activeskillLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscAnimated
            (var miscanimatedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MeleeTrailsKey1
            (var meleetrailskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MeleeTrailsKey2
            (var meleetrailskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MeleeTrailsKey3
            (var meleetrailskey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MeleeTrailsKey4
            (var meleetrailskey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MeleeTrailsKey5
            (var meleetrailskey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MeleeTrailsKey6
            (var meleetrailskey6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MeleeTrailsKey7
            (var meleetrailskey7Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SurgeEffect_EPKFile
            (var surgeeffect_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown157
            (var unknown157Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown165
            (var unknown165Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MeleeDat()
            {
                ActiveSkill = activeskillLoading,
                Unknown16 = unknown16Loading,
                MiscAnimated = miscanimatedLoading,
                MeleeTrailsKey1 = meleetrailskey1Loading,
                MeleeTrailsKey2 = meleetrailskey2Loading,
                MeleeTrailsKey3 = meleetrailskey3Loading,
                MeleeTrailsKey4 = meleetrailskey4Loading,
                MeleeTrailsKey5 = meleetrailskey5Loading,
                MeleeTrailsKey6 = meleetrailskey6Loading,
                MeleeTrailsKey7 = meleetrailskey7Loading,
                Unknown148 = unknown148Loading,
                SurgeEffect_EPKFile = surgeeffect_epkfileLoading,
                Unknown157 = unknown157Loading,
                Unknown165 = unknown165Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
