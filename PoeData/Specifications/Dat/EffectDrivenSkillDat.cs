// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing EffectDrivenSkill.dat data.
/// </summary>
public sealed partial class EffectDrivenSkillDat : IDat<EffectDrivenSkillDat>
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required ReadOnlyCollection<int> Unknown4 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required ReadOnlyCollection<int> Unknown20 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether Unknown45 is set.</summary>
    public required bool Unknown45 { get; init; }

    /// <summary> Gets a value indicating whether Unknown46 is set.</summary>
    public required bool Unknown46 { get; init; }

    /// <summary> Gets Unknown47.</summary>
    public required ReadOnlyCollection<int> Unknown47 { get; init; }

    /// <summary> Gets Unknown63.</summary>
    public required int Unknown63 { get; init; }

    /// <summary> Gets Unknown67.</summary>
    public required int Unknown67 { get; init; }

    /// <summary> Gets Unknown71.</summary>
    public required int Unknown71 { get; init; }

    /// <summary> Gets Unknown75.</summary>
    public required int Unknown75 { get; init; }

    /// <summary> Gets Unknown79.</summary>
    public required int Unknown79 { get; init; }

    /// <summary> Gets a value indicating whether Unknown83 is set.</summary>
    public required bool Unknown83 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required int Unknown84 { get; init; }

    /// <summary> Gets a value indicating whether Unknown88 is set.</summary>
    public required bool Unknown88 { get; init; }

    /// <summary> Gets a value indicating whether Unknown89 is set.</summary>
    public required bool Unknown89 { get; init; }

    /// <summary> Gets Unknown90.</summary>
    public required int Unknown90 { get; init; }

    /// <summary> Gets Unknown94.</summary>
    public required int Unknown94 { get; init; }

    /// <summary> Gets a value indicating whether Unknown98 is set.</summary>
    public required bool Unknown98 { get; init; }

    /// <summary> Gets a value indicating whether Unknown99 is set.</summary>
    public required bool Unknown99 { get; init; }

    /// <summary> Gets a value indicating whether Unknown100 is set.</summary>
    public required bool Unknown100 { get; init; }

    /// <summary> Gets Unknown101.</summary>
    public required int Unknown101 { get; init; }

    /// <summary> Gets a value indicating whether Unknown105 is set.</summary>
    public required bool Unknown105 { get; init; }

    /// <summary> Gets a value indicating whether Unknown106 is set.</summary>
    public required bool Unknown106 { get; init; }

    /// <summary> Gets Unknown107.</summary>
    public required int Unknown107 { get; init; }

    /// <summary> Gets Unknown111.</summary>
    public required int Unknown111 { get; init; }

    /// <summary> Gets Unknown115.</summary>
    public required int Unknown115 { get; init; }

    /// <inheritdoc/>
    public static EffectDrivenSkillDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/EffectDrivenSkill.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EffectDrivenSkillDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var tempunknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown4Loading = tempunknown4Loading.AsReadOnly();

            // loading Unknown20
            (var tempunknown20Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown20Loading = tempunknown20Loading.AsReadOnly();

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown47
            (var tempunknown47Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown47Loading = tempunknown47Loading.AsReadOnly();

            // loading Unknown63
            (var unknown63Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown67
            (var unknown67Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown71
            (var unknown71Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown75
            (var unknown75Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown83
            (var unknown83Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown89
            (var unknown89Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown90
            (var unknown90Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown94
            (var unknown94Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown98
            (var unknown98Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown107
            (var unknown107Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown111
            (var unknown111Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EffectDrivenSkillDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown45 = unknown45Loading,
                Unknown46 = unknown46Loading,
                Unknown47 = unknown47Loading,
                Unknown63 = unknown63Loading,
                Unknown67 = unknown67Loading,
                Unknown71 = unknown71Loading,
                Unknown75 = unknown75Loading,
                Unknown79 = unknown79Loading,
                Unknown83 = unknown83Loading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown89 = unknown89Loading,
                Unknown90 = unknown90Loading,
                Unknown94 = unknown94Loading,
                Unknown98 = unknown98Loading,
                Unknown99 = unknown99Loading,
                Unknown100 = unknown100Loading,
                Unknown101 = unknown101Loading,
                Unknown105 = unknown105Loading,
                Unknown106 = unknown106Loading,
                Unknown107 = unknown107Loading,
                Unknown111 = unknown111Loading,
                Unknown115 = unknown115Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
