// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing DamageParticleEffects.dat data.
/// </summary>
public sealed partial class DamageParticleEffectsDat : IDat<DamageParticleEffectsDat>
{
    /// <summary> Gets DamageParticleEffectTypes.</summary>
    /// <remarks> references <see cref="DamageParticleEffectTypesDat"/> on <see cref="Specification.GetDamageParticleEffectTypesDat"/> index.</remarks>
    public required int DamageParticleEffectTypes { get; init; }

    /// <summary> Gets Variation.</summary>
    public required int Variation { get; init; }

    /// <summary> Gets PETFile.</summary>
    public required string PETFile { get; init; }

    /// <summary> Gets ImpactSoundData1.</summary>
    /// <remarks> references <see cref="ImpactSoundDataDat"/> on <see cref="Specification.GetImpactSoundDataDat"/> index.</remarks>
    public required int? ImpactSoundData1 { get; init; }

    /// <summary> Gets ImpactSoundData2.</summary>
    /// <remarks> references <see cref="ImpactSoundDataDat"/> on <see cref="Specification.GetImpactSoundDataDat"/> index.</remarks>
    public required int? ImpactSoundData2 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <inheritdoc/>
    public static DamageParticleEffectsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/DamageParticleEffects.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DamageParticleEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading DamageParticleEffectTypes
            (var damageparticleeffecttypesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Variation
            (var variationLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PETFile
            (var petfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ImpactSoundData1
            (var impactsounddata1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ImpactSoundData2
            (var impactsounddata2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DamageParticleEffectsDat()
            {
                DamageParticleEffectTypes = damageparticleeffecttypesLoading,
                Variation = variationLoading,
                PETFile = petfileLoading,
                ImpactSoundData1 = impactsounddata1Loading,
                ImpactSoundData2 = impactsounddata2Loading,
                Unknown48 = unknown48Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
