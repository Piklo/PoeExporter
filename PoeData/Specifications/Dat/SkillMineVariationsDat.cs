// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing SkillMineVariations.dat data.
/// </summary>
public sealed partial class SkillMineVariationsDat : ISpecificationFile<SkillMineVariationsDat>
{
    /// <summary> Gets SkillMinesKey.</summary>
    public required int SkillMinesKey { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int Unknown4 { get; init; }

    /// <summary> Gets MiscObject.</summary>
    /// <remarks> references <see cref="MiscObjectsDat"/> on <see cref="Specification.GetMiscObjectsDat"/> index.</remarks>
    public required int? MiscObject { get; init; }

    /// <inheritdoc/>
    public static SkillMineVariationsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/SkillMineVariations.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillMineVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading SkillMinesKey
            (var skillmineskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscObject
            (var miscobjectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillMineVariationsDat()
            {
                SkillMinesKey = skillmineskeyLoading,
                Unknown4 = unknown4Loading,
                MiscObject = miscobjectLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
