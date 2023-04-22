// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveTreeExpansionSpecialSkills.dat data.
/// </summary>
public sealed partial class PassiveTreeExpansionSpecialSkillsDat
{
    /// <summary> Gets PassiveSkillsKey.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.LoadPassiveSkillsDat"/> index.</remarks>
    public required int? PassiveSkillsKey { get; init; }

    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }

    /// <summary>
    /// Gets PassiveTreeExpansionSpecialSkillsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PassiveTreeExpansionSpecialSkillsDat.</returns>
    internal static PassiveTreeExpansionSpecialSkillsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PassiveTreeExpansionSpecialSkills.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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
            (var passiveskillskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
