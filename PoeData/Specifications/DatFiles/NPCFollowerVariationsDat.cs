// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCFollowerVariations.dat data.
/// </summary>
public sealed partial class NPCFollowerVariationsDat
{
    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets MiscAnimatedKey0.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey0 { get; init; }

    /// <summary> Gets MiscAnimatedKey1.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey1 { get; init; }

    /// <summary> Gets a value indicating whether Unknown48 is set.</summary>
    public required bool Unknown48 { get; init; }

    /// <summary> Gets a value indicating whether Unknown49 is set.</summary>
    public required bool Unknown49 { get; init; }

    /// <summary> Gets Unknown50.</summary>
    public required int Unknown50 { get; init; }

    /// <summary> Gets Unknown54.</summary>
    public required int Unknown54 { get; init; }

    /// <summary> Gets Unknown58.</summary>
    public required int Unknown58 { get; init; }

    /// <summary> Gets Unknown62.</summary>
    public required int Unknown62 { get; init; }

    /// <summary> Gets a value indicating whether Unknown66 is set.</summary>
    public required bool Unknown66 { get; init; }

    /// <summary> Gets a value indicating whether Unknown67 is set.</summary>
    public required bool Unknown67 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required ReadOnlyCollection<int> Unknown68 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required ReadOnlyCollection<int> Unknown84 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required int Unknown100 { get; init; }

    /// <summary> Gets a value indicating whether Unknown104 is set.</summary>
    public required bool Unknown104 { get; init; }

    /// <summary> Gets a value indicating whether Unknown105 is set.</summary>
    public required bool Unknown105 { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required int Unknown106 { get; init; }

    /// <summary>
    /// Gets NPCFollowerVariationsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of NPCFollowerVariationsDat.</returns>
    internal static NPCFollowerVariationsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/NPCFollowerVariations.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCFollowerVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimatedKey0
            (var miscanimatedkey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimatedKey1
            (var miscanimatedkey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown54
            (var unknown54Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown58
            (var unknown58Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown67
            (var unknown67Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown68
            (var tempunknown68Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown68Loading = tempunknown68Loading.AsReadOnly();

            // loading Unknown84
            (var tempunknown84Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown84Loading = tempunknown84Loading.AsReadOnly();

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCFollowerVariationsDat()
            {
                MonsterVarietiesKey = monstervarietieskeyLoading,
                MiscAnimatedKey0 = miscanimatedkey0Loading,
                MiscAnimatedKey1 = miscanimatedkey1Loading,
                Unknown48 = unknown48Loading,
                Unknown49 = unknown49Loading,
                Unknown50 = unknown50Loading,
                Unknown54 = unknown54Loading,
                Unknown58 = unknown58Loading,
                Unknown62 = unknown62Loading,
                Unknown66 = unknown66Loading,
                Unknown67 = unknown67Loading,
                Unknown68 = unknown68Loading,
                Unknown84 = unknown84Loading,
                Unknown100 = unknown100Loading,
                Unknown104 = unknown104Loading,
                Unknown105 = unknown105Loading,
                Unknown106 = unknown106Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
