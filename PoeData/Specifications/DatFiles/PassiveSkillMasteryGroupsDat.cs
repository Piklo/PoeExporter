// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveSkillMasteryGroups.dat data.
/// </summary>
public sealed partial class PassiveSkillMasteryGroupsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MasteryEffects.</summary>
    /// <remarks> references <see cref="PassiveSkillMasteryEffectsDat"/> on <see cref="Specification.GetPassiveSkillMasteryEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MasteryEffects { get; init; }

    /// <summary> Gets InactiveIcon.</summary>
    public required string InactiveIcon { get; init; }

    /// <summary> Gets ActiveIcon.</summary>
    public required string ActiveIcon { get; init; }

    /// <summary> Gets ActiveEffectImage.</summary>
    public required string ActiveEffectImage { get; init; }

    /// <summary> Gets a value indicating whether Unknown48 is set.</summary>
    public required bool Unknown48 { get; init; }

    /// <summary> Gets SoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.GetSoundEffectsDat"/> index.</remarks>
    public required int? SoundEffect { get; init; }

    /// <summary> Gets MasteryCountStat.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? MasteryCountStat { get; init; }

    /// <summary>
    /// Gets PassiveSkillMasteryGroupsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PassiveSkillMasteryGroupsDat.</returns>
    internal static PassiveSkillMasteryGroupsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PassiveSkillMasteryGroups.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillMasteryGroupsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MasteryEffects
            (var tempmasteryeffectsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var masteryeffectsLoading = tempmasteryeffectsLoading.AsReadOnly();

            // loading InactiveIcon
            (var inactiveiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ActiveIcon
            (var activeiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ActiveEffectImage
            (var activeeffectimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SoundEffect
            (var soundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MasteryCountStat
            (var masterycountstatLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillMasteryGroupsDat()
            {
                Id = idLoading,
                MasteryEffects = masteryeffectsLoading,
                InactiveIcon = inactiveiconLoading,
                ActiveIcon = activeiconLoading,
                ActiveEffectImage = activeeffectimageLoading,
                Unknown48 = unknown48Loading,
                SoundEffect = soundeffectLoading,
                MasteryCountStat = masterycountstatLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
