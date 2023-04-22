// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BetrayalUpgrades.dat data.
/// </summary>
public sealed partial class BetrayalUpgradesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKey { get; init; }

    /// <summary> Gets ArtFile.</summary>
    public required string ArtFile { get; init; }

    /// <summary> Gets BetrayalUpgradeSlotsKey.</summary>
    public required int BetrayalUpgradeSlotsKey { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required ReadOnlyCollection<int> Unknown52 { get; init; }

    /// <summary> Gets ItemVisualIdentityKey0.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey0 { get; init; }

    /// <summary> Gets ItemVisualIdentityKey1.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey1 { get; init; }

    /// <summary> Gets GrantedEffectsKey.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required int? GrantedEffectsKey { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required int Unknown116 { get; init; }

    /// <summary> Gets ItemClassesKey.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required int? ItemClassesKey { get; init; }

    /// <summary>
    /// Gets BetrayalUpgradesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BetrayalUpgradesDat.</returns>
    internal static BetrayalUpgradesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BetrayalUpgrades.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalUpgradesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKey
            (var tempmodskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeyLoading = tempmodskeyLoading.AsReadOnly();

            // loading ArtFile
            (var artfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BetrayalUpgradeSlotsKey
            (var betrayalupgradeslotskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var tempunknown52Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown52Loading = tempunknown52Loading.AsReadOnly();

            // loading ItemVisualIdentityKey0
            (var itemvisualidentitykey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemVisualIdentityKey1
            (var itemvisualidentitykey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading GrantedEffectsKey
            (var grantedeffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemClassesKey
            (var itemclasseskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalUpgradesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Description = descriptionLoading,
                ModsKey = modskeyLoading,
                ArtFile = artfileLoading,
                BetrayalUpgradeSlotsKey = betrayalupgradeslotskeyLoading,
                Unknown52 = unknown52Loading,
                ItemVisualIdentityKey0 = itemvisualidentitykey0Loading,
                ItemVisualIdentityKey1 = itemvisualidentitykey1Loading,
                GrantedEffectsKey = grantedeffectskeyLoading,
                Unknown116 = unknown116Loading,
                ItemClassesKey = itemclasseskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
