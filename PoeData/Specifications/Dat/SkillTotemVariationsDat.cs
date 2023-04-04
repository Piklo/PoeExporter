// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing SkillTotemVariations.dat data.
/// </summary>
public sealed partial class SkillTotemVariationsDat : ISpecificationFile<SkillTotemVariationsDat>
{
    /// <summary> Gets SkillTotemsKey.</summary>
    /// <remarks> references <see cref="SkillTotemsDat"/> on <see cref="Specification.GetSkillTotemsDat"/> index.</remarks>
    public required int SkillTotemsKey { get; init; }

    /// <summary> Gets TotemSkinId.</summary>
    public required int TotemSkinId { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <inheritdoc/>
    public static SkillTotemVariationsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/SkillTotemVariations.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillTotemVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading SkillTotemsKey
            (var skilltotemskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TotemSkinId
            (var totemskinidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillTotemVariationsDat()
            {
                SkillTotemsKey = skilltotemskeyLoading,
                TotemSkinId = totemskinidLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
