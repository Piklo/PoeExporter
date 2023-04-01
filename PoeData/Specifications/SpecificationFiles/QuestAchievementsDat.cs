﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing QuestAchievements.dat data.
/// </summary>
public sealed partial class QuestAchievementsDat : ISpecificationFile<QuestAchievementsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets QuestStates.</summary>
    public required ReadOnlyCollection<int> QuestStates { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required ReadOnlyCollection<int> Unknown24 { get; init; }

    /// <summary> Gets AchievementItems.</summary>
    public required ReadOnlyCollection<int> AchievementItems { get; init; }

    /// <summary> Gets NPCs.</summary>
    public required ReadOnlyCollection<int> NPCs { get; init; }

    /// <summary> Gets a value indicating whether Unknown72 is set.</summary>
    public required bool Unknown72 { get; init; }

    /// <inheritdoc/>
    public static QuestAchievementsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/QuestAchievements.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetAchievementItemsDat();
            // specification.GetNPCsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading QuestStates
            (var tempqueststatesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var queststatesLoading = tempqueststatesLoading.AsReadOnly();

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            // loading AchievementItems
            (var tempachievementitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemsLoading = tempachievementitemsLoading.AsReadOnly();

            // loading NPCs
            (var tempnpcsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npcsLoading = tempnpcsLoading.AsReadOnly();

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestAchievementsDat()
            {
                Id = idLoading,
                QuestStates = queststatesLoading,
                Unknown24 = unknown24Loading,
                AchievementItems = achievementitemsLoading,
                NPCs = npcsLoading,
                Unknown72 = unknown72Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
