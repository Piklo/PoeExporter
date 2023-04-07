// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ArchnemesisModVisuals.dat data.
/// </summary>
public sealed partial class ArchnemesisModVisualsDat : IDat<ArchnemesisModVisualsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required int? Unknown8 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int? Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.GetBuffVisualsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown56 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown72 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown88 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required ReadOnlyCollection<int> Unknown104 { get; init; }

    /// <inheritdoc/>
    public static ArchnemesisModVisualsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/ArchnemesisModVisuals.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArchnemesisModVisualsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading Unknown72
            (var tempunknown72Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown72Loading = tempunknown72Loading.AsReadOnly();

            // loading Unknown88
            (var tempunknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown88Loading = tempunknown88Loading.AsReadOnly();

            // loading Unknown104
            (var tempunknown104Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown104Loading = tempunknown104Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArchnemesisModVisualsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
