// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MonsterDeathConditions.dat data.
/// </summary>
public sealed partial class MonsterDeathConditionsDat
{
    /// <summary> Gets Unknown0.</summary>
    public required string Unknown0 { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required ReadOnlyCollection<int> Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown24 is set.</summary>
    public required bool Unknown24 { get; init; }

    /// <summary> Gets Unknown25.</summary>
    public required int Unknown25 { get; init; }

    /// <summary> Gets Unknown29.</summary>
    public required ReadOnlyCollection<int> Unknown29 { get; init; }

    /// <summary> Gets a value indicating whether Unknown45 is set.</summary>
    public required bool Unknown45 { get; init; }

    /// <summary> Gets Unknown46.</summary>
    public required int Unknown46 { get; init; }

    /// <summary> Gets Unknown50.</summary>
    public required int? Unknown50 { get; init; }

    /// <summary> Gets a value indicating whether Unknown66 is set.</summary>
    public required bool Unknown66 { get; init; }

    /// <summary> Gets Unknown67.</summary>
    public required ReadOnlyCollection<int> Unknown67 { get; init; }

    /// <summary> Gets Unknown83.</summary>
    public required int Unknown83 { get; init; }

    /// <summary> Gets a value indicating whether Unknown87 is set.</summary>
    public required bool Unknown87 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required ReadOnlyCollection<int> Unknown88 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required int Unknown104 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required int? Unknown108 { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required int? Unknown124 { get; init; }

    /// <summary> Gets Unknown140.</summary>
    public required int Unknown140 { get; init; }

    /// <summary> Gets Unknown144.</summary>
    public required int Unknown144 { get; init; }

    /// <inheritdoc/>
    public static MonsterDeathConditionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/MonsterDeathConditions.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterDeathConditionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var tempunknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown8Loading = tempunknown8Loading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown29
            (var tempunknown29Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown29Loading = tempunknown29Loading.AsReadOnly();

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown67
            (var tempunknown67Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown67Loading = tempunknown67Loading.AsReadOnly();

            // loading Unknown83
            (var unknown83Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown88
            (var tempunknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown88Loading = tempunknown88Loading.AsReadOnly();

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown140
            (var unknown140Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterDeathConditionsDat()
            {
                Unknown0 = unknown0Loading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown25 = unknown25Loading,
                Unknown29 = unknown29Loading,
                Unknown45 = unknown45Loading,
                Unknown46 = unknown46Loading,
                Unknown50 = unknown50Loading,
                Unknown66 = unknown66Loading,
                Unknown67 = unknown67Loading,
                Unknown83 = unknown83Loading,
                Unknown87 = unknown87Loading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
                Unknown108 = unknown108Loading,
                Unknown124 = unknown124Loading,
                Unknown140 = unknown140Loading,
                Unknown144 = unknown144Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
