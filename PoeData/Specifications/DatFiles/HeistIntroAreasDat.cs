﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistIntroAreas.dat data.
/// </summary>
public sealed partial class HeistIntroAreasDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HeistAreasKey.</summary>
    /// <remarks> references <see cref="HeistAreasDat"/> on <see cref="Specification.LoadHeistAreasDat"/> index.</remarks>
    public required int? HeistAreasKey { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets DGRFile.</summary>
    public required string DGRFile { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether Unknown48 is set.</summary>
    public required bool Unknown48 { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required int Unknown49 { get; init; }

    /// <summary> Gets Unknown53.</summary>
    public required int Unknown53 { get; init; }

    /// <summary> Gets Unknown57.</summary>
    public required int Unknown57 { get; init; }
}
