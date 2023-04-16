// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BetrayalDialogue.dat data.
/// </summary>
public sealed partial class BetrayalDialogueDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required ReadOnlyCollection<int> Unknown24 { get; init; }

    /// <summary> Gets BetrayalTargetsKey.</summary>
    /// <remarks> references <see cref="BetrayalTargetsDat"/> on <see cref="Specification.GetBetrayalTargetsDat"/> index.</remarks>
    public required int? BetrayalTargetsKey { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int? Unknown60 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required ReadOnlyCollection<int> Unknown76 { get; init; }

    /// <summary> Gets BetrayalUpgradesKey.</summary>
    /// <remarks> references <see cref="BetrayalUpgradesDat"/> on <see cref="Specification.GetBetrayalUpgradesDat"/> index.</remarks>
    public required int? BetrayalUpgradesKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown108 is set.</summary>
    public required bool Unknown108 { get; init; }

    /// <summary> Gets Unknown109.</summary>
    public required ReadOnlyCollection<int> Unknown109 { get; init; }

    /// <summary> Gets Unknown125.</summary>
    public required ReadOnlyCollection<int> Unknown125 { get; init; }

    /// <summary> Gets a value indicating whether Unknown141 is set.</summary>
    public required bool Unknown141 { get; init; }

    /// <summary> Gets Unknown142.</summary>
    public required ReadOnlyCollection<int> Unknown142 { get; init; }

    /// <summary> Gets NPCTextAudioKey.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudioKey { get; init; }

    /// <summary> Gets Unknown174.</summary>
    public required ReadOnlyCollection<int> Unknown174 { get; init; }

    /// <summary>
    /// Gets BetrayalDialogueDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BetrayalDialogueDat.</returns>
    internal static BetrayalDialogueDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BetrayalDialogue.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalDialogueDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            // loading BetrayalTargetsKey
            (var betrayaltargetskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown76
            (var tempunknown76Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown76Loading = tempunknown76Loading.AsReadOnly();

            // loading BetrayalUpgradesKey
            (var betrayalupgradeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown109
            (var tempunknown109Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown109Loading = tempunknown109Loading.AsReadOnly();

            // loading Unknown125
            (var tempunknown125Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown125Loading = tempunknown125Loading.AsReadOnly();

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown142
            (var tempunknown142Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown142Loading = tempunknown142Loading.AsReadOnly();

            // loading NPCTextAudioKey
            (var npctextaudiokeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown174
            (var tempunknown174Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown174Loading = tempunknown174Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalDialogueDat()
            {
                Unknown0 = unknown0Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                BetrayalTargetsKey = betrayaltargetskeyLoading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown76 = unknown76Loading,
                BetrayalUpgradesKey = betrayalupgradeskeyLoading,
                Unknown108 = unknown108Loading,
                Unknown109 = unknown109Loading,
                Unknown125 = unknown125Loading,
                Unknown141 = unknown141Loading,
                Unknown142 = unknown142Loading,
                NPCTextAudioKey = npctextaudiokeyLoading,
                Unknown174 = unknown174Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
