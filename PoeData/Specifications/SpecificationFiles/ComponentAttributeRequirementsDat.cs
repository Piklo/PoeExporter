// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing ComponentAttributeRequirements.dat data.
/// </summary>
public sealed partial class ComponentAttributeRequirementsDat : ISpecificationFile<ComponentAttributeRequirementsDat>
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    public required string BaseItemTypesKey { get; init; }

    /// <summary> Gets ReqStr.</summary>
    public required int ReqStr { get; init; }

    /// <summary> Gets ReqDex.</summary>
    public required int ReqDex { get; init; }

    /// <summary> Gets ReqInt.</summary>
    public required int ReqInt { get; init; }

    /// <inheritdoc/>
    public static ComponentAttributeRequirementsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ComponentAttributeRequirements.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ComponentAttributeRequirementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ReqStr
            (var reqstrLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ReqDex
            (var reqdexLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ReqInt
            (var reqintLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ComponentAttributeRequirementsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                ReqStr = reqstrLoading,
                ReqDex = reqdexLoading,
                ReqInt = reqintLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
