// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing GroundEffects.dat data.
/// </summary>
public sealed partial class GroundEffectsDat : IDat<GroundEffectsDat>
{
    /// <summary> Gets GroundEffectTypesKey.</summary>
    /// <remarks> references <see cref="GroundEffectTypesDat"/> on <see cref="Specification.GetGroundEffectTypesDat"/> index.</remarks>
    public required int? GroundEffectTypesKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required float Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required ReadOnlyCollection<int> Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether Unknown60 is set.</summary>
    public required bool Unknown60 { get; init; }

    /// <summary> Gets AOFile.</summary>
    public required ReadOnlyCollection<string> AOFile { get; init; }

    /// <summary> Gets Unknown77.</summary>
    public required ReadOnlyCollection<string> Unknown77 { get; init; }

    /// <summary> Gets EndEffect.</summary>
    public required string EndEffect { get; init; }

    /// <summary> Gets Unknown101.</summary>
    public required int? Unknown101 { get; init; }

    /// <summary> Gets Unknown117.</summary>
    public required int? Unknown117 { get; init; }

    /// <summary> Gets Unknown133.</summary>
    public required int? Unknown133 { get; init; }

    /// <summary> Gets Unknown149.</summary>
    public required int? Unknown149 { get; init; }

    /// <summary> Gets Unknown165.</summary>
    public required int? Unknown165 { get; init; }

    /// <summary> Gets a value indicating whether Unknown181 is set.</summary>
    public required bool Unknown181 { get; init; }

    /// <summary> Gets a value indicating whether Unknown182 is set.</summary>
    public required bool Unknown182 { get; init; }

    /// <summary> Gets a value indicating whether Unknown183 is set.</summary>
    public required bool Unknown183 { get; init; }

    /// <summary> Gets Unknown184.</summary>
    public required int? Unknown184 { get; init; }

    /// <summary> Gets Unknown200.</summary>
    public required int? Unknown200 { get; init; }

    /// <summary> Gets a value indicating whether Unknown216 is set.</summary>
    public required bool Unknown216 { get; init; }

    /// <summary> Gets a value indicating whether Unknown217 is set.</summary>
    public required bool Unknown217 { get; init; }

    /// <inheritdoc/>
    public static GroundEffectsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/GroundEffects.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GroundEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading GroundEffectTypesKey
            (var groundeffecttypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown44
            (var tempunknown44Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown44Loading = tempunknown44Loading.AsReadOnly();

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AOFile
            (var tempaofileLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofileLoading = tempaofileLoading.AsReadOnly();

            // loading Unknown77
            (var tempunknown77Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown77Loading = tempunknown77Loading.AsReadOnly();

            // loading EndEffect
            (var endeffectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown133
            (var unknown133Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown165
            (var unknown165Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown181
            (var unknown181Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown182
            (var unknown182Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown183
            (var unknown183Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown184
            (var unknown184Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown200
            (var unknown200Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown216
            (var unknown216Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown217
            (var unknown217Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GroundEffectsDat()
            {
                GroundEffectTypesKey = groundeffecttypeskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown60 = unknown60Loading,
                AOFile = aofileLoading,
                Unknown77 = unknown77Loading,
                EndEffect = endeffectLoading,
                Unknown101 = unknown101Loading,
                Unknown117 = unknown117Loading,
                Unknown133 = unknown133Loading,
                Unknown149 = unknown149Loading,
                Unknown165 = unknown165Loading,
                Unknown181 = unknown181Loading,
                Unknown182 = unknown182Loading,
                Unknown183 = unknown183Loading,
                Unknown184 = unknown184Loading,
                Unknown200 = unknown200Loading,
                Unknown216 = unknown216Loading,
                Unknown217 = unknown217Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
