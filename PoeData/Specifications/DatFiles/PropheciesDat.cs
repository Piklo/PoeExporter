// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Prophecies.dat data.
/// </summary>
public sealed partial class PropheciesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets PredictionText.</summary>
    public required string PredictionText { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets FlavourText.</summary>
    public required string FlavourText { get; init; }

    /// <summary> Gets QuestTracker_ClientStringsKeys.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> QuestTracker_ClientStringsKeys { get; init; }

    /// <summary> Gets OGGFile.</summary>
    public required string OGGFile { get; init; }

    /// <summary> Gets ProphecyChainKey.</summary>
    /// <remarks> references <see cref="ProphecyChainDat"/> on <see cref="Specification.LoadProphecyChainDat"/> index.</remarks>
    public required int? ProphecyChainKey { get; init; }

    /// <summary> Gets ProphecyChainPosition.</summary>
    public required int ProphecyChainPosition { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }

    /// <summary> Gets SealCost.</summary>
    public required int SealCost { get; init; }

    /// <summary> Gets PredictionText2.</summary>
    public required string PredictionText2 { get; init; }

    /// <summary>
    /// Gets PropheciesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PropheciesDat.</returns>
    internal static PropheciesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Prophecies.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PropheciesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PredictionText
            (var predictiontextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading QuestTracker_ClientStringsKeys
            (var tempquesttracker_clientstringskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var questtracker_clientstringskeysLoading = tempquesttracker_clientstringskeysLoading.AsReadOnly();

            // loading OGGFile
            (var oggfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ProphecyChainKey
            (var prophecychainkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ProphecyChainPosition
            (var prophecychainpositionLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SealCost
            (var sealcostLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PredictionText2
            (var predictiontext2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PropheciesDat()
            {
                Id = idLoading,
                PredictionText = predictiontextLoading,
                Unknown16 = unknown16Loading,
                Name = nameLoading,
                FlavourText = flavourtextLoading,
                QuestTracker_ClientStringsKeys = questtracker_clientstringskeysLoading,
                OGGFile = oggfileLoading,
                ProphecyChainKey = prophecychainkeyLoading,
                ProphecyChainPosition = prophecychainpositionLoading,
                IsEnabled = isenabledLoading,
                SealCost = sealcostLoading,
                PredictionText2 = predictiontext2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
