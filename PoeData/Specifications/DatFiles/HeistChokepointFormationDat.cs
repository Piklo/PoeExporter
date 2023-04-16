// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistChokepointFormation.dat data.
/// </summary>
public sealed partial class HeistChokepointFormationDat
{
    /// <summary> Gets Unknown0.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required ReadOnlyCollection<int> Unknown20 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown36 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required int? Unknown52 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets a value indicating whether Unknown72 is set.</summary>
    public required bool Unknown72 { get; init; }

    /// <summary> Gets Unknown73.</summary>
    public required int Unknown73 { get; init; }

    /// <summary> Gets Unknown77.</summary>
    public required int Unknown77 { get; init; }

    /// <summary> Gets Unknown81.</summary>
    public required int Unknown81 { get; init; }

    /// <summary> Gets Unknown85.</summary>
    public required int Unknown85 { get; init; }

    /// <summary>
    /// Gets HeistChokepointFormationDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistChokepointFormationDat.</returns>
    internal static HeistChokepointFormationDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistChokepointFormation.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistChokepointFormationDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var tempunknown20Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown20Loading = tempunknown20Loading.AsReadOnly();

            // loading Unknown36
            (var tempunknown36Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown36Loading = tempunknown36Loading.AsReadOnly();

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown77
            (var unknown77Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown81
            (var unknown81Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown85
            (var unknown85Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistChokepointFormationDat()
            {
                Unknown0 = unknown0Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Unknown52 = unknown52Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown73 = unknown73Loading,
                Unknown77 = unknown77Loading,
                Unknown81 = unknown81Loading,
                Unknown85 = unknown85Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
