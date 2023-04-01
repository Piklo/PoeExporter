// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing Flasks.dat data.
/// </summary>
public sealed partial class FlasksDat : ISpecificationFile<FlasksDat>
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Group.</summary>
    public required int Group { get; init; }

    /// <summary> Gets LifePerUse.</summary>
    public required int LifePerUse { get; init; }

    /// <summary> Gets ManaPerUse.</summary>
    public required int ManaPerUse { get; init; }

    /// <summary> Gets RecoveryTime.</summary>
    public required int RecoveryTime { get; init; }

    /// <summary> Gets BuffDefinitionsKey.</summary>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets BuffStatValues.</summary>
    public required ReadOnlyCollection<int> BuffStatValues { get; init; }

    /// <summary> Gets RecoveryTime2.</summary>
    public required int RecoveryTime2 { get; init; }

    /// <summary> Gets BuffStatValues2.</summary>
    public required ReadOnlyCollection<int> BuffStatValues2 { get; init; }

    /// <inheritdoc/>
    public static FlasksDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Flasks.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new FlasksDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();
            // specification.GetBuffDefinitionsDat();

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Group
            (var groupLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifePerUse
            (var lifeperuseLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ManaPerUse
            (var manaperuseLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RecoveryTime
            (var recoverytimeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BuffStatValues
            (var tempbuffstatvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var buffstatvaluesLoading = tempbuffstatvaluesLoading.AsReadOnly();

            // loading RecoveryTime2
            (var recoverytime2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffStatValues2
            (var tempbuffstatvalues2Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var buffstatvalues2Loading = tempbuffstatvalues2Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new FlasksDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Name = nameLoading,
                Group = groupLoading,
                LifePerUse = lifeperuseLoading,
                ManaPerUse = manaperuseLoading,
                RecoveryTime = recoverytimeLoading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                BuffStatValues = buffstatvaluesLoading,
                RecoveryTime2 = recoverytime2Loading,
                BuffStatValues2 = buffstatvalues2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
