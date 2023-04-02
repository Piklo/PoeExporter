// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MavenFights.dat data.
/// </summary>
public sealed partial class MavenFightsDat : ISpecificationFile<MavenFightsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets WitnessesRequired.</summary>
    public required int WitnessesRequired { get; init; }

    /// <summary> Gets AreaLevel.</summary>
    public required int AreaLevel { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets BaseItemType.</summary>
    public required int? BaseItemType { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets MinMapTier.</summary>
    public required int MinMapTier { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int? Unknown48 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets WitnessAreas.</summary>
    public required ReadOnlyCollection<int> WitnessAreas { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required int Unknown84 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int? Unknown88 { get; init; }

    /// <summary> Gets a value indicating whether Unknown104 is set.</summary>
    public required bool Unknown104 { get; init; }

    /// <summary> Gets Unknown105.</summary>
    public required ReadOnlyCollection<int> Unknown105 { get; init; }

    /// <summary> Gets Achievements1.</summary>
    public required ReadOnlyCollection<int> Achievements1 { get; init; }

    /// <summary> Gets Achievements2.</summary>
    public required ReadOnlyCollection<int> Achievements2 { get; init; }

    /// <inheritdoc/>
    public static MavenFightsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MavenFights.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MavenFightsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();
            // specification.GetQuestFlagsDat();
            // specification.GetWorldAreasDat();
            // specification.GetAchievementItemsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WitnessesRequired
            (var witnessesrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinMapTier
            (var minmaptierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WitnessAreas
            (var tempwitnessareasLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var witnessareasLoading = tempwitnessareasLoading.AsReadOnly();

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown105
            (var tempunknown105Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown105Loading = tempunknown105Loading.AsReadOnly();

            // loading Achievements1
            (var tempachievements1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievements1Loading = tempachievements1Loading.AsReadOnly();

            // loading Achievements2
            (var tempachievements2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievements2Loading = tempachievements2Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MavenFightsDat()
            {
                Id = idLoading,
                WitnessesRequired = witnessesrequiredLoading,
                AreaLevel = arealevelLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                BaseItemType = baseitemtypeLoading,
                Unknown40 = unknown40Loading,
                MinMapTier = minmaptierLoading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                WitnessAreas = witnessareasLoading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
                Unknown105 = unknown105Loading,
                Achievements1 = achievements1Loading,
                Achievements2 = achievements2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
