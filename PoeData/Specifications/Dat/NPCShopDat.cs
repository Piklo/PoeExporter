// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing NPCShop.dat data.
/// </summary>
public sealed partial class NPCShopDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets SoldItem_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SoldItem_TagsKeys { get; init; }

    /// <summary> Gets SoldItem_Weights.</summary>
    public required ReadOnlyCollection<int> SoldItem_Weights { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required ReadOnlyCollection<int> Unknown44 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required ReadOnlyCollection<int> Unknown60 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required ReadOnlyCollection<int> Unknown76 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required ReadOnlyCollection<int> Unknown92 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required int Unknown108 { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required int Unknown112 { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required int Unknown116 { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required ReadOnlyCollection<int> Unknown120 { get; init; }

    /// <summary> Gets Unknown136.</summary>
    public required int Unknown136 { get; init; }

    /// <summary> Gets Unknown140.</summary>
    public required int Unknown140 { get; init; }

    /// <summary>
    /// Gets NPCShopDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of NPCShopDat.</returns>
    internal static NPCShopDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/NPCShop.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCShopDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SoldItem_TagsKeys
            (var tempsolditem_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var solditem_tagskeysLoading = tempsolditem_tagskeysLoading.AsReadOnly();

            // loading SoldItem_Weights
            (var tempsolditem_weightsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var solditem_weightsLoading = tempsolditem_weightsLoading.AsReadOnly();

            // loading Unknown44
            (var tempunknown44Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown44Loading = tempunknown44Loading.AsReadOnly();

            // loading Unknown60
            (var tempunknown60Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown60Loading = tempunknown60Loading.AsReadOnly();

            // loading Unknown76
            (var tempunknown76Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown76Loading = tempunknown76Loading.AsReadOnly();

            // loading Unknown92
            (var tempunknown92Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown92Loading = tempunknown92Loading.AsReadOnly();

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown120
            (var tempunknown120Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown120Loading = tempunknown120Loading.AsReadOnly();

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown140
            (var unknown140Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCShopDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                SoldItem_TagsKeys = solditem_tagskeysLoading,
                SoldItem_Weights = solditem_weightsLoading,
                Unknown44 = unknown44Loading,
                Unknown60 = unknown60Loading,
                Unknown76 = unknown76Loading,
                Unknown92 = unknown92Loading,
                Unknown108 = unknown108Loading,
                Unknown112 = unknown112Loading,
                Unknown116 = unknown116Loading,
                Unknown120 = unknown120Loading,
                Unknown136 = unknown136Loading,
                Unknown140 = unknown140Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
