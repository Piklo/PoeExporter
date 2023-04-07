// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ShrineBuffs.dat data.
/// </summary>
public sealed partial class ShrineBuffsDat : IDat<ShrineBuffsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BuffStatValues.</summary>
    public required ReadOnlyCollection<int> BuffStatValues { get; init; }

    /// <summary> Gets BuffDefinitionsKey.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.GetBuffDefinitionsDat"/> index.</remarks>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets BuffVisual.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.GetBuffVisualsDat"/> index.</remarks>
    public required int? BuffVisual { get; init; }

    /// <inheritdoc/>
    public static ShrineBuffsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/ShrineBuffs.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShrineBuffsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffStatValues
            (var tempbuffstatvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var buffstatvaluesLoading = tempbuffstatvaluesLoading.AsReadOnly();

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BuffVisual
            (var buffvisualLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShrineBuffsDat()
            {
                Id = idLoading,
                BuffStatValues = buffstatvaluesLoading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                BuffVisual = buffvisualLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
