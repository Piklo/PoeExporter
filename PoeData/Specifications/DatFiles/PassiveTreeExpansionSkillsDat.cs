// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveTreeExpansionSkills.dat data.
/// </summary>
public sealed partial class PassiveTreeExpansionSkillsDat
{
    /// <summary> Gets PassiveSkillsKey.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.LoadPassiveSkillsDat"/> index.</remarks>
    public required int? PassiveSkillsKey { get; init; }

    /// <summary> Gets Mastery_PassiveSkillsKey.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.LoadPassiveSkillsDat"/> index.</remarks>
    public required int? Mastery_PassiveSkillsKey { get; init; }

    /// <summary> Gets TagsKey.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required int? TagsKey { get; init; }

    /// <summary> Gets PassiveTreeExpansionJewelSizesKey.</summary>
    /// <remarks> references <see cref="PassiveTreeExpansionJewelSizesDat"/> on <see cref="Specification.LoadPassiveTreeExpansionJewelSizesDat"/> index.</remarks>
    public required int? PassiveTreeExpansionJewelSizesKey { get; init; }

    /// <summary>
    /// Gets PassiveTreeExpansionSkillsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PassiveTreeExpansionSkillsDat.</returns>
    internal static PassiveTreeExpansionSkillsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PassiveTreeExpansionSkills.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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

            // loading PassiveSkillsKey
            (var passiveskillskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Mastery_PassiveSkillsKey
            (var mastery_passiveskillskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TagsKey
            (var tagskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PassiveTreeExpansionJewelSizesKey
            (var passivetreeexpansionjewelsizeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
