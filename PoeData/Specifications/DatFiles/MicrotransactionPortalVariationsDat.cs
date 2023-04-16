// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MicrotransactionPortalVariations.dat data.
/// </summary>
public sealed partial class MicrotransactionPortalVariationsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets AOFile.</summary>
    public required string AOFile { get; init; }

    /// <summary> Gets MapAOFile.</summary>
    public required string MapAOFile { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required float Unknown36 { get; init; }

    /// <summary> Gets MiscObject.</summary>
    /// <remarks> references <see cref="MiscObjectsDat"/> on <see cref="Specification.LoadMiscObjectsDat"/> index.</remarks>
    public required int? MiscObject { get; init; }

    /// <summary> Gets PortalEffect.</summary>
    public required string PortalEffect { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required float Unknown64 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required string Unknown68 { get; init; }

    /// <summary> Gets PortalEffectLarge.</summary>
    public required string PortalEffectLarge { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required string Unknown84 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required string Unknown92 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required string Unknown100 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required int Unknown108 { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required int Unknown112 { get; init; }

    /// <summary>
    /// Gets MicrotransactionPortalVariationsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MicrotransactionPortalVariationsDat.</returns>
    internal static MicrotransactionPortalVariationsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MicrotransactionPortalVariations.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionPortalVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MapAOFile
            (var mapaofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading MiscObject
            (var miscobjectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PortalEffect
            (var portaleffectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PortalEffectLarge
            (var portaleffectlargeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionPortalVariationsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Id = idLoading,
                AOFile = aofileLoading,
                MapAOFile = mapaofileLoading,
                Unknown36 = unknown36Loading,
                MiscObject = miscobjectLoading,
                PortalEffect = portaleffectLoading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                PortalEffectLarge = portaleffectlargeLoading,
                Unknown84 = unknown84Loading,
                Unknown92 = unknown92Loading,
                Unknown100 = unknown100Loading,
                Unknown108 = unknown108Loading,
                Unknown112 = unknown112Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
