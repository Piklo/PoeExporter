﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MavenDialog.dat data.
/// </summary>
public sealed partial class MavenDialogDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets TextAudioT1.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudioT1 { get; init; }

    /// <summary> Gets TextAudioT2.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudioT2 { get; init; }

    /// <summary> Gets TextAudioT3.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudioT3 { get; init; }

    /// <summary> Gets TextAudioT4.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudioT4 { get; init; }

    /// <summary> Gets TextAudioT5.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudioT5 { get; init; }

    /// <summary> Gets a value indicating whether Unknown88 is set.</summary>
    public required bool Unknown88 { get; init; }

    /// <summary> Gets TextAudioT6.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudioT6 { get; init; }

    /// <summary>
    /// Gets MavenDialogDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MavenDialogDat.</returns>
    internal static MavenDialogDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MavenDialog.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MavenDialogDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TextAudioT1
            (var textaudiot1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading TextAudioT2
            (var textaudiot2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading TextAudioT3
            (var textaudiot3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading TextAudioT4
            (var textaudiot4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading TextAudioT5
            (var textaudiot5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TextAudioT6
            (var textaudiot6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MavenDialogDat()
            {
                Id = idLoading,
                TextAudioT1 = textaudiot1Loading,
                TextAudioT2 = textaudiot2Loading,
                TextAudioT3 = textaudiot3Loading,
                TextAudioT4 = textaudiot4Loading,
                TextAudioT5 = textaudiot5Loading,
                Unknown88 = unknown88Loading,
                TextAudioT6 = textaudiot6Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
