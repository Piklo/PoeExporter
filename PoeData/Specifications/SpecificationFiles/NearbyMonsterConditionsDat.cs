// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing NearbyMonsterConditions.dat data.
/// </summary>
public sealed partial class NearbyMonsterConditionsDat : ISpecificationFile<NearbyMonsterConditionsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterVarietiesKeys.</summary>
    public required ReadOnlyCollection<int> MonsterVarietiesKeys { get; init; }

    /// <summary> Gets MonsterAmount.</summary>
    public required int MonsterAmount { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets a value indicating whether IsNegated is set.</summary>
    public required bool IsNegated { get; init; }

    /// <summary> Gets Unknown33.</summary>
    public required int Unknown33 { get; init; }

    /// <summary> Gets Unknown37.</summary>
    public required ReadOnlyCollection<int> Unknown37 { get; init; }

    /// <summary> Gets a value indicating whether IsLessThen is set.</summary>
    public required bool IsLessThen { get; init; }

    /// <summary> Gets MinimumHealthPercentage.</summary>
    public required int MinimumHealthPercentage { get; init; }

    /// <inheritdoc/>
    public static NearbyMonsterConditionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/NearbyMonsterConditions.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NearbyMonsterConditionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetMonsterVarietiesDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterVarietiesKeys
            (var tempmonstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monstervarietieskeysLoading = tempmonstervarietieskeysLoading.AsReadOnly();

            // loading MonsterAmount
            (var monsteramountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsNegated
            (var isnegatedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown37
            (var tempunknown37Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown37Loading = tempunknown37Loading.AsReadOnly();

            // loading IsLessThen
            (var islessthenLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MinimumHealthPercentage
            (var minimumhealthpercentageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NearbyMonsterConditionsDat()
            {
                Id = idLoading,
                MonsterVarietiesKeys = monstervarietieskeysLoading,
                MonsterAmount = monsteramountLoading,
                Unknown28 = unknown28Loading,
                IsNegated = isnegatedLoading,
                Unknown33 = unknown33Loading,
                Unknown37 = unknown37Loading,
                IsLessThen = islessthenLoading,
                MinimumHealthPercentage = minimumhealthpercentageLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
