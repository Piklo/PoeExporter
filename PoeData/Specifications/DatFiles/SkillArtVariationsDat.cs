// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SkillArtVariations.dat data.
/// </summary>
public sealed partial class SkillArtVariationsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required ReadOnlyCollection<int> Unknown8 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required ReadOnlyCollection<int> Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<int> Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required ReadOnlyCollection<int> Unknown56 { get; init; }

    /// <summary> Gets Variants.</summary>
    public required ReadOnlyCollection<string> Variants { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required ReadOnlyCollection<int> Unknown88 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required ReadOnlyCollection<int> Unknown104 { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required ReadOnlyCollection<int> Unknown120 { get; init; }

    /// <summary> Gets Unknown136.</summary>
    public required int? Unknown136 { get; init; }

    /// <summary> Gets Unknown152.</summary>
    public required ReadOnlyCollection<int> Unknown152 { get; init; }

    /// <summary> Gets Unknown168.</summary>
    public required ReadOnlyCollection<int> Unknown168 { get; init; }

    /// <summary> Gets Unknown184.</summary>
    public required ReadOnlyCollection<int> Unknown184 { get; init; }

    /// <summary>
    /// Gets SkillArtVariationsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SkillArtVariationsDat.</returns>
    internal static SkillArtVariationsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SkillArtVariations.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillArtVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var tempunknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown8Loading = tempunknown8Loading.AsReadOnly();

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading Variants
            (var tempvariantsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var variantsLoading = tempvariantsLoading.AsReadOnly();

            // loading Unknown88
            (var tempunknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown88Loading = tempunknown88Loading.AsReadOnly();

            // loading Unknown104
            (var tempunknown104Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown104Loading = tempunknown104Loading.AsReadOnly();

            // loading Unknown120
            (var tempunknown120Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown120Loading = tempunknown120Loading.AsReadOnly();

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown152
            (var tempunknown152Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown152Loading = tempunknown152Loading.AsReadOnly();

            // loading Unknown168
            (var tempunknown168Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown168Loading = tempunknown168Loading.AsReadOnly();

            // loading Unknown184
            (var tempunknown184Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown184Loading = tempunknown184Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillArtVariationsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                Variants = variantsLoading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
                Unknown120 = unknown120Loading,
                Unknown136 = unknown136Loading,
                Unknown152 = unknown152Loading,
                Unknown168 = unknown168Loading,
                Unknown184 = unknown184Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
