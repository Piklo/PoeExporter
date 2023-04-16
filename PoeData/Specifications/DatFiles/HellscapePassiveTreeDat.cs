// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HellscapePassiveTree.dat data.
/// </summary>
public sealed partial class HellscapePassiveTreeDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets AllocationsRequired.</summary>
    public required int AllocationsRequired { get; init; }

    /// <summary> Gets Passives.</summary>
    /// <remarks> references <see cref="HellscapePassivesDat"/> on <see cref="Specification.GetHellscapePassivesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Passives { get; init; }

    /// <summary>
    /// Gets HellscapePassiveTreeDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HellscapePassiveTreeDat.</returns>
    internal static HellscapePassiveTreeDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HellscapePassiveTree.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapePassiveTreeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AllocationsRequired
            (var allocationsrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Passives
            (var temppassivesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var passivesLoading = temppassivesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapePassiveTreeDat()
            {
                Id = idLoading,
                AllocationsRequired = allocationsrequiredLoading,
                Passives = passivesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
