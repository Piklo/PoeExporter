// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ShopPaymentPackage.dat data.
/// </summary>
public sealed partial class ShopPaymentPackageDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets Coins.</summary>
    public required int Coins { get; init; }

    /// <summary> Gets a value indicating whether AvailableFlag is set.</summary>
    public required bool AvailableFlag { get; init; }

    /// <summary> Gets Unknown21.</summary>
    public required int Unknown21 { get; init; }

    /// <summary> Gets Unknown25.</summary>
    public required int Unknown25 { get; init; }

    /// <summary> Gets a value indicating whether Unknown29 is set.</summary>
    public required bool Unknown29 { get; init; }

    /// <summary> Gets a value indicating whether ContainsBetaKey is set.</summary>
    public required bool ContainsBetaKey { get; init; }

    /// <summary> Gets Unknown31.</summary>
    public required ReadOnlyCollection<int> Unknown31 { get; init; }

    /// <summary> Gets Unknown47.</summary>
    public required int? Unknown47 { get; init; }

    /// <summary> Gets BackgroundImage.</summary>
    public required string BackgroundImage { get; init; }

    /// <summary> Gets Unknown71.</summary>
    public required string Unknown71 { get; init; }

    /// <summary> Gets a value indicating whether Unknown79 is set.</summary>
    public required bool Unknown79 { get; init; }

    /// <summary> Gets Upgrade_ShopPaymentPackageKey.</summary>
    /// <remarks> references <see cref="ShopPaymentPackageDat"/> on <see cref="Specification.LoadShopPaymentPackageDat"/> index.</remarks>
    public required int? Upgrade_ShopPaymentPackageKey { get; init; }

    /// <summary> Gets PhysicalItemPoints.</summary>
    public required int PhysicalItemPoints { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required int Unknown92 { get; init; }

    /// <summary> Gets ShopPackagePlatform.</summary>
    /// <remarks> references <see cref="ShopPackagePlatformDat"/> on <see cref="Specification.LoadShopPackagePlatformDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ShopPackagePlatform { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required string Unknown112 { get; init; }

    /// <summary>
    /// Gets ShopPaymentPackageDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ShopPaymentPackageDat.</returns>
    internal static ShopPaymentPackageDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ShopPaymentPackage.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopPaymentPackageDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Coins
            (var coinsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AvailableFlag
            (var availableflagLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown21
            (var unknown21Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ContainsBetaKey
            (var containsbetakeyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown31
            (var tempunknown31Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown31Loading = tempunknown31Loading.AsReadOnly();

            // loading Unknown47
            (var unknown47Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BackgroundImage
            (var backgroundimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown71
            (var unknown71Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Upgrade_ShopPaymentPackageKey
            (var upgrade_shoppaymentpackagekeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading PhysicalItemPoints
            (var physicalitempointsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ShopPackagePlatform
            (var tempshoppackageplatformLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var shoppackageplatformLoading = tempshoppackageplatformLoading.AsReadOnly();

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopPaymentPackageDat()
            {
                Id = idLoading,
                Text = textLoading,
                Coins = coinsLoading,
                AvailableFlag = availableflagLoading,
                Unknown21 = unknown21Loading,
                Unknown25 = unknown25Loading,
                Unknown29 = unknown29Loading,
                ContainsBetaKey = containsbetakeyLoading,
                Unknown31 = unknown31Loading,
                Unknown47 = unknown47Loading,
                BackgroundImage = backgroundimageLoading,
                Unknown71 = unknown71Loading,
                Unknown79 = unknown79Loading,
                Upgrade_ShopPaymentPackageKey = upgrade_shoppaymentpackagekeyLoading,
                PhysicalItemPoints = physicalitempointsLoading,
                Unknown92 = unknown92Loading,
                ShopPackagePlatform = shoppackageplatformLoading,
                Unknown112 = unknown112Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
