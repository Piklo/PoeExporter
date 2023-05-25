// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MicrotransactionSlot.dat data.
/// </summary>
public sealed partial class MicrotransactionSlotDat
{
    /// <summary> Gets Id.</summary>
    /// <remarks> references <see cref="MicrotransactionSlotIdDat"/> on <see cref="Specification.LoadMicrotransactionSlotIdDat"/> index.</remarks>
    public required int Id { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int? Unknown4 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }
}
