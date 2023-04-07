// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SurgeTypes.dat data.
/// </summary>
public sealed partial class SurgeTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SurgeEffects.</summary>
    /// <remarks> references <see cref="SurgeEffectsDat"/> on <see cref="Specification.GetSurgeEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SurgeEffects { get; init; }

    /// <summary> Gets IntId.</summary>
    public required int IntId { get; init; }

    /// <summary>
    /// Gets SurgeTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SurgeTypesDat.</returns>
    internal static SurgeTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SurgeTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SurgeTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SurgeEffects
            (var tempsurgeeffectsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var surgeeffectsLoading = tempsurgeeffectsLoading.AsReadOnly();

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SurgeTypesDat()
            {
                Id = idLoading,
                SurgeEffects = surgeeffectsLoading,
                IntId = intidLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
