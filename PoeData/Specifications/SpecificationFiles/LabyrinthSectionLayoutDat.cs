﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing LabyrinthSectionLayout.dat data.
/// </summary>
public sealed partial class LabyrinthSectionLayoutDat : ISpecificationFile<LabyrinthSectionLayoutDat>
{
    /// <summary> Gets LabyrinthSectionKey.</summary>
    public required int? LabyrinthSectionKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets LabyrinthSectionLayoutKeys.</summary>
    public required ReadOnlyCollection<int> LabyrinthSectionLayoutKeys { get; init; }

    /// <summary> Gets LabyrinthSecretsKey0.</summary>
    public required int? LabyrinthSecretsKey0 { get; init; }

    /// <summary> Gets LabyrinthSecretsKey1.</summary>
    public required int? LabyrinthSecretsKey1 { get; init; }

    /// <summary> Gets LabyrinthAreasKey.</summary>
    public required int? LabyrinthAreasKey { get; init; }

    /// <summary> Gets Float0.</summary>
    public required float Float0 { get; init; }

    /// <summary> Gets Float1.</summary>
    public required float Float1 { get; init; }

    /// <summary> Gets LabyrinthNodeOverridesKeys.</summary>
    public required ReadOnlyCollection<int> LabyrinthNodeOverridesKeys { get; init; }

    /// <inheritdoc/>
    public static LabyrinthSectionLayoutDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/LabyrinthSectionLayout.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthSectionLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetLabyrinthSectionDat();
            // specification.GetLabyrinthSectionLayoutDat();
            // specification.GetLabyrinthSecretsDat();
            // specification.GetLabyrinthAreasDat();
            // specification.GetLabyrinthNodeOverridesDat();

            // loading LabyrinthSectionKey
            (var labyrinthsectionkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LabyrinthSectionLayoutKeys
            (var templabyrinthsectionlayoutkeysLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsectionlayoutkeysLoading = templabyrinthsectionlayoutkeysLoading.AsReadOnly();

            // loading LabyrinthSecretsKey0
            (var labyrinthsecretskey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading LabyrinthSecretsKey1
            (var labyrinthsecretskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading LabyrinthAreasKey
            (var labyrinthareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Float0
            (var float0Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Float1
            (var float1Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading LabyrinthNodeOverridesKeys
            (var templabyrinthnodeoverrideskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthnodeoverrideskeysLoading = templabyrinthnodeoverrideskeysLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthSectionLayoutDat()
            {
                LabyrinthSectionKey = labyrinthsectionkeyLoading,
                Unknown16 = unknown16Loading,
                LabyrinthSectionLayoutKeys = labyrinthsectionlayoutkeysLoading,
                LabyrinthSecretsKey0 = labyrinthsecretskey0Loading,
                LabyrinthSecretsKey1 = labyrinthsecretskey1Loading,
                LabyrinthAreasKey = labyrinthareaskeyLoading,
                Float0 = float0Loading,
                Float1 = float1Loading,
                LabyrinthNodeOverridesKeys = labyrinthnodeoverrideskeysLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}