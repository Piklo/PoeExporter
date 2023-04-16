// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AfflictionStartDialogue.dat data.
/// </summary>
public sealed partial class AfflictionStartDialogueDat
{
    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets NPCTextAudioKey.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudioKey { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required ReadOnlyCollection<int> Unknown32 { get; init; }

    /// <summary>
    /// Gets AfflictionStartDialogueDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AfflictionStartDialogueDat.</returns>
    internal static AfflictionStartDialogueDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AfflictionStartDialogue.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AfflictionStartDialogueDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NPCTextAudioKey
            (var npctextaudiokeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var tempunknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown32Loading = tempunknown32Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AfflictionStartDialogueDat()
            {
                WorldAreasKey = worldareaskeyLoading,
                NPCTextAudioKey = npctextaudiokeyLoading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
