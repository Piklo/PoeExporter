// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ProjectilesArtVariations.dat data.
/// </summary>
public sealed partial class ProjectilesArtVariationsDat : IDat<ProjectilesArtVariationsDat>
{
    /// <summary> Gets Projectile.</summary>
    public required string Projectile { get; init; }

    /// <summary> Gets Variant.</summary>
    public required int Variant { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required ReadOnlyCollection<int> Unknown12 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <inheritdoc/>
    public static ProjectilesArtVariationsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/ProjectilesArtVariations.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ProjectilesArtVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Projectile
            (var projectileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Variant
            (var variantLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var tempunknown12Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown12Loading = tempunknown12Loading.AsReadOnly();

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ProjectilesArtVariationsDat()
            {
                Projectile = projectileLoading,
                Variant = variantLoading,
                Unknown12 = unknown12Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
