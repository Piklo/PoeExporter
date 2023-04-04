// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing GeometryProjectiles.dat data.
/// </summary>
public sealed partial class GeometryProjectilesDat : ISpecificationFile<GeometryProjectilesDat>
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int? Unknown4 { get; init; }

    /// <summary> Gets a value indicating whether Unknown20 is set.</summary>
    public required bool Unknown20 { get; init; }

    /// <summary> Gets Unknown21.</summary>
    public required int Unknown21 { get; init; }

    /// <summary> Gets a value indicating whether Unknown25 is set.</summary>
    public required bool Unknown25 { get; init; }

    /// <summary> Gets Unknown26.</summary>
    public required int Unknown26 { get; init; }

    /// <summary> Gets Unknown30.</summary>
    public required int Unknown30 { get; init; }

    /// <summary> Gets a value indicating whether Unknown34 is set.</summary>
    public required bool Unknown34 { get; init; }

    /// <summary> Gets Unknown35.</summary>
    public required int Unknown35 { get; init; }

    /// <summary> Gets Unknown39.</summary>
    public required int Unknown39 { get; init; }

    /// <summary> Gets Unknown43.</summary>
    public required int Unknown43 { get; init; }

    /// <summary> Gets a value indicating whether Unknown47 is set.</summary>
    public required bool Unknown47 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets a value indicating whether Unknown60 is set.</summary>
    public required bool Unknown60 { get; init; }

    /// <summary> Gets a value indicating whether Unknown61 is set.</summary>
    public required bool Unknown61 { get; init; }

    /// <summary> Gets a value indicating whether Unknown62 is set.</summary>
    public required bool Unknown62 { get; init; }

    /// <summary> Gets Unknown63.</summary>
    public required int? Unknown63 { get; init; }

    /// <summary> Gets a value indicating whether Unknown79 is set.</summary>
    public required bool Unknown79 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int? Unknown80 { get; init; }

    /// <summary> Gets Unknown96.</summary>
    public required int Unknown96 { get; init; }

    /// <inheritdoc/>
    public static GeometryProjectilesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/GeometryProjectiles.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GeometryProjectilesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown21
            (var unknown21Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown26
            (var unknown26Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown30
            (var unknown30Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown34
            (var unknown34Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown35
            (var unknown35Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown39
            (var unknown39Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown43
            (var unknown43Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown47
            (var unknown47Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown63
            (var unknown63Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GeometryProjectilesDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown21 = unknown21Loading,
                Unknown25 = unknown25Loading,
                Unknown26 = unknown26Loading,
                Unknown30 = unknown30Loading,
                Unknown34 = unknown34Loading,
                Unknown35 = unknown35Loading,
                Unknown39 = unknown39Loading,
                Unknown43 = unknown43Loading,
                Unknown47 = unknown47Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown61 = unknown61Loading,
                Unknown62 = unknown62Loading,
                Unknown63 = unknown63Loading,
                Unknown79 = unknown79Loading,
                Unknown80 = unknown80Loading,
                Unknown96 = unknown96Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
