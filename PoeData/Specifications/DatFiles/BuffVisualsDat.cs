// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BuffVisuals.dat data.
/// </summary>
public sealed partial class BuffVisualsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BuffDDSFile.</summary>
    public required string BuffDDSFile { get; init; }

    /// <summary> Gets EPKFiles1.</summary>
    public required ReadOnlyCollection<string> EPKFiles1 { get; init; }

    /// <summary> Gets EPKFiles2.</summary>
    public required ReadOnlyCollection<string> EPKFiles2 { get; init; }

    /// <summary> Gets PreloadGroups.</summary>
    /// <remarks> references <see cref="PreloadGroupsDat"/> on <see cref="Specification.GetPreloadGroupsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PreloadGroups { get; init; }

    /// <summary> Gets a value indicating whether Unknown64 is set.</summary>
    public required bool Unknown64 { get; init; }

    /// <summary> Gets BuffName.</summary>
    public required string BuffName { get; init; }

    /// <summary> Gets MiscAnimated1.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimated1 { get; init; }

    /// <summary> Gets MiscAnimated2.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimated2 { get; init; }

    /// <summary> Gets BuffDescription.</summary>
    public required string BuffDescription { get; init; }

    /// <summary> Gets EPKFile.</summary>
    public required string EPKFile { get; init; }

    /// <summary> Gets a value indicating whether HasExtraArt is set.</summary>
    public required bool HasExtraArt { get; init; }

    /// <summary> Gets ExtraArt.</summary>
    public required string ExtraArt { get; init; }

    /// <summary> Gets Unknown130.</summary>
    public required ReadOnlyCollection<int> Unknown130 { get; init; }

    /// <summary> Gets EPKFiles.</summary>
    public required ReadOnlyCollection<string> EPKFiles { get; init; }

    /// <summary> Gets BuffVisualOrbs.</summary>
    /// <remarks> references <see cref="BuffVisualOrbsDat"/> on <see cref="Specification.GetBuffVisualOrbsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BuffVisualOrbs { get; init; }

    /// <summary> Gets MiscAnimated3.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimated3 { get; init; }

    /// <summary> Gets Unknown194.</summary>
    public required int? Unknown194 { get; init; }

    /// <summary>
    /// Gets BuffVisualsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BuffVisualsDat.</returns>
    internal static BuffVisualsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BuffVisuals.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BuffVisualsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffDDSFile
            (var buffddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EPKFiles1
            (var tempepkfiles1Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var epkfiles1Loading = tempepkfiles1Loading.AsReadOnly();

            // loading EPKFiles2
            (var tempepkfiles2Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var epkfiles2Loading = tempepkfiles2Loading.AsReadOnly();

            // loading PreloadGroups
            (var temppreloadgroupsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var preloadgroupsLoading = temppreloadgroupsLoading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading BuffName
            (var buffnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscAnimated1
            (var miscanimated1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MiscAnimated2
            (var miscanimated2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BuffDescription
            (var buffdescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EPKFile
            (var epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HasExtraArt
            (var hasextraartLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ExtraArt
            (var extraartLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown130
            (var tempunknown130Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown130Loading = tempunknown130Loading.AsReadOnly();

            // loading EPKFiles
            (var tempepkfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var epkfilesLoading = tempepkfilesLoading.AsReadOnly();

            // loading BuffVisualOrbs
            (var tempbuffvisualorbsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var buffvisualorbsLoading = tempbuffvisualorbsLoading.AsReadOnly();

            // loading MiscAnimated3
            (var miscanimated3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown194
            (var unknown194Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BuffVisualsDat()
            {
                Id = idLoading,
                BuffDDSFile = buffddsfileLoading,
                EPKFiles1 = epkfiles1Loading,
                EPKFiles2 = epkfiles2Loading,
                PreloadGroups = preloadgroupsLoading,
                Unknown64 = unknown64Loading,
                BuffName = buffnameLoading,
                MiscAnimated1 = miscanimated1Loading,
                MiscAnimated2 = miscanimated2Loading,
                BuffDescription = buffdescriptionLoading,
                EPKFile = epkfileLoading,
                HasExtraArt = hasextraartLoading,
                ExtraArt = extraartLoading,
                Unknown130 = unknown130Loading,
                EPKFiles = epkfilesLoading,
                BuffVisualOrbs = buffvisualorbsLoading,
                MiscAnimated3 = miscanimated3Loading,
                Unknown194 = unknown194Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
