// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing PassiveTreeExpansionSpecialSkills.dat data.
/// </summary>
public sealed partial class PassiveTreeExpansionSpecialSkillsDat
{
    /// <summary> Gets PassiveSkillsKey.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.GetPassiveSkillsDat"/> index.</remarks>
    public required int? PassiveSkillsKey { get; init; }

    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }

    /// <inheritdoc/>
    public static PassiveTreeExpansionSpecialSkillsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/PassiveTreeExpansionSpecialSkills.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveTreeExpansionSpecialSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading PassiveSkillsKey
            (var passiveskillskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveTreeExpansionSpecialSkillsDat()
            {
                PassiveSkillsKey = passiveskillskeyLoading,
                StatsKey = statskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
