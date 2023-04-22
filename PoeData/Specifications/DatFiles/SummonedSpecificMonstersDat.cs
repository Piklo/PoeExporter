// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SummonedSpecificMonsters.dat data.
/// </summary>
public sealed partial class SummonedSpecificMonstersDat
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets a value indicating whether Unknown40 is set.</summary>
    public required bool Unknown40 { get; init; }

    /// <summary> Gets a value indicating whether Unknown41 is set.</summary>
    public required bool Unknown41 { get; init; }

    /// <summary> Gets Unknown42.</summary>
    public required int Unknown42 { get; init; }

    /// <summary> Gets Unknown46.</summary>
    public required int Unknown46 { get; init; }

    /// <summary> Gets a value indicating whether Unknown50 is set.</summary>
    public required bool Unknown50 { get; init; }

    /// <summary> Gets Unknown51.</summary>
    public required int? Unknown51 { get; init; }

    /// <summary> Gets Unknown67.</summary>
    public required int? Unknown67 { get; init; }

    /// <summary> Gets Unknown83.</summary>
    public required int Unknown83 { get; init; }

    /// <summary> Gets a value indicating whether Unknown87 is set.</summary>
    public required bool Unknown87 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int Unknown88 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required string Unknown92 { get; init; }

    /// <summary> Gets a value indicating whether Unknown100 is set.</summary>
    public required bool Unknown100 { get; init; }

    /// <summary> Gets a value indicating whether Unknown101 is set.</summary>
    public required bool Unknown101 { get; init; }

    /// <summary> Gets Unknown102.</summary>
    public required int Unknown102 { get; init; }

    /// <summary>
    /// Gets SummonedSpecificMonstersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SummonedSpecificMonstersDat.</returns>
    internal static SummonedSpecificMonstersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SummonedSpecificMonsters.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SummonedSpecificMonstersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown51
            (var unknown51Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown67
            (var unknown67Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown83
            (var unknown83Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown102
            (var unknown102Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SummonedSpecificMonstersDat()
            {
                Id = idLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown41 = unknown41Loading,
                Unknown42 = unknown42Loading,
                Unknown46 = unknown46Loading,
                Unknown50 = unknown50Loading,
                Unknown51 = unknown51Loading,
                Unknown67 = unknown67Loading,
                Unknown83 = unknown83Loading,
                Unknown87 = unknown87Loading,
                Unknown88 = unknown88Loading,
                Unknown92 = unknown92Loading,
                Unknown100 = unknown100Loading,
                Unknown101 = unknown101Loading,
                Unknown102 = unknown102Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
