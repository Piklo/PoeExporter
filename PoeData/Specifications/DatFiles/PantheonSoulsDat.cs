// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PantheonSouls.dat data.
/// </summary>
public sealed partial class PantheonSoulsDat
{
    /// <summary> Gets WorldArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldArea { get; init; }

    /// <summary> Gets CapturedVessel.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? CapturedVessel { get; init; }

    /// <summary> Gets QuestFlag.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? QuestFlag { get; init; }

    /// <summary> Gets CapturedMonster.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? CapturedMonster { get; init; }

    /// <summary> Gets PanelLayout.</summary>
    /// <remarks> references <see cref="PantheonPanelLayoutDat"/> on <see cref="Specification.GetPantheonPanelLayoutDat"/> index.</remarks>
    public required int? PanelLayout { get; init; }

    /// <summary>
    /// Gets PantheonSoulsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PantheonSoulsDat.</returns>
    internal static PantheonSoulsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PantheonSouls.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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
