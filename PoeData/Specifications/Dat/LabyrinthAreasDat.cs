// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing LabyrinthAreas.dat data.
/// </summary>
public sealed partial class LabyrinthAreasDat : IDat<LabyrinthAreasDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Normal_WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Normal_WorldAreasKeys { get; init; }

    /// <summary> Gets Cruel_WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cruel_WorldAreasKeys { get; init; }

    /// <summary> Gets Merciless_WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Merciless_WorldAreasKeys { get; init; }

    /// <summary> Gets Endgame_WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Endgame_WorldAreasKeys { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <inheritdoc/>
    public static LabyrinthAreasDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/LabyrinthAreas.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthAreasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Normal_WorldAreasKeys
            (var tempnormal_worldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var normal_worldareaskeysLoading = tempnormal_worldareaskeysLoading.AsReadOnly();

            // loading Cruel_WorldAreasKeys
            (var tempcruel_worldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cruel_worldareaskeysLoading = tempcruel_worldareaskeysLoading.AsReadOnly();

            // loading Merciless_WorldAreasKeys
            (var tempmerciless_worldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var merciless_worldareaskeysLoading = tempmerciless_worldareaskeysLoading.AsReadOnly();

            // loading Endgame_WorldAreasKeys
            (var tempendgame_worldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var endgame_worldareaskeysLoading = tempendgame_worldareaskeysLoading.AsReadOnly();

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthAreasDat()
            {
                Id = idLoading,
                Normal_WorldAreasKeys = normal_worldareaskeysLoading,
                Cruel_WorldAreasKeys = cruel_worldareaskeysLoading,
                Merciless_WorldAreasKeys = merciless_worldareaskeysLoading,
                Endgame_WorldAreasKeys = endgame_worldareaskeysLoading,
                Unknown72 = unknown72Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
