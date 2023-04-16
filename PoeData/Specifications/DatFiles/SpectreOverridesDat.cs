// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SpectreOverrides.dat data.
/// </summary>
public sealed partial class SpectreOverridesDat
{
    /// <summary> Gets Monster.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? Monster { get; init; }

    /// <summary> Gets Spectre.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? Spectre { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required ReadOnlyCollection<int> Unknown32 { get; init; }

    /// <summary>
    /// Gets SpectreOverridesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SpectreOverridesDat.</returns>
    internal static SpectreOverridesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SpectreOverrides.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SpectreOverridesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Monster
            (var monsterLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Spectre
            (var spectreLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var tempunknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown32Loading = tempunknown32Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SpectreOverridesDat()
            {
                Monster = monsterLoading,
                Spectre = spectreLoading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
