// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing PassiveSkillTrees.dat data.
/// </summary>
public sealed partial class PassiveSkillTreesDat : IDat<PassiveSkillTreesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets PassiveSkillGraph.</summary>
    public required string PassiveSkillGraph { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required float Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required float Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required float Unknown28 { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <summary> Gets a value indicating whether Unknown33 is set.</summary>
    public required bool Unknown33 { get; init; }

    /// <summary> Gets a value indicating whether Unknown34 is set.</summary>
    public required bool Unknown34 { get; init; }

    /// <summary> Gets a value indicating whether Unknown35 is set.</summary>
    public required bool Unknown35 { get; init; }

    /// <summary> Gets a value indicating whether Unknown36 is set.</summary>
    public required bool Unknown36 { get; init; }

    /// <summary> Gets a value indicating whether Unknown37 is set.</summary>
    public required bool Unknown37 { get; init; }

    /// <summary> Gets a value indicating whether Unknown38 is set.</summary>
    public required bool Unknown38 { get; init; }

    /// <summary> Gets a value indicating whether Unknown39 is set.</summary>
    public required bool Unknown39 { get; init; }

    /// <summary> Gets a value indicating whether Unknown40 is set.</summary>
    public required bool Unknown40 { get; init; }

    /// <summary> Gets a value indicating whether Unknown41 is set.</summary>
    public required bool Unknown41 { get; init; }

    /// <summary> Gets a value indicating whether Unknown42 is set.</summary>
    public required bool Unknown42 { get; init; }

    /// <summary> Gets Unknown43.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.GetClientStringsDat"/> index.</remarks>
    public required int? Unknown43 { get; init; }

    /// <summary> Gets UIArt.</summary>
    /// <remarks> references <see cref="PassiveSkillTreeUIArtDat"/> on <see cref="Specification.GetPassiveSkillTreeUIArtDat"/> index.</remarks>
    public required int? UIArt { get; init; }

    /// <inheritdoc/>
    public static PassiveSkillTreesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/PassiveSkillTrees.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillTreesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveSkillGraph
            (var passiveskillgraphLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown34
            (var unknown34Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown35
            (var unknown35Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown38
            (var unknown38Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown39
            (var unknown39Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown43
            (var unknown43Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading UIArt
            (var uiartLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillTreesDat()
            {
                Id = idLoading,
                PassiveSkillGraph = passiveskillgraphLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                Unknown33 = unknown33Loading,
                Unknown34 = unknown34Loading,
                Unknown35 = unknown35Loading,
                Unknown36 = unknown36Loading,
                Unknown37 = unknown37Loading,
                Unknown38 = unknown38Loading,
                Unknown39 = unknown39Loading,
                Unknown40 = unknown40Loading,
                Unknown41 = unknown41Loading,
                Unknown42 = unknown42Loading,
                Unknown43 = unknown43Loading,
                UIArt = uiartLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
