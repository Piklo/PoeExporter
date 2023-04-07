// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing StartingPassiveSkills.dat data.
/// </summary>
public sealed partial class StartingPassiveSkillsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets PassiveSkills.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.GetPassiveSkillsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PassiveSkills { get; init; }

    /// <inheritdoc/>
    public static StartingPassiveSkillsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/StartingPassiveSkills.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StartingPassiveSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveSkills
            (var temppassiveskillsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var passiveskillsLoading = temppassiveskillsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StartingPassiveSkillsDat()
            {
                Id = idLoading,
                PassiveSkills = passiveskillsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
