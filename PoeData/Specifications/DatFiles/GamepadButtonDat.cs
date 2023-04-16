// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GamepadButton.dat data.
/// </summary>
public sealed partial class GamepadButtonDat
{
    /// <summary> Gets Unknown0.</summary>
    public required string Unknown0 { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required string Unknown8 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required string Unknown16 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required string Unknown24 { get; init; }

    /// <summary>
    /// Gets GamepadButtonDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of GamepadButtonDat.</returns>
    internal static GamepadButtonDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/GamepadButton.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GamepadButtonDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GamepadButtonDat()
            {
                Unknown0 = unknown0Loading,
                Unknown8 = unknown8Loading,
                Unknown16 = unknown16Loading,
                Unknown24 = unknown24Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
