// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MonsterConditionalEffectPacks.dat data.
/// </summary>
public sealed partial class MonsterConditionalEffectPacksDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MiscEffectPack1.</summary>
    /// <remarks> references <see cref="MiscEffectPacksDat"/> on <see cref="Specification.GetMiscEffectPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscEffectPack1 { get; init; }

    /// <summary> Gets MiscEffectPack2.</summary>
    /// <remarks> references <see cref="MiscEffectPacksDat"/> on <see cref="Specification.GetMiscEffectPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscEffectPack2 { get; init; }

    /// <summary> Gets MiscEffectPack3.</summary>
    /// <remarks> references <see cref="MiscEffectPacksDat"/> on <see cref="Specification.GetMiscEffectPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscEffectPack3 { get; init; }

    /// <summary> Gets MiscEffectPack4.</summary>
    /// <remarks> references <see cref="MiscEffectPacksDat"/> on <see cref="Specification.GetMiscEffectPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscEffectPack4 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary>
    /// Gets MonsterConditionalEffectPacksDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MonsterConditionalEffectPacksDat.</returns>
    internal static MonsterConditionalEffectPacksDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MonsterConditionalEffectPacks.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterConditionalEffectPacksDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscEffectPack1
            (var tempmisceffectpack1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var misceffectpack1Loading = tempmisceffectpack1Loading.AsReadOnly();

            // loading MiscEffectPack2
            (var tempmisceffectpack2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var misceffectpack2Loading = tempmisceffectpack2Loading.AsReadOnly();

            // loading MiscEffectPack3
            (var tempmisceffectpack3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var misceffectpack3Loading = tempmisceffectpack3Loading.AsReadOnly();

            // loading MiscEffectPack4
            (var tempmisceffectpack4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var misceffectpack4Loading = tempmisceffectpack4Loading.AsReadOnly();

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterConditionalEffectPacksDat()
            {
                Id = idLoading,
                MiscEffectPack1 = misceffectpack1Loading,
                MiscEffectPack2 = misceffectpack2Loading,
                MiscEffectPack3 = misceffectpack3Loading,
                MiscEffectPack4 = misceffectpack4Loading,
                Unknown72 = unknown72Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
