// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HideoutNPCs.dat data.
/// </summary>
public sealed partial class HideoutNPCsDat
{
    /// <summary> Gets Hideout_NPCsKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required int? Hideout_NPCsKey { get; init; }

    /// <summary> Gets Regular_NPCsKeys.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Regular_NPCsKeys { get; init; }

    /// <summary> Gets HideoutDoodadsKey.</summary>
    /// <remarks> references <see cref="HideoutDoodadsDat"/> on <see cref="Specification.LoadHideoutDoodadsDat"/> index.</remarks>
    public required int? HideoutDoodadsKey { get; init; }

    /// <summary> Gets NPCMasterKey.</summary>
    public required int NPCMasterKey { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int? Unknown52 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int? Unknown68 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required int Unknown84 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int? Unknown88 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required int Unknown104 { get; init; }

    /// <summary> Gets a value indicating whether Unknown108 is set.</summary>
    public required bool Unknown108 { get; init; }

    /// <summary> Gets Unknown109.</summary>
    public required int? Unknown109 { get; init; }

    /// <summary>
    /// Gets HideoutNPCsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HideoutNPCsDat.</returns>
    internal static HideoutNPCsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HideoutNPCs.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HideoutNPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Hideout_NPCsKey
            (var hideout_npcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Regular_NPCsKeys
            (var tempregular_npcskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var regular_npcskeysLoading = tempregular_npcskeysLoading.AsReadOnly();

            // loading HideoutDoodadsKey
            (var hideoutdoodadskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading NPCMasterKey
            (var npcmasterkeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HideoutNPCsDat()
            {
                Hideout_NPCsKey = hideout_npcskeyLoading,
                Regular_NPCsKeys = regular_npcskeysLoading,
                HideoutDoodadsKey = hideoutdoodadskeyLoading,
                NPCMasterKey = npcmasterkeyLoading,
                Unknown52 = unknown52Loading,
                Unknown68 = unknown68Loading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
                Unknown108 = unknown108Loading,
                Unknown109 = unknown109Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
