// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TalkingPetNPCAudio.dat data.
/// </summary>
public sealed partial class TalkingPetNPCAudioDat
{
    /// <summary> Gets Unknown0.</summary>
    /// <remarks> references <see cref="TalkingPetAudioEventsDat"/> on <see cref="Specification.LoadTalkingPetAudioEventsDat"/> index.</remarks>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    /// <remarks> references <see cref="TalkingPetsDat"/> on <see cref="Specification.LoadTalkingPetsDat"/> index.</remarks>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required ReadOnlyCollection<int> Unknown32 { get; init; }
}
