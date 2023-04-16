// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GeometryTrigger.dat data.
/// </summary>
public sealed partial class GeometryTriggerDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int? Unknown4 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }

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

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int Unknown60 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required int Unknown76 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int Unknown80 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required int Unknown84 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int Unknown88 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required int Unknown92 { get; init; }

    /// <summary> Gets a value indicating whether Unknown96 is set.</summary>
    public required bool Unknown96 { get; init; }

    /// <summary> Gets Unknown97.</summary>
    public required int Unknown97 { get; init; }

    /// <summary> Gets a value indicating whether Unknown101 is set.</summary>
    public required bool Unknown101 { get; init; }

    /// <summary> Gets Unknown102.</summary>
    public required int Unknown102 { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required int Unknown106 { get; init; }

    /// <summary> Gets a value indicating whether Unknown110 is set.</summary>
    public required bool Unknown110 { get; init; }

    /// <summary> Gets Unknown111.</summary>
    public required int Unknown111 { get; init; }

    /// <summary> Gets Unknown115.</summary>
    public required ReadOnlyCollection<int> Unknown115 { get; init; }

    /// <summary> Gets Unknown131.</summary>
    public required int Unknown131 { get; init; }

    /// <summary> Gets a value indicating whether Unknown135 is set.</summary>
    public required bool Unknown135 { get; init; }

    /// <summary> Gets Unknown136.</summary>
    public required int Unknown136 { get; init; }

    /// <summary> Gets a value indicating whether Unknown140 is set.</summary>
    public required bool Unknown140 { get; init; }

    /// <summary> Gets a value indicating whether Unknown141 is set.</summary>
    public required bool Unknown141 { get; init; }

    /// <summary> Gets Unknown142.</summary>
    public required int Unknown142 { get; init; }

    /// <summary> Gets a value indicating whether Unknown146 is set.</summary>
    public required bool Unknown146 { get; init; }

    /// <summary> Gets a value indicating whether Unknown147 is set.</summary>
    public required bool Unknown147 { get; init; }

    /// <summary> Gets Unknown148.</summary>
    public required int? Unknown148 { get; init; }

    /// <summary> Gets a value indicating whether Unknown164 is set.</summary>
    public required bool Unknown164 { get; init; }

    /// <summary> Gets a value indicating whether Unknown165 is set.</summary>
    public required bool Unknown165 { get; init; }

    /// <summary> Gets a value indicating whether Unknown166 is set.</summary>
    public required bool Unknown166 { get; init; }

    /// <summary> Gets Unknown167.</summary>
    public required int Unknown167 { get; init; }

    /// <summary> Gets Unknown171.</summary>
    public required int Unknown171 { get; init; }

    /// <summary> Gets Unknown175.</summary>
    public required ReadOnlyCollection<int> Unknown175 { get; init; }

    /// <summary> Gets Unknown191.</summary>
    public required ReadOnlyCollection<int> Unknown191 { get; init; }

    /// <summary> Gets Unknown207.</summary>
    public required int Unknown207 { get; init; }

    /// <summary> Gets Unknown211.</summary>
    public required int Unknown211 { get; init; }

    /// <summary> Gets Unknown215.</summary>
    public required int Unknown215 { get; init; }

    /// <summary> Gets a value indicating whether Unknown219 is set.</summary>
    public required bool Unknown219 { get; init; }

    /// <summary>
    /// Gets GeometryTriggerDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of GeometryTriggerDat.</returns>
    internal static GeometryTriggerDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/GeometryTrigger.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GeometryTriggerDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

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
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown97
            (var unknown97Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown102
            (var unknown102Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown110
            (var unknown110Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown111
            (var unknown111Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown115
            (var tempunknown115Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown115Loading = tempunknown115Loading.AsReadOnly();

            // loading Unknown131
            (var unknown131Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown135
            (var unknown135Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown140
            (var unknown140Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown146
            (var unknown146Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown147
            (var unknown147Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown165
            (var unknown165Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown166
            (var unknown166Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown167
            (var unknown167Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown171
            (var unknown171Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown175
            (var tempunknown175Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown175Loading = tempunknown175Loading.AsReadOnly();

            // loading Unknown191
            (var tempunknown191Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown191Loading = tempunknown191Loading.AsReadOnly();

            // loading Unknown207
            (var unknown207Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown211
            (var unknown211Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown215
            (var unknown215Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown219
            (var unknown219Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GeometryTriggerDat()
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
                Unknown60 = unknown60Loading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown92 = unknown92Loading,
                Unknown96 = unknown96Loading,
                Unknown97 = unknown97Loading,
                Unknown101 = unknown101Loading,
                Unknown102 = unknown102Loading,
                Unknown106 = unknown106Loading,
                Unknown110 = unknown110Loading,
                Unknown111 = unknown111Loading,
                Unknown115 = unknown115Loading,
                Unknown131 = unknown131Loading,
                Unknown135 = unknown135Loading,
                Unknown136 = unknown136Loading,
                Unknown140 = unknown140Loading,
                Unknown141 = unknown141Loading,
                Unknown142 = unknown142Loading,
                Unknown146 = unknown146Loading,
                Unknown147 = unknown147Loading,
                Unknown148 = unknown148Loading,
                Unknown164 = unknown164Loading,
                Unknown165 = unknown165Loading,
                Unknown166 = unknown166Loading,
                Unknown167 = unknown167Loading,
                Unknown171 = unknown171Loading,
                Unknown175 = unknown175Loading,
                Unknown191 = unknown191Loading,
                Unknown207 = unknown207Loading,
                Unknown211 = unknown211Loading,
                Unknown215 = unknown215Loading,
                Unknown219 = unknown219Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
