// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing DefaultMonsterStats.dat data.
/// </summary>
public sealed partial class DefaultMonsterStatsDat : ISpecificationFile<DefaultMonsterStatsDat>
{
    /// <summary> Gets DisplayLevel.</summary>
    public required string DisplayLevel { get; init; }

    /// <summary> Gets Damage.</summary>
    public required float Damage { get; init; }

    /// <summary> Gets Evasion.</summary>
    public required int Evasion { get; init; }

    /// <summary> Gets Accuracy.</summary>
    public required int Accuracy { get; init; }

    /// <summary> Gets Life.</summary>
    public required int Life { get; init; }

    /// <summary> Gets Experience.</summary>
    public required int Experience { get; init; }

    /// <summary> Gets AllyLife.</summary>
    public required int AllyLife { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Difficulty.</summary>
    public required int Difficulty { get; init; }

    /// <summary> Gets Damage2.</summary>
    public required float Damage2 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required float Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required float Unknown52 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int Unknown60 { get; init; }

    /// <summary> Gets Armour.</summary>
    public required int Armour { get; init; }

    /// <inheritdoc/>
    public static DefaultMonsterStatsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/DefaultMonsterStats.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DefaultMonsterStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading DisplayLevel
            (var displaylevelLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Damage
            (var damageLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Evasion
            (var evasionLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Accuracy
            (var accuracyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Life
            (var lifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Experience
            (var experienceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AllyLife
            (var allylifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Difficulty
            (var difficultyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Damage2
            (var damage2Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Armour
            (var armourLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DefaultMonsterStatsDat()
            {
                DisplayLevel = displaylevelLoading,
                Damage = damageLoading,
                Evasion = evasionLoading,
                Accuracy = accuracyLoading,
                Life = lifeLoading,
                Experience = experienceLoading,
                AllyLife = allylifeLoading,
                Unknown32 = unknown32Loading,
                Difficulty = difficultyLoading,
                Damage2 = damage2Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Armour = armourLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
