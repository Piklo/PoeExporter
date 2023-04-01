// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing HeistRevealingNPCs.dat data.
/// </summary>
public sealed partial class HeistRevealingNPCsDat : ISpecificationFile<HeistRevealingNPCsDat>
{
    /// <summary> Gets NPCsKey.</summary>
    public required int? NPCsKey { get; init; }

    /// <summary> Gets PortraitFile.</summary>
    public required string PortraitFile { get; init; }

    /// <summary> Gets NPCAudioKey.</summary>
    public required ReadOnlyCollection<int> NPCAudioKey { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <inheritdoc/>
    public static HeistRevealingNPCsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/HeistRevealingNPCs.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistRevealingNPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetNPCsDat();
            // specification.GetNPCAudioDat();

            // loading NPCsKey
            (var npcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PortraitFile
            (var portraitfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NPCAudioKey
            (var tempnpcaudiokeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npcaudiokeyLoading = tempnpcaudiokeyLoading.AsReadOnly();

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistRevealingNPCsDat()
            {
                NPCsKey = npcskeyLoading,
                PortraitFile = portraitfileLoading,
                NPCAudioKey = npcaudiokeyLoading,
                Unknown40 = unknown40Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
