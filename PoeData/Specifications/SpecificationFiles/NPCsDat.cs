﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing NPCs.dat data.
/// </summary>
public sealed partial class NPCsDat : ISpecificationFile<NPCsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Metadata.</summary>
    public required string Metadata { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets NPCMasterKey.</summary>
    public required int? NPCMasterKey { get; init; }

    /// <summary> Gets ShortName.</summary>
    public required string ShortName { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets NPCAudios1.</summary>
    public required ReadOnlyCollection<int> NPCAudios1 { get; init; }

    /// <summary> Gets NPCAudios2.</summary>
    public required ReadOnlyCollection<int> NPCAudios2 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required int Unknown100 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required int? Unknown104 { get; init; }

    /// <summary> Gets Portrait.</summary>
    public required int? Portrait { get; init; }

    /// <summary> Gets DialogueStyle.</summary>
    public required int? DialogueStyle { get; init; }

    /// <summary> Gets a value indicating whether Unknown144 is set.</summary>
    public required bool Unknown144 { get; init; }

    /// <summary> Gets Unknown145.</summary>
    public required int? Unknown145 { get; init; }

    /// <inheritdoc/>
    public static NPCsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/NPCs.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetNPCMasterDat();
            // specification.GetNPCAudioDat();
            // specification.GetNPCPortraitsDat();
            // specification.GetNPCDialogueStylesDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Metadata
            (var metadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NPCMasterKey
            (var npcmasterkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ShortName
            (var shortnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading NPCAudios1
            (var tempnpcaudios1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npcaudios1Loading = tempnpcaudios1Loading.AsReadOnly();

            // loading NPCAudios2
            (var tempnpcaudios2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npcaudios2Loading = tempnpcaudios2Loading.AsReadOnly();

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Portrait
            (var portraitLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading DialogueStyle
            (var dialoguestyleLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown145
            (var unknown145Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCsDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Metadata = metadataLoading,
                Unknown24 = unknown24Loading,
                NPCMasterKey = npcmasterkeyLoading,
                ShortName = shortnameLoading,
                Unknown64 = unknown64Loading,
                NPCAudios1 = npcaudios1Loading,
                NPCAudios2 = npcaudios2Loading,
                Unknown100 = unknown100Loading,
                Unknown104 = unknown104Loading,
                Portrait = portraitLoading,
                DialogueStyle = dialoguestyleLoading,
                Unknown144 = unknown144Loading,
                Unknown145 = unknown145Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}