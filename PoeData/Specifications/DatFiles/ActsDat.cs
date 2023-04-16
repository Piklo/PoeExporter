// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Acts.dat data.
/// </summary>
public sealed partial class ActsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Part.</summary>
    public required int Part { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required string Unknown12 { get; init; }

    /// <summary> Gets ActNumber.</summary>
    public required int ActNumber { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets WorldPanelImage.</summary>
    public required string WorldPanelImage { get; init; }

    /// <summary> Gets WorldPanelImageEpilogue.</summary>
    public required string WorldPanelImageEpilogue { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether IsPostGame is set.</summary>
    public required bool IsPostGame { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required int Unknown49 { get; init; }

    /// <summary> Gets Unknown53.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown53 { get; init; }

    /// <summary>
    /// Gets ActsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ActsDat.</returns>
    internal static ActsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Acts.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ActsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Part
            (var partLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ActNumber
            (var actnumberLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WorldPanelImage
            (var worldpanelimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WorldPanelImageEpilogue
            (var worldpanelimageepilogueLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsPostGame
            (var ispostgameLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown53
            (var tempunknown53Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown53Loading = tempunknown53Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ActsDat()
            {
                Id = idLoading,
                Part = partLoading,
                Unknown12 = unknown12Loading,
                ActNumber = actnumberLoading,
                Unknown24 = unknown24Loading,
                WorldPanelImage = worldpanelimageLoading,
                WorldPanelImageEpilogue = worldpanelimageepilogueLoading,
                Unknown44 = unknown44Loading,
                IsPostGame = ispostgameLoading,
                Unknown49 = unknown49Loading,
                Unknown53 = unknown53Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
