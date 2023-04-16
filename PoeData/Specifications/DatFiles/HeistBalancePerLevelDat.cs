// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistBalancePerLevel.dat data.
/// </summary>
public sealed partial class HeistBalancePerLevelDat
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required float Unknown4 { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required float Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required int Unknown12 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required float Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required float Unknown24 { get; init; }

    /// <summary> Gets HeistValueScalingKey1.</summary>
    /// <remarks> references <see cref="HeistValueScalingDat"/> on <see cref="Specification.GetHeistValueScalingDat"/> index.</remarks>
    public required int? HeistValueScalingKey1 { get; init; }

    /// <summary> Gets HeistValueScalingKey2.</summary>
    /// <remarks> references <see cref="HeistValueScalingDat"/> on <see cref="Specification.GetHeistValueScalingDat"/> index.</remarks>
    public required int? HeistValueScalingKey2 { get; init; }

    /// <summary> Gets HeistValueScalingKey3.</summary>
    /// <remarks> references <see cref="HeistValueScalingDat"/> on <see cref="Specification.GetHeistValueScalingDat"/> index.</remarks>
    public required int? HeistValueScalingKey3 { get; init; }

    /// <summary> Gets HeistValueScalingKey4.</summary>
    /// <remarks> references <see cref="HeistValueScalingDat"/> on <see cref="Specification.GetHeistValueScalingDat"/> index.</remarks>
    public required int? HeistValueScalingKey4 { get; init; }

    /// <summary> Gets HeistValueScalingKey5.</summary>
    /// <remarks> references <see cref="HeistValueScalingDat"/> on <see cref="Specification.GetHeistValueScalingDat"/> index.</remarks>
    public required int? HeistValueScalingKey5 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required float Unknown108 { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required float Unknown112 { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required float Unknown116 { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required float Unknown120 { get; init; }

    /// <summary> Gets HeistValueScalingKey6.</summary>
    /// <remarks> references <see cref="HeistValueScalingDat"/> on <see cref="Specification.GetHeistValueScalingDat"/> index.</remarks>
    public required int? HeistValueScalingKey6 { get; init; }

    /// <summary> Gets HeistValueScalingKey7.</summary>
    /// <remarks> references <see cref="HeistValueScalingDat"/> on <see cref="Specification.GetHeistValueScalingDat"/> index.</remarks>
    public required int? HeistValueScalingKey7 { get; init; }

    /// <summary> Gets Unknown156.</summary>
    public required float Unknown156 { get; init; }

    /// <summary> Gets Unknown160.</summary>
    public required float Unknown160 { get; init; }

    /// <summary> Gets Unknown164.</summary>
    public required int Unknown164 { get; init; }

    /// <summary>
    /// Gets HeistBalancePerLevelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistBalancePerLevelDat.</returns>
    internal static HeistBalancePerLevelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistBalancePerLevel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistBalancePerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading HeistValueScalingKey1
            (var heistvaluescalingkey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HeistValueScalingKey2
            (var heistvaluescalingkey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HeistValueScalingKey3
            (var heistvaluescalingkey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HeistValueScalingKey4
            (var heistvaluescalingkey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HeistValueScalingKey5
            (var heistvaluescalingkey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading HeistValueScalingKey6
            (var heistvaluescalingkey6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HeistValueScalingKey7
            (var heistvaluescalingkey7Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown156
            (var unknown156Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown160
            (var unknown160Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistBalancePerLevelDat()
            {
                Level = levelLoading,
                Unknown4 = unknown4Loading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                HeistValueScalingKey1 = heistvaluescalingkey1Loading,
                HeistValueScalingKey2 = heistvaluescalingkey2Loading,
                HeistValueScalingKey3 = heistvaluescalingkey3Loading,
                HeistValueScalingKey4 = heistvaluescalingkey4Loading,
                HeistValueScalingKey5 = heistvaluescalingkey5Loading,
                Unknown108 = unknown108Loading,
                Unknown112 = unknown112Loading,
                Unknown116 = unknown116Loading,
                Unknown120 = unknown120Loading,
                HeistValueScalingKey6 = heistvaluescalingkey6Loading,
                HeistValueScalingKey7 = heistvaluescalingkey7Loading,
                Unknown156 = unknown156Loading,
                Unknown160 = unknown160Loading,
                Unknown164 = unknown164Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
