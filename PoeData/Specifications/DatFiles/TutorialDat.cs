// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Tutorial.dat data.
/// </summary>
public sealed partial class TutorialDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets UIFile.</summary>
    public required string UIFile { get; init; }

    /// <summary> Gets ClientString.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? ClientString { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }

    /// <summary> Gets Unknown33.</summary>
    public required int Unknown33 { get; init; }

    /// <summary> Gets Unknown37.</summary>
    public required ReadOnlyCollection<int> Unknown37 { get; init; }

    /// <summary> Gets Unknown53.</summary>
    public required int? Unknown53 { get; init; }

    /// <summary> Gets Unknown69.</summary>
    public required int Unknown69 { get; init; }

    /// <summary> Gets Unknown73.</summary>
    public required ReadOnlyCollection<int> Unknown73 { get; init; }

    /// <summary> Gets a value indicating whether Unknown89 is set.</summary>
    public required bool Unknown89 { get; init; }

    /// <summary> Gets a value indicating whether Unknown90 is set.</summary>
    public required bool Unknown90 { get; init; }

    /// <summary> Gets Unknown91.</summary>
    public required int Unknown91 { get; init; }

    /// <summary>
    /// Gets TutorialDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of TutorialDat.</returns>
    internal static TutorialDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Tutorial.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TutorialDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading UIFile
            (var uifileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ClientString
            (var clientstringLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown37
            (var tempunknown37Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown37Loading = tempunknown37Loading.AsReadOnly();

            // loading Unknown53
            (var unknown53Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown73
            (var tempunknown73Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown73Loading = tempunknown73Loading.AsReadOnly();

            // loading Unknown89
            (var unknown89Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown90
            (var unknown90Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TutorialDat()
            {
                Id = idLoading,
                UIFile = uifileLoading,
                ClientString = clientstringLoading,
                IsEnabled = isenabledLoading,
                Unknown33 = unknown33Loading,
                Unknown37 = unknown37Loading,
                Unknown53 = unknown53Loading,
                Unknown69 = unknown69Loading,
                Unknown73 = unknown73Loading,
                Unknown89 = unknown89Loading,
                Unknown90 = unknown90Loading,
                Unknown91 = unknown91Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
