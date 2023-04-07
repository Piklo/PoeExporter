// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing LabyrinthTrinkets.dat data.
/// </summary>
public sealed partial class LabyrinthTrinketsDat : IDat<LabyrinthTrinketsDat>
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets LabyrinthSecretsKey.</summary>
    /// <remarks> references <see cref="LabyrinthSecretsDat"/> on <see cref="Specification.GetLabyrinthSecretsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSecretsKey { get; init; }

    /// <summary> Gets Buff_BuffDefinitionsKey.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.GetBuffDefinitionsDat"/> index.</remarks>
    public required int? Buff_BuffDefinitionsKey { get; init; }

    /// <summary> Gets Buff_StatValues.</summary>
    public required ReadOnlyCollection<int> Buff_StatValues { get; init; }

    /// <inheritdoc/>
    public static LabyrinthTrinketsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/LabyrinthTrinkets.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthTrinketsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading LabyrinthSecretsKey
            (var templabyrinthsecretskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsecretskeyLoading = templabyrinthsecretskeyLoading.AsReadOnly();

            // loading Buff_BuffDefinitionsKey
            (var buff_buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Buff_StatValues
            (var tempbuff_statvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var buff_statvaluesLoading = tempbuff_statvaluesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthTrinketsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                LabyrinthSecretsKey = labyrinthsecretskeyLoading,
                Buff_BuffDefinitionsKey = buff_buffdefinitionskeyLoading,
                Buff_StatValues = buff_statvaluesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
