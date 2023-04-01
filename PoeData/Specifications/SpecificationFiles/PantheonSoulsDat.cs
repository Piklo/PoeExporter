// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing PantheonSouls.dat data.
/// </summary>
public sealed partial class PantheonSoulsDat : ISpecificationFile<PantheonSoulsDat>
{
    /// <summary> Gets WorldArea.</summary>
    public required int? WorldArea { get; init; }

    /// <summary> Gets CapturedVessel.</summary>
    public required int? CapturedVessel { get; init; }

    /// <summary> Gets QuestFlag.</summary>
    public required int? QuestFlag { get; init; }

    /// <summary> Gets CapturedMonster.</summary>
    public required int? CapturedMonster { get; init; }

    /// <summary> Gets PanelLayout.</summary>
    public required int? PanelLayout { get; init; }

    /// <inheritdoc/>
    public static PantheonSoulsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/PantheonSouls.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PantheonSoulsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetWorldAreasDat();
            // specification.GetBaseItemTypesDat();
            // specification.GetQuestFlagsDat();
            // specification.GetMonsterVarietiesDat();
            // specification.GetPantheonPanelLayoutDat();

            // loading WorldArea
            (var worldareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading CapturedVessel
            (var capturedvesselLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading CapturedMonster
            (var capturedmonsterLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PanelLayout
            (var panellayoutLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PantheonSoulsDat()
            {
                WorldArea = worldareaLoading,
                CapturedVessel = capturedvesselLoading,
                QuestFlag = questflagLoading,
                CapturedMonster = capturedmonsterLoading,
                PanelLayout = panellayoutLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
