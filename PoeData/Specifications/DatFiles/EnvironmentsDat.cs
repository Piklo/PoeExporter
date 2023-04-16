// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Environments.dat data.
/// </summary>
public sealed partial class EnvironmentsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Base_ENVFile.</summary>
    public required string Base_ENVFile { get; init; }

    /// <summary> Gets Corrupted_ENVFile.</summary>
    public required string Corrupted_ENVFile { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int? Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required ReadOnlyCollection<int> Unknown56 { get; init; }

    /// <summary> Gets EnvironmentTransitionsKey.</summary>
    /// <remarks> references <see cref="EnvironmentTransitionsDat"/> on <see cref="Specification.LoadEnvironmentTransitionsDat"/> index.</remarks>
    public required int? EnvironmentTransitionsKey { get; init; }

    /// <summary> Gets PreloadGroup.</summary>
    /// <remarks> references <see cref="PreloadGroupsDat"/> on <see cref="Specification.LoadPreloadGroupsDat"/> index.</remarks>
    public required int? PreloadGroup { get; init; }

    /// <summary>
    /// Gets EnvironmentsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of EnvironmentsDat.</returns>
    internal static EnvironmentsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Environments.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EnvironmentsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Base_ENVFile
            (var base_envfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Corrupted_ENVFile
            (var corrupted_envfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading EnvironmentTransitionsKey
            (var environmenttransitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PreloadGroup
            (var preloadgroupLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EnvironmentsDat()
            {
                Id = idLoading,
                Base_ENVFile = base_envfileLoading,
                Corrupted_ENVFile = corrupted_envfileLoading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                EnvironmentTransitionsKey = environmenttransitionskeyLoading,
                PreloadGroup = preloadgroupLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
