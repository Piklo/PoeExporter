// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemisedVisualEffect.dat data.
/// </summary>
public sealed partial class ItemisedVisualEffectDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets ItemVisualEffectKey.</summary>
    /// <remarks> references <see cref="ItemVisualEffectDat"/> on <see cref="Specification.GetItemVisualEffectDat"/> index.</remarks>
    public required int? ItemVisualEffectKey { get; init; }

    /// <summary> Gets ItemVisualIdentityKey1.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.GetItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey1 { get; init; }

    /// <summary> Gets ItemVisualIdentityKey2.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.GetItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey2 { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets ItemClasses.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.GetItemClassesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ItemClasses { get; init; }

    /// <summary> Gets Unknown96.</summary>
    public required ReadOnlyCollection<int> Unknown96 { get; init; }

    /// <summary> Gets a value indicating whether Unknown112 is set.</summary>
    public required bool Unknown112 { get; init; }

    /// <summary> Gets Unknown113.</summary>
    public required ReadOnlyCollection<int> Unknown113 { get; init; }

    /// <summary> Gets Unknown129.</summary>
    public required ReadOnlyCollection<int> Unknown129 { get; init; }

    /// <summary> Gets a value indicating whether Unknown145 is set.</summary>
    public required bool Unknown145 { get; init; }

    /// <summary> Gets Unknown146.</summary>
    public required ReadOnlyCollection<int> Unknown146 { get; init; }

    /// <summary> Gets a value indicating whether Unknown162 is set.</summary>
    public required bool Unknown162 { get; init; }

    /// <summary>
    /// Gets ItemisedVisualEffectDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ItemisedVisualEffectDat.</returns>
    internal static ItemisedVisualEffectDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ItemisedVisualEffect.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemisedVisualEffectDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ItemVisualEffectKey
            (var itemvisualeffectkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ItemVisualIdentityKey1
            (var itemvisualidentitykey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ItemVisualIdentityKey2
            (var itemvisualidentitykey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading ItemClasses
            (var tempitemclassesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var itemclassesLoading = tempitemclassesLoading.AsReadOnly();

            // loading Unknown96
            (var tempunknown96Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown96Loading = tempunknown96Loading.AsReadOnly();

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown113
            (var tempunknown113Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown113Loading = tempunknown113Loading.AsReadOnly();

            // loading Unknown129
            (var tempunknown129Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown129Loading = tempunknown129Loading.AsReadOnly();

            // loading Unknown145
            (var unknown145Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown146
            (var tempunknown146Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown146Loading = tempunknown146Loading.AsReadOnly();

            // loading Unknown162
            (var unknown162Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemisedVisualEffectDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                ItemVisualEffectKey = itemvisualeffectkeyLoading,
                ItemVisualIdentityKey1 = itemvisualidentitykey1Loading,
                ItemVisualIdentityKey2 = itemvisualidentitykey2Loading,
                Stats = statsLoading,
                ItemClasses = itemclassesLoading,
                Unknown96 = unknown96Loading,
                Unknown112 = unknown112Loading,
                Unknown113 = unknown113Loading,
                Unknown129 = unknown129Loading,
                Unknown145 = unknown145Loading,
                Unknown146 = unknown146Loading,
                Unknown162 = unknown162Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
