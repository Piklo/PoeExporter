// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AegisVariations.dat data.
/// </summary>
public sealed partial class AegisVariationsDat
{
    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets a value indicating whether DefendsAgainstPhysical is set.</summary>
    public required bool DefendsAgainstPhysical { get; init; }

    /// <summary> Gets a value indicating whether DefendsAgainstFire is set.</summary>
    public required bool DefendsAgainstFire { get; init; }

    /// <summary> Gets a value indicating whether DefendsAgainstCold is set.</summary>
    public required bool DefendsAgainstCold { get; init; }

    /// <summary> Gets a value indicating whether DefendsAgainstLightning is set.</summary>
    public required bool DefendsAgainstLightning { get; init; }

    /// <summary> Gets a value indicating whether DefendsAgainstChaos is set.</summary>
    public required bool DefendsAgainstChaos { get; init; }

    /// <summary> Gets Unknown13.</summary>
    public required int? Unknown13 { get; init; }

    /// <summary> Gets Unknown29.</summary>
    public required int? Unknown29 { get; init; }

    /// <summary> Gets Unknown45.</summary>
    public required int? Unknown45 { get; init; }

    /// <summary> Gets Unknown61.</summary>
    public required int? Unknown61 { get; init; }

    /// <summary>
    /// Gets AegisVariationsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AegisVariationsDat.</returns>
    internal static AegisVariationsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AegisVariations.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AegisVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DefendsAgainstPhysical
            (var defendsagainstphysicalLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DefendsAgainstFire
            (var defendsagainstfireLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DefendsAgainstCold
            (var defendsagainstcoldLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DefendsAgainstLightning
            (var defendsagainstlightningLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DefendsAgainstChaos
            (var defendsagainstchaosLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown13
            (var unknown13Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AegisVariationsDat()
            {
                Name = nameLoading,
                DefendsAgainstPhysical = defendsagainstphysicalLoading,
                DefendsAgainstFire = defendsagainstfireLoading,
                DefendsAgainstCold = defendsagainstcoldLoading,
                DefendsAgainstLightning = defendsagainstlightningLoading,
                DefendsAgainstChaos = defendsagainstchaosLoading,
                Unknown13 = unknown13Loading,
                Unknown29 = unknown29Loading,
                Unknown45 = unknown45Loading,
                Unknown61 = unknown61Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
