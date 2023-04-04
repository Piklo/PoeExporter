// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing WarbandsPackNumbers.dat data.
/// </summary>
public sealed partial class WarbandsPackNumbersDat : ISpecificationFile<WarbandsPackNumbersDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SpawnChance.</summary>
    public required int SpawnChance { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets Tier4Number.</summary>
    public required int Tier4Number { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Tier3Number.</summary>
    public required int Tier3Number { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Tier2Number.</summary>
    public required int Tier2Number { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Tier1Number.</summary>
    public required int Tier1Number { get; init; }

    /// <inheritdoc/>
    public static WarbandsPackNumbersDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/WarbandsPackNumbers.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WarbandsPackNumbersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnChance
            (var spawnchanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier4Number
            (var tier4numberLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier3Number
            (var tier3numberLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier2Number
            (var tier2numberLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier1Number
            (var tier1numberLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WarbandsPackNumbersDat()
            {
                Id = idLoading,
                SpawnChance = spawnchanceLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                Tier4Number = tier4numberLoading,
                Unknown24 = unknown24Loading,
                Tier3Number = tier3numberLoading,
                Unknown32 = unknown32Loading,
                Tier2Number = tier2numberLoading,
                Unknown40 = unknown40Loading,
                Tier1Number = tier1numberLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
