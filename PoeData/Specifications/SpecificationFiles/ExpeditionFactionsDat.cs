// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing ExpeditionFactions.dat data.
/// </summary>
public sealed partial class ExpeditionFactionsDat : ISpecificationFile<ExpeditionFactionsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets FactionFlag.</summary>
    public required string FactionFlag { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets FactionIcon.</summary>
    public required string FactionIcon { get; init; }

    /// <summary> Gets MonsterVarieties.</summary>
    public required int? MonsterVarieties { get; init; }

    /// <summary> Gets Progress1.</summary>
    public required int? Progress1 { get; init; }

    /// <summary> Gets Progress2Vaal.</summary>
    public required int? Progress2Vaal { get; init; }

    /// <summary> Gets Progress3Final.</summary>
    public required int? Progress3Final { get; init; }

    /// <summary> Gets Tags.</summary>
    public required int? Tags { get; init; }

    /// <inheritdoc/>
    public static ExpeditionFactionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ExpeditionFactions.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionFactionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetMonsterVarietiesDat();
            // specification.GetNPCTextAudioDat();
            // specification.GetTagsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FactionFlag
            (var factionflagLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FactionIcon
            (var factioniconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterVarieties
            (var monstervarietiesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Progress1
            (var progress1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Progress2Vaal
            (var progress2vaalLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Progress3Final
            (var progress3finalLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Tags
            (var tagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionFactionsDat()
            {
                Id = idLoading,
                Name = nameLoading,
                FactionFlag = factionflagLoading,
                Unknown24 = unknown24Loading,
                FactionIcon = factioniconLoading,
                MonsterVarieties = monstervarietiesLoading,
                Progress1 = progress1Loading,
                Progress2Vaal = progress2vaalLoading,
                Progress3Final = progress3finalLoading,
                Tags = tagsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
