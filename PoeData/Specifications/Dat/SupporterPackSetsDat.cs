// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing SupporterPackSets.dat data.
/// </summary>
public sealed partial class SupporterPackSetsDat : IDat<SupporterPackSetsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets FormatTitle.</summary>
    public required string FormatTitle { get; init; }

    /// <summary> Gets Background.</summary>
    public required string Background { get; init; }

    /// <summary> Gets Time0.</summary>
    public required string Time0 { get; init; }

    /// <summary> Gets Time1.</summary>
    public required string Time1 { get; init; }

    /// <summary> Gets ShopPackagePlatform.</summary>
    /// <remarks> references <see cref="ShopPackagePlatformDat"/> on <see cref="Specification.GetShopPackagePlatformDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ShopPackagePlatform { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required string Unknown56 { get; init; }

    /// <inheritdoc/>
    public static SupporterPackSetsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/SupporterPackSets.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SupporterPackSetsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FormatTitle
            (var formattitleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Background
            (var backgroundLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Time0
            (var time0Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Time1
            (var time1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShopPackagePlatform
            (var tempshoppackageplatformLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var shoppackageplatformLoading = tempshoppackageplatformLoading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SupporterPackSetsDat()
            {
                Id = idLoading,
                FormatTitle = formattitleLoading,
                Background = backgroundLoading,
                Time0 = time0Loading,
                Time1 = time1Loading,
                ShopPackagePlatform = shoppackageplatformLoading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
