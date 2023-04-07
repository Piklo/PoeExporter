// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LeagueInfo.dat data.
/// </summary>
public sealed partial class LeagueInfoDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets PanelImage.</summary>
    public required string PanelImage { get; init; }

    /// <summary> Gets HeaderImage.</summary>
    public required string HeaderImage { get; init; }

    /// <summary> Gets Screenshots.</summary>
    public required ReadOnlyCollection<string> Screenshots { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets League.</summary>
    public required string League { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets TrailerVideoLink.</summary>
    public required string TrailerVideoLink { get; init; }

    /// <summary> Gets BackgroundImage.</summary>
    public required string BackgroundImage { get; init; }

    /// <summary> Gets a value indicating whether Unknown73 is set.</summary>
    public required bool Unknown73 { get; init; }

    /// <summary> Gets a value indicating whether Unknown74 is set.</summary>
    public required bool Unknown74 { get; init; }

    /// <summary> Gets PanelItems.</summary>
    public required ReadOnlyCollection<string> PanelItems { get; init; }

    /// <summary>
    /// Gets LeagueInfoDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of LeagueInfoDat.</returns>
    internal static LeagueInfoDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/LeagueInfo.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LeagueInfoDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PanelImage
            (var panelimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeaderImage
            (var headerimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Screenshots
            (var tempscreenshotsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var screenshotsLoading = tempscreenshotsLoading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading League
            (var leagueLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TrailerVideoLink
            (var trailervideolinkLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BackgroundImage
            (var backgroundimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading PanelItems
            (var temppanelitemsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var panelitemsLoading = temppanelitemsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LeagueInfoDat()
            {
                Id = idLoading,
                PanelImage = panelimageLoading,
                HeaderImage = headerimageLoading,
                Screenshots = screenshotsLoading,
                Description = descriptionLoading,
                League = leagueLoading,
                Unknown56 = unknown56Loading,
                TrailerVideoLink = trailervideolinkLoading,
                BackgroundImage = backgroundimageLoading,
                Unknown73 = unknown73Loading,
                Unknown74 = unknown74Loading,
                PanelItems = panelitemsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
