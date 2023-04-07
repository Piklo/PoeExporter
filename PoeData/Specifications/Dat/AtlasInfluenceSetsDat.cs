// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AtlasInfluenceSets.dat data.
/// </summary>
public sealed partial class AtlasInfluenceSetsDat : IDat<AtlasInfluenceSetsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets InfluencePacks.</summary>
    /// <remarks> references <see cref="AtlasInfluenceOutcomesDat"/> on <see cref="Specification.GetAtlasInfluenceOutcomesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> InfluencePacks { get; init; }

    /// <inheritdoc/>
    public static AtlasInfluenceSetsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/AtlasInfluenceSets.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasInfluenceSetsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading InfluencePacks
            (var tempinfluencepacksLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var influencepacksLoading = tempinfluencepacksLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasInfluenceSetsDat()
            {
                Id = idLoading,
                InfluencePacks = influencepacksLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
