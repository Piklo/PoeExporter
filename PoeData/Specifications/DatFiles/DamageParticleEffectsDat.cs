// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DamageParticleEffects.dat data.
/// </summary>
public sealed partial class DamageParticleEffectsDat
{
    /// <summary> Gets DamageParticleEffectTypes.</summary>
    /// <remarks> references <see cref="DamageParticleEffectTypesDat"/> on <see cref="Specification.LoadDamageParticleEffectTypesDat"/> index.</remarks>
    public required int DamageParticleEffectTypes { get; init; }

    /// <summary> Gets Variation.</summary>
    public required int Variation { get; init; }

    /// <summary> Gets PETFile.</summary>
    public required string PETFile { get; init; }

    /// <summary> Gets ImpactSoundData1.</summary>
    /// <remarks> references <see cref="ImpactSoundDataDat"/> on <see cref="Specification.LoadImpactSoundDataDat"/> index.</remarks>
    public required int? ImpactSoundData1 { get; init; }

    /// <summary> Gets ImpactSoundData2.</summary>
    /// <remarks> references <see cref="ImpactSoundDataDat"/> on <see cref="Specification.LoadImpactSoundDataDat"/> index.</remarks>
    public required int? ImpactSoundData2 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }
}
