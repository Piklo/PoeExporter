// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ExpeditionDeals.dat data.
/// </summary>
public sealed partial class ExpeditionDealsDat
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets Function.</summary>
    public required string Function { get; init; }

    /// <summary> Gets Arguments.</summary>
    public required string Arguments { get; init; }

    /// <summary> Gets TextAudio.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudio { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets BuyAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BuyAchievements { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int? Unknown60 { get; init; }

    /// <summary> Gets DealFamily.</summary>
    /// <remarks> references <see cref="ExpeditionDealFamiliesDat"/> on <see cref="Specification.GetExpeditionDealFamiliesDat"/> index.</remarks>
    public required int DealFamily { get; init; }

    /// <summary>
    /// Gets ExpeditionDealsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ExpeditionDealsDat.</returns>
    internal static ExpeditionDealsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ExpeditionDeals.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionDealsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Function
            (var functionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Arguments
            (var argumentsLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuyAchievements
            (var tempbuyachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var buyachievementsLoading = tempbuyachievementsLoading.AsReadOnly();

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading DealFamily
            (var dealfamilyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionDealsDat()
            {
                Id = idLoading,
                Function = functionLoading,
                Arguments = argumentsLoading,
                TextAudio = textaudioLoading,
                Description = descriptionLoading,
                BuyAchievements = buyachievementsLoading,
                Unknown60 = unknown60Loading,
                DealFamily = dealfamilyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
