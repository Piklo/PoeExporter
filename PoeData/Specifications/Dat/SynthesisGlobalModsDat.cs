// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing SynthesisGlobalMods.dat data.
/// </summary>
public sealed partial class SynthesisGlobalModsDat : IDat<SynthesisGlobalModsDat>
{
    /// <summary> Gets ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? ModsKey { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <inheritdoc/>
    public static SynthesisGlobalModsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/SynthesisGlobalMods.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SynthesisGlobalModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SynthesisGlobalModsDat()
            {
                ModsKey = modskeyLoading,
                Weight = weightLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
