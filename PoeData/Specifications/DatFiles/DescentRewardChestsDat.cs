// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DescentRewardChests.dat data.
/// </summary>
public sealed partial class DescentRewardChestsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BaseItemTypesKeys1.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys1 { get; init; }

    /// <summary> Gets BaseItemTypesKeys2.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys2 { get; init; }

    /// <summary> Gets BaseItemTypesKeys3.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys3 { get; init; }

    /// <summary> Gets BaseItemTypesKeys4.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys4 { get; init; }

    /// <summary> Gets BaseItemTypesKeys5.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys5 { get; init; }

    /// <summary> Gets BaseItemTypesKeys6.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys6 { get; init; }

    /// <summary> Gets BaseItemTypesKeys7.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys7 { get; init; }

    /// <summary> Gets BaseItemTypesKeys8.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys8 { get; init; }

    /// <summary> Gets BaseItemTypesKeys9.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys9 { get; init; }

    /// <summary> Gets BaseItemTypesKeys10.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys10 { get; init; }

    /// <summary> Gets BaseItemTypesKeys11.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys11 { get; init; }

    /// <summary> Gets BaseItemTypesKeys12.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys12 { get; init; }

    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets BaseItemTypesKeys13.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys13 { get; init; }

    /// <summary> Gets BaseItemTypesKeys14.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys14 { get; init; }

    /// <summary>
    /// Gets DescentRewardChestsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of DescentRewardChestsDat.</returns>
    internal static DescentRewardChestsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/DescentRewardChests.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DescentRewardChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKeys1
            (var tempbaseitemtypeskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys1Loading = tempbaseitemtypeskeys1Loading.AsReadOnly();

            // loading BaseItemTypesKeys2
            (var tempbaseitemtypeskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys2Loading = tempbaseitemtypeskeys2Loading.AsReadOnly();

            // loading BaseItemTypesKeys3
            (var tempbaseitemtypeskeys3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys3Loading = tempbaseitemtypeskeys3Loading.AsReadOnly();

            // loading BaseItemTypesKeys4
            (var tempbaseitemtypeskeys4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys4Loading = tempbaseitemtypeskeys4Loading.AsReadOnly();

            // loading BaseItemTypesKeys5
            (var tempbaseitemtypeskeys5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys5Loading = tempbaseitemtypeskeys5Loading.AsReadOnly();

            // loading BaseItemTypesKeys6
            (var tempbaseitemtypeskeys6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys6Loading = tempbaseitemtypeskeys6Loading.AsReadOnly();

            // loading BaseItemTypesKeys7
            (var tempbaseitemtypeskeys7Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys7Loading = tempbaseitemtypeskeys7Loading.AsReadOnly();

            // loading BaseItemTypesKeys8
            (var tempbaseitemtypeskeys8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys8Loading = tempbaseitemtypeskeys8Loading.AsReadOnly();

            // loading BaseItemTypesKeys9
            (var tempbaseitemtypeskeys9Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys9Loading = tempbaseitemtypeskeys9Loading.AsReadOnly();

            // loading BaseItemTypesKeys10
            (var tempbaseitemtypeskeys10Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys10Loading = tempbaseitemtypeskeys10Loading.AsReadOnly();

            // loading BaseItemTypesKeys11
            (var tempbaseitemtypeskeys11Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys11Loading = tempbaseitemtypeskeys11Loading.AsReadOnly();

            // loading BaseItemTypesKeys12
            (var tempbaseitemtypeskeys12Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys12Loading = tempbaseitemtypeskeys12Loading.AsReadOnly();

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKeys13
            (var tempbaseitemtypeskeys13Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys13Loading = tempbaseitemtypeskeys13Loading.AsReadOnly();

            // loading BaseItemTypesKeys14
            (var tempbaseitemtypeskeys14Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys14Loading = tempbaseitemtypeskeys14Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DescentRewardChestsDat()
            {
                Id = idLoading,
                BaseItemTypesKeys1 = baseitemtypeskeys1Loading,
                BaseItemTypesKeys2 = baseitemtypeskeys2Loading,
                BaseItemTypesKeys3 = baseitemtypeskeys3Loading,
                BaseItemTypesKeys4 = baseitemtypeskeys4Loading,
                BaseItemTypesKeys5 = baseitemtypeskeys5Loading,
                BaseItemTypesKeys6 = baseitemtypeskeys6Loading,
                BaseItemTypesKeys7 = baseitemtypeskeys7Loading,
                BaseItemTypesKeys8 = baseitemtypeskeys8Loading,
                BaseItemTypesKeys9 = baseitemtypeskeys9Loading,
                BaseItemTypesKeys10 = baseitemtypeskeys10Loading,
                BaseItemTypesKeys11 = baseitemtypeskeys11Loading,
                BaseItemTypesKeys12 = baseitemtypeskeys12Loading,
                WorldAreasKey = worldareaskeyLoading,
                BaseItemTypesKeys13 = baseitemtypeskeys13Loading,
                BaseItemTypesKeys14 = baseitemtypeskeys14Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
