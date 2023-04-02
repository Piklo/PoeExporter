// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing PassiveTreeExpansionSkills.dat data.
/// </summary>
public sealed partial class PassiveTreeExpansionSkillsDat : ISpecificationFile<PassiveTreeExpansionSkillsDat>
{
    /// <summary> Gets PassiveSkillsKey.</summary>
    public required int? PassiveSkillsKey { get; init; }

    /// <summary> Gets Mastery_PassiveSkillsKey.</summary>
    public required int? Mastery_PassiveSkillsKey { get; init; }

    /// <summary> Gets TagsKey.</summary>
    public required int? TagsKey { get; init; }

    /// <summary> Gets PassiveTreeExpansionJewelSizesKey.</summary>
    public required int? PassiveTreeExpansionJewelSizesKey { get; init; }

    /// <inheritdoc/>
    public static PassiveTreeExpansionSkillsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/PassiveTreeExpansionSkills.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveTreeExpansionSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetPassiveSkillsDat();
            // specification.GetTagsDat();
            // specification.GetPassiveTreeExpansionJewelSizesDat();

            // loading PassiveSkillsKey
            (var passiveskillskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Mastery_PassiveSkillsKey
            (var mastery_passiveskillskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading TagsKey
            (var tagskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PassiveTreeExpansionJewelSizesKey
            (var passivetreeexpansionjewelsizeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveTreeExpansionSkillsDat()
            {
                PassiveSkillsKey = passiveskillskeyLoading,
                Mastery_PassiveSkillsKey = mastery_passiveskillskeyLoading,
                TagsKey = tagskeyLoading,
                PassiveTreeExpansionJewelSizesKey = passivetreeexpansionjewelsizeskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
