// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing LeagueProgressQuestFlags.dat data.
/// </summary>
public sealed partial class LeagueProgressQuestFlagsDat
{
    /// <summary> Gets QuestFlag.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? QuestFlag { get; init; }

    /// <summary> Gets CompletionString.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.GetClientStringsDat"/> index.</remarks>
    public required int? CompletionString { get; init; }

    /// <summary> Gets Boss.</summary>
    public required string Boss { get; init; }

    /// <summary> Gets a value indicating whether Unknown40 is set.</summary>
    public required bool Unknown40 { get; init; }

    /// <inheritdoc/>
    public static LeagueProgressQuestFlagsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/LeagueProgressQuestFlags.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LeagueProgressQuestFlagsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading CompletionString
            (var completionstringLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Boss
            (var bossLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LeagueProgressQuestFlagsDat()
            {
                QuestFlag = questflagLoading,
                CompletionString = completionstringLoading,
                Boss = bossLoading,
                Unknown40 = unknown40Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
