// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing TableCharge.dat data.
/// </summary>
public sealed partial class TableChargeDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required float Unknown4 { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required float Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown12 is set.</summary>
    public required bool Unknown12 { get; init; }

    /// <summary> Gets Unknown13.</summary>
    public required int? Unknown13 { get; init; }

    /// <summary> Gets a value indicating whether Unknown29 is set.</summary>
    public required bool Unknown29 { get; init; }

    /// <summary> Gets Unknown30.</summary>
    public required ReadOnlyCollection<int> Unknown30 { get; init; }

    /// <summary> Gets Unknown46.</summary>
    public required int? Unknown46 { get; init; }

    /// <summary> Gets Unknown62.</summary>
    public required int Unknown62 { get; init; }

    /// <summary> Gets Unknown66.</summary>
    public required int Unknown66 { get; init; }

    /// <summary> Gets Unknown70.</summary>
    public required int Unknown70 { get; init; }

    /// <summary> Gets Unknown74.</summary>
    public required int Unknown74 { get; init; }

    /// <summary> Gets a value indicating whether Unknown78 is set.</summary>
    public required bool Unknown78 { get; init; }

    /// <summary> Gets a value indicating whether Unknown79 is set.</summary>
    public required bool Unknown79 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int? Unknown80 { get; init; }

    /// <summary> Gets Unknown96.</summary>
    public required int? Unknown96 { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required int Unknown112 { get; init; }

    /// <summary> Gets a value indicating whether Unknown116 is set.</summary>
    public required bool Unknown116 { get; init; }

    /// <summary> Gets Unknown117.</summary>
    public required int Unknown117 { get; init; }

    /// <summary> Gets Unknown121.</summary>
    public required int Unknown121 { get; init; }

    /// <summary> Gets Unknown125.</summary>
    public required int Unknown125 { get; init; }

    /// <summary> Gets Unknown129.</summary>
    public required int Unknown129 { get; init; }

    /// <summary> Gets Unknown133.</summary>
    public required int Unknown133 { get; init; }

    /// <summary> Gets Unknown137.</summary>
    public required int Unknown137 { get; init; }

    /// <summary> Gets Unknown141.</summary>
    public required int Unknown141 { get; init; }

    /// <summary> Gets a value indicating whether Unknown145 is set.</summary>
    public required bool Unknown145 { get; init; }

    /// <summary> Gets a value indicating whether Unknown146 is set.</summary>
    public required bool Unknown146 { get; init; }

    /// <summary> Gets Unknown147.</summary>
    public required int Unknown147 { get; init; }

    /// <inheritdoc/>
    public static TableChargeDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/TableCharge.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TableChargeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown13
            (var unknown13Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown30
            (var tempunknown30Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown30Loading = tempunknown30Loading.AsReadOnly();

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown121
            (var unknown121Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown125
            (var unknown125Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown129
            (var unknown129Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown133
            (var unknown133Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown137
            (var unknown137Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown145
            (var unknown145Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown146
            (var unknown146Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown147
            (var unknown147Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TableChargeDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown13 = unknown13Loading,
                Unknown29 = unknown29Loading,
                Unknown30 = unknown30Loading,
                Unknown46 = unknown46Loading,
                Unknown62 = unknown62Loading,
                Unknown66 = unknown66Loading,
                Unknown70 = unknown70Loading,
                Unknown74 = unknown74Loading,
                Unknown78 = unknown78Loading,
                Unknown79 = unknown79Loading,
                Unknown80 = unknown80Loading,
                Unknown96 = unknown96Loading,
                Unknown112 = unknown112Loading,
                Unknown116 = unknown116Loading,
                Unknown117 = unknown117Loading,
                Unknown121 = unknown121Loading,
                Unknown125 = unknown125Loading,
                Unknown129 = unknown129Loading,
                Unknown133 = unknown133Loading,
                Unknown137 = unknown137Loading,
                Unknown141 = unknown141Loading,
                Unknown145 = unknown145Loading,
                Unknown146 = unknown146Loading,
                Unknown147 = unknown147Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
