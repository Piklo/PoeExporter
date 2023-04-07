// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AchievementItems.dat data.
/// </summary>
public sealed partial class AchievementItemsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required int Unknown12 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets CompletionsRequired.</summary>
    public required int CompletionsRequired { get; init; }

    /// <summary> Gets AchievementsKey.</summary>
    /// <remarks> references <see cref="AchievementsDat"/> on <see cref="Specification.GetAchievementsDat"/> index.</remarks>
    public required int? AchievementsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether Unknown45 is set.</summary>
    public required bool Unknown45 { get; init; }

    /// <summary> Gets a value indicating whether Unknown46 is set.</summary>
    public required bool Unknown46 { get; init; }

    /// <summary> Gets a value indicating whether Unknown47 is set.</summary>
    public required bool Unknown47 { get; init; }

    /// <summary>
    /// Gets AchievementItemsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AchievementItemsDat.</returns>
    internal static AchievementItemsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AchievementItems.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AchievementItemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CompletionsRequired
            (var completionsrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementsKey
            (var achievementskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown47
            (var unknown47Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AchievementItemsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Name = nameLoading,
                CompletionsRequired = completionsrequiredLoading,
                AchievementsKey = achievementskeyLoading,
                Unknown44 = unknown44Loading,
                Unknown45 = unknown45Loading,
                Unknown46 = unknown46Loading,
                Unknown47 = unknown47Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
