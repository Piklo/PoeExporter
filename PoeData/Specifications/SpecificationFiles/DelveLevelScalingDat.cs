// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing DelveLevelScaling.dat data.
/// </summary>
public sealed partial class DelveLevelScalingDat : ISpecificationFile<DelveLevelScalingDat>
{
    /// <summary> Gets Depth.</summary>
    public required int Depth { get; init; }

    /// <summary> Gets MonsterLevel.</summary>
    public required int MonsterLevel { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets SulphiteCost.</summary>
    public required int SulphiteCost { get; init; }

    /// <summary> Gets MonsterLevel2.</summary>
    public required int MonsterLevel2 { get; init; }

    /// <summary> Gets MoreMonsterLife.</summary>
    public required int MoreMonsterLife { get; init; }

    /// <summary> Gets MoreMonsterDamage.</summary>
    public required int MoreMonsterDamage { get; init; }

    /// <summary> Gets DarknessResistance.</summary>
    public required int DarknessResistance { get; init; }

    /// <summary> Gets LightRadius.</summary>
    public required int LightRadius { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <inheritdoc/>
    public static DelveLevelScalingDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/DelveLevelScaling.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveLevelScalingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading Depth
            (var depthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterLevel
            (var monsterlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SulphiteCost
            (var sulphitecostLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterLevel2
            (var monsterlevel2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MoreMonsterLife
            (var moremonsterlifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MoreMonsterDamage
            (var moremonsterdamageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DarknessResistance
            (var darknessresistanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LightRadius
            (var lightradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveLevelScalingDat()
            {
                Depth = depthLoading,
                MonsterLevel = monsterlevelLoading,
                Unknown8 = unknown8Loading,
                SulphiteCost = sulphitecostLoading,
                MonsterLevel2 = monsterlevel2Loading,
                MoreMonsterLife = moremonsterlifeLoading,
                MoreMonsterDamage = moremonsterdamageLoading,
                DarknessResistance = darknessresistanceLoading,
                LightRadius = lightradiusLoading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
