// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BuffTemplates.dat data.
/// </summary>
public sealed partial class BuffTemplatesDat : IDat<BuffTemplatesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BuffDefinitionsKey.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.GetBuffDefinitionsDat"/> index.</remarks>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required ReadOnlyCollection<int> Unknown24 { get; init; }

    /// <summary> Gets AuraRadius.</summary>
    public required int AuraRadius { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required ReadOnlyCollection<int> Unknown44 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required ReadOnlyCollection<int> Unknown60 { get; init; }

    /// <summary> Gets BuffVisualsKey.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.GetBuffVisualsDat"/> index.</remarks>
    public required int? BuffVisualsKey { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required float Unknown92 { get; init; }

    /// <summary> Gets a value indicating whether Unknown96 is set.</summary>
    public required bool Unknown96 { get; init; }

    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKey { get; init; }

    /// <summary> Gets Unknown113.</summary>
    public required int Unknown113 { get; init; }

    /// <summary> Gets Unknown117.</summary>
    public required int Unknown117 { get; init; }

    /// <summary> Gets a value indicating whether Unknown121 is set.</summary>
    public required bool Unknown121 { get; init; }

    /// <summary> Gets Unknown122.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown122 { get; init; }

    /// <inheritdoc/>
    public static BuffTemplatesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/BuffTemplates.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BuffTemplatesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            // loading AuraRadius
            (var auraradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var tempunknown44Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown44Loading = tempunknown44Loading.AsReadOnly();

            // loading Unknown60
            (var tempunknown60Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown60Loading = tempunknown60Loading.AsReadOnly();

            // loading BuffVisualsKey
            (var buffvisualskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading StatsKey
            (var tempstatskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeyLoading = tempstatskeyLoading.AsReadOnly();

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown121
            (var unknown121Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown122
            (var unknown122Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BuffTemplatesDat()
            {
                Id = idLoading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                Unknown24 = unknown24Loading,
                AuraRadius = auraradiusLoading,
                Unknown44 = unknown44Loading,
                Unknown60 = unknown60Loading,
                BuffVisualsKey = buffvisualskeyLoading,
                Unknown92 = unknown92Loading,
                Unknown96 = unknown96Loading,
                StatsKey = statskeyLoading,
                Unknown113 = unknown113Loading,
                Unknown117 = unknown117Loading,
                Unknown121 = unknown121Loading,
                Unknown122 = unknown122Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
