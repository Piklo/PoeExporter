// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing GeometryAttack.dat data.
/// </summary>
public sealed partial class GeometryAttackDat
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

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets Unknown57.</summary>
    public required int Unknown57 { get; init; }

    /// <summary> Gets Unknown61.</summary>
    public required int Unknown61 { get; init; }

    /// <summary> Gets Unknown65.</summary>
    public required int Unknown65 { get; init; }

    /// <summary> Gets a value indicating whether Unknown69 is set.</summary>
    public required bool Unknown69 { get; init; }

    /// <summary> Gets a value indicating whether Unknown70 is set.</summary>
    public required bool Unknown70 { get; init; }

    /// <summary> Gets Unknown71.</summary>
    public required ReadOnlyCollection<int> Unknown71 { get; init; }

    /// <summary> Gets Unknown87.</summary>
    public required int Unknown87 { get; init; }

    /// <summary> Gets Unknown91.</summary>
    public required int Unknown91 { get; init; }

    /// <summary> Gets Unknown95.</summary>
    public required int Unknown95 { get; init; }

    /// <summary> Gets Unknown99.</summary>
    public required int Unknown99 { get; init; }

    /// <summary> Gets Unknown103.</summary>
    public required int Unknown103 { get; init; }

    /// <summary> Gets Unknown107.</summary>
    public required int Unknown107 { get; init; }

    /// <summary> Gets a value indicating whether Unknown111 is set.</summary>
    public required bool Unknown111 { get; init; }

    /// <summary> Gets a value indicating whether Unknown112 is set.</summary>
    public required bool Unknown112 { get; init; }

    /// <summary> Gets Unknown113.</summary>
    public required int Unknown113 { get; init; }

    /// <summary> Gets Unknown117.</summary>
    public required int Unknown117 { get; init; }

    /// <summary> Gets a value indicating whether Unknown121 is set.</summary>
    public required bool Unknown121 { get; init; }

    /// <summary> Gets Unknown122.</summary>
    public required int Unknown122 { get; init; }

    /// <summary> Gets a value indicating whether Unknown126 is set.</summary>
    public required bool Unknown126 { get; init; }

    /// <summary> Gets Unknown127.</summary>
    public required int? Unknown127 { get; init; }

    /// <summary> Gets Unknown143.</summary>
    public required ReadOnlyCollection<int> Unknown143 { get; init; }

    /// <summary> Gets Unknown159.</summary>
    public required int Unknown159 { get; init; }

    /// <summary> Gets a value indicating whether Unknown163 is set.</summary>
    public required bool Unknown163 { get; init; }

    /// <summary> Gets a value indicating whether Unknown164 is set.</summary>
    public required bool Unknown164 { get; init; }

    /// <summary> Gets Unknown165.</summary>
    public required int? Unknown165 { get; init; }

    /// <summary> Gets a value indicating whether Unknown181 is set.</summary>
    public required bool Unknown181 { get; init; }

    /// <summary> Gets Unknown182.</summary>
    public required ReadOnlyCollection<int> Unknown182 { get; init; }

    /// <summary> Gets a value indicating whether Unknown198 is set.</summary>
    public required bool Unknown198 { get; init; }

    /// <summary> Gets a value indicating whether Unknown199 is set.</summary>
    public required bool Unknown199 { get; init; }

    /// <summary> Gets Unknown200.</summary>
    public required int? Unknown200 { get; init; }

    /// <summary> Gets a value indicating whether Unknown216 is set.</summary>
    public required bool Unknown216 { get; init; }

    /// <summary> Gets a value indicating whether Unknown217 is set.</summary>
    public required bool Unknown217 { get; init; }

    /// <summary> Gets Unknown218.</summary>
    public required ReadOnlyCollection<int> Unknown218 { get; init; }

    /// <summary> Gets a value indicating whether Unknown234 is set.</summary>
    public required bool Unknown234 { get; init; }

    /// <summary> Gets a value indicating whether Unknown235 is set.</summary>
    public required bool Unknown235 { get; init; }

    /// <summary> Gets a value indicating whether Unknown236 is set.</summary>
    public required bool Unknown236 { get; init; }

    /// <summary> Gets a value indicating whether Unknown237 is set.</summary>
    public required bool Unknown237 { get; init; }

    /// <summary>
    /// Gets GeometryAttackDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of GeometryAttackDat.</returns>
    internal static GeometryAttackDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/GeometryAttack.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GeometryAttackDat[tableRows];
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
            (var tempunknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown20Loading = tempunknown20Loading.AsReadOnly();

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown57
            (var unknown57Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown71
            (var tempunknown71Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown71Loading = tempunknown71Loading.AsReadOnly();

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown95
            (var unknown95Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown103
            (var unknown103Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown107
            (var unknown107Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown111
            (var unknown111Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown121
            (var unknown121Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown122
            (var unknown122Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown126
            (var unknown126Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown127
            (var unknown127Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown143
            (var tempunknown143Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown143Loading = tempunknown143Loading.AsReadOnly();

            // loading Unknown159
            (var unknown159Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown163
            (var unknown163Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown165
            (var unknown165Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown181
            (var unknown181Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown182
            (var tempunknown182Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown182Loading = tempunknown182Loading.AsReadOnly();

            // loading Unknown198
            (var unknown198Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown199
            (var unknown199Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown200
            (var unknown200Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown216
            (var unknown216Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown217
            (var unknown217Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown218
            (var tempunknown218Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown218Loading = tempunknown218Loading.AsReadOnly();

            // loading Unknown234
            (var unknown234Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown235
            (var unknown235Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown236
            (var unknown236Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown237
            (var unknown237Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GeometryAttackDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown57 = unknown57Loading,
                Unknown61 = unknown61Loading,
                Unknown65 = unknown65Loading,
                Unknown69 = unknown69Loading,
                Unknown70 = unknown70Loading,
                Unknown71 = unknown71Loading,
                Unknown87 = unknown87Loading,
                Unknown91 = unknown91Loading,
                Unknown95 = unknown95Loading,
                Unknown99 = unknown99Loading,
                Unknown103 = unknown103Loading,
                Unknown107 = unknown107Loading,
                Unknown111 = unknown111Loading,
                Unknown112 = unknown112Loading,
                Unknown113 = unknown113Loading,
                Unknown117 = unknown117Loading,
                Unknown121 = unknown121Loading,
                Unknown122 = unknown122Loading,
                Unknown126 = unknown126Loading,
                Unknown127 = unknown127Loading,
                Unknown143 = unknown143Loading,
                Unknown159 = unknown159Loading,
                Unknown163 = unknown163Loading,
                Unknown164 = unknown164Loading,
                Unknown165 = unknown165Loading,
                Unknown181 = unknown181Loading,
                Unknown182 = unknown182Loading,
                Unknown198 = unknown198Loading,
                Unknown199 = unknown199Loading,
                Unknown200 = unknown200Loading,
                Unknown216 = unknown216Loading,
                Unknown217 = unknown217Loading,
                Unknown218 = unknown218Loading,
                Unknown234 = unknown234Loading,
                Unknown235 = unknown235Loading,
                Unknown236 = unknown236Loading,
                Unknown237 = unknown237Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
