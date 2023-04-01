﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing SkillSurgeEffects.dat data.
/// </summary>
public sealed partial class SkillSurgeEffectsDat : ISpecificationFile<SkillSurgeEffectsDat>
{
    /// <summary> Gets GrantedEffectsKey.</summary>
    public required int? GrantedEffectsKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required string Unknown16 { get; init; }

    /// <summary> Gets a value indicating whether Unknown24 is set.</summary>
    public required bool Unknown24 { get; init; }

    /// <summary> Gets a value indicating whether Unknown25 is set.</summary>
    public required bool Unknown25 { get; init; }

    /// <summary> Gets a value indicating whether Unknown26 is set.</summary>
    public required bool Unknown26 { get; init; }

    /// <summary> Gets MiscAnimated.</summary>
    public required int? MiscAnimated { get; init; }

    /// <summary> Gets a value indicating whether Unknown43 is set.</summary>
    public required bool Unknown43 { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary> Gets Unknown45.</summary>
    public required int Unknown45 { get; init; }

    /// <summary> Gets a value indicating whether Unknown49 is set.</summary>
    public required bool Unknown49 { get; init; }

    /// <summary> Gets a value indicating whether Unknown50 is set.</summary>
    public required bool Unknown50 { get; init; }

    /// <summary> Gets a value indicating whether Unknown51 is set.</summary>
    public required bool Unknown51 { get; init; }

    /// <summary> Gets a value indicating whether Unknown52 is set.</summary>
    public required bool Unknown52 { get; init; }

    /// <inheritdoc/>
    public static SkillSurgeEffectsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/SkillSurgeEffects.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillSurgeEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetGrantedEffectsDat();
            // specification.GetMiscAnimatedDat();

            // loading GrantedEffectsKey
            (var grantedeffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown26
            (var unknown26Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MiscAnimated
            (var miscanimatedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown43
            (var unknown43Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown51
            (var unknown51Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillSurgeEffectsDat()
            {
                GrantedEffectsKey = grantedeffectskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown24 = unknown24Loading,
                Unknown25 = unknown25Loading,
                Unknown26 = unknown26Loading,
                MiscAnimated = miscanimatedLoading,
                Unknown43 = unknown43Loading,
                Unknown44 = unknown44Loading,
                Unknown45 = unknown45Loading,
                Unknown49 = unknown49Loading,
                Unknown50 = unknown50Loading,
                Unknown51 = unknown51Loading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
