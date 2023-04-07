// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ExtraTerrainFeatures.dat data.
/// </summary>
public sealed partial class ExtraTerrainFeaturesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ArmFiles.</summary>
    public required ReadOnlyCollection<string> ArmFiles { get; init; }

    /// <summary> Gets TdtFiles.</summary>
    public required ReadOnlyCollection<string> TdtFiles { get; init; }

    /// <summary> Gets a value indicating whether Unknown40 is set.</summary>
    public required bool Unknown40 { get; init; }

    /// <summary> Gets Unknown41.</summary>
    public required ReadOnlyCollection<string> Unknown41 { get; init; }

    /// <summary> Gets Unknown57.</summary>
    public required ReadOnlyCollection<int> Unknown57 { get; init; }

    /// <summary> Gets Unknown73.</summary>
    /// <remarks> references <see cref="ExtraTerrainFeaturesDat"/> on <see cref="Specification.GetExtraTerrainFeaturesDat"/> index.</remarks>
    public required int? Unknown73 { get; init; }

    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown97 is set.</summary>
    public required bool Unknown97 { get; init; }

    /// <summary> Gets Unknown98.</summary>
    public required int Unknown98 { get; init; }

    /// <inheritdoc/>
    public static ExtraTerrainFeaturesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/ExtraTerrainFeatures.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExtraTerrainFeaturesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ArmFiles
            (var temparmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var armfilesLoading = temparmfilesLoading.AsReadOnly();

            // loading TdtFiles
            (var temptdtfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var tdtfilesLoading = temptdtfilesLoading.AsReadOnly();

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown41
            (var tempunknown41Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown41Loading = tempunknown41Loading.AsReadOnly();

            // loading Unknown57
            (var tempunknown57Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown57Loading = tempunknown57Loading.AsReadOnly();

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown97
            (var unknown97Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown98
            (var unknown98Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExtraTerrainFeaturesDat()
            {
                Id = idLoading,
                ArmFiles = armfilesLoading,
                TdtFiles = tdtfilesLoading,
                Unknown40 = unknown40Loading,
                Unknown41 = unknown41Loading,
                Unknown57 = unknown57Loading,
                Unknown73 = unknown73Loading,
                WorldAreasKey = worldareaskeyLoading,
                Unknown97 = unknown97Loading,
                Unknown98 = unknown98Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
