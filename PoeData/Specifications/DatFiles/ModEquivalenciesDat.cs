// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ModEquivalencies.dat data.
/// </summary>
public sealed partial class ModEquivalenciesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ModsKey0.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? ModsKey0 { get; init; }

    /// <summary> Gets ModsKey1.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? ModsKey1 { get; init; }

    /// <summary> Gets ModsKey2.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? ModsKey2 { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary>
    /// Gets ModEquivalenciesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ModEquivalenciesDat.</returns>
    internal static ModEquivalenciesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ModEquivalencies.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ModEquivalenciesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKey0
            (var modskey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ModsKey1
            (var modskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ModsKey2
            (var modskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ModEquivalenciesDat()
            {
                Id = idLoading,
                ModsKey0 = modskey0Loading,
                ModsKey1 = modskey1Loading,
                ModsKey2 = modskey2Loading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
