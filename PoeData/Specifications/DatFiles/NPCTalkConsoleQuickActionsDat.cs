// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCTalkConsoleQuickActions.dat data.
/// </summary>
public sealed partial class NPCTalkConsoleQuickActionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Controller.</summary>
    public required string Controller { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required string Unknown16 { get; init; }

    /// <summary>
    /// Gets NPCTalkConsoleQuickActionsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of NPCTalkConsoleQuickActionsDat.</returns>
    internal static NPCTalkConsoleQuickActionsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/NPCTalkConsoleQuickActions.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCTalkConsoleQuickActionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Controller
            (var controllerLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCTalkConsoleQuickActionsDat()
            {
                Id = idLoading,
                Controller = controllerLoading,
                Unknown16 = unknown16Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
