// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing PassiveTreeExpansionJewels.dat data.
/// </summary>
public sealed partial class PassiveTreeExpansionJewelsDat : ISpecificationFile<PassiveTreeExpansionJewelsDat>
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets PassiveTreeExpansionJewelSizesKey.</summary>
    public required int? PassiveTreeExpansionJewelSizesKey { get; init; }

    /// <summary> Gets MinNodes.</summary>
    public required int MinNodes { get; init; }

    /// <summary> Gets MaxNodes.</summary>
    public required int MaxNodes { get; init; }

    /// <summary> Gets SmallIndices.</summary>
    public required ReadOnlyCollection<int> SmallIndices { get; init; }

    /// <summary> Gets NotableIndices.</summary>
    public required ReadOnlyCollection<int> NotableIndices { get; init; }

    /// <summary> Gets SocketIndices.</summary>
    public required ReadOnlyCollection<int> SocketIndices { get; init; }

    /// <summary> Gets TotalIndices.</summary>
    public required int TotalIndices { get; init; }

    /// <inheritdoc/>
    public static PassiveTreeExpansionJewelsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/PassiveTreeExpansionJewels.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveTreeExpansionJewelsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();
            // specification.GetPassiveTreeExpansionJewelSizesDat();

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PassiveTreeExpansionJewelSizesKey
            (var passivetreeexpansionjewelsizeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MinNodes
            (var minnodesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxNodes
            (var maxnodesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SmallIndices
            (var tempsmallindicesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var smallindicesLoading = tempsmallindicesLoading.AsReadOnly();

            // loading NotableIndices
            (var tempnotableindicesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var notableindicesLoading = tempnotableindicesLoading.AsReadOnly();

            // loading SocketIndices
            (var tempsocketindicesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var socketindicesLoading = tempsocketindicesLoading.AsReadOnly();

            // loading TotalIndices
            (var totalindicesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveTreeExpansionJewelsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                PassiveTreeExpansionJewelSizesKey = passivetreeexpansionjewelsizeskeyLoading,
                MinNodes = minnodesLoading,
                MaxNodes = maxnodesLoading,
                SmallIndices = smallindicesLoading,
                NotableIndices = notableindicesLoading,
                SocketIndices = socketindicesLoading,
                TotalIndices = totalindicesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
