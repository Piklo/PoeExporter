// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LeagueFlag.dat data.
/// </summary>
public sealed partial class LeagueFlagDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Image.</summary>
    public required string Image { get; init; }

    /// <summary> Gets a value indicating whether IsHC is set.</summary>
    public required bool IsHC { get; init; }

    /// <summary> Gets a value indicating whether IsSSF is set.</summary>
    public required bool IsSSF { get; init; }

    /// <summary> Gets Banner.</summary>
    public required string Banner { get; init; }

    /// <summary> Gets a value indicating whether IsRuthless is set.</summary>
    public required bool IsRuthless { get; init; }

    /// <summary>
    /// Gets LeagueFlagDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of LeagueFlagDat.</returns>
    internal static LeagueFlagDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/LeagueFlag.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LeagueFlagDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Image
            (var imageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsHC
            (var ishcLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsSSF
            (var isssfLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Banner
            (var bannerLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsRuthless
            (var isruthlessLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LeagueFlagDat()
            {
                Id = idLoading,
                Image = imageLoading,
                IsHC = ishcLoading,
                IsSSF = isssfLoading,
                Banner = bannerLoading,
                IsRuthless = isruthlessLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
