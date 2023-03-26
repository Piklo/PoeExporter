// this file is auto generated
// the generated class is partial, please extend it in another file

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing AbyssObjects.dat data.
/// </summary>
public sealed partial class AbyssObjects : ISpecificationFile<AbyssObjects>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets Unknown0.</summary>
    public required Unknown<int> Unknown0 { get; init; }

    /// <summary> Gets MetadataFile.</summary>
    public required string MetadataFile { get; init; }

    /// <summary> Gets Unknown1.</summary>
    public required Unknown<int> Unknown1 { get; init; }

    /// <summary> Gets DaemonSpawners.</summary>
    public required ReadOnlyCollection<MonsterVarieties> DaemonSpawners { get; init; }

    /// <summary> Gets Unknown2.</summary>
    public required Unknown<int> Unknown2 { get; init; }

    /// <summary> Gets Unknown3.</summary>
    public required Unknown<int> Unknown3 { get; init; }

    /// <summary> Gets AbyssalDepths.</summary>
    public required WorldAreas? AbyssalDepths { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required Unknown<int> Unknown4 { get; init; }

    /// <summary> Gets Unknown5.</summary>
    public required Unknown<int> Unknown5 { get; init; }

    /// <summary> Gets Unknown6.</summary>
    public required Unknown<int> Unknown6 { get; init; }

    /// <summary> Gets Unknown7.</summary>
    public required Unknown<int> Unknown7 { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required Unknown<int> Unknown8 { get; init; }

    /// <summary> Gets Unknown9.</summary>
    public required Unknown<int> Unknown9 { get; init; }

    /// <summary> Gets Unknown10.</summary>
    public required Unknown<int> Unknown10 { get; init; }

    /// <summary> Gets Unknown11.</summary>
    public required Unknown<int> Unknown11 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required Unknown<int> Unknown12 { get; init; }

    /// <summary> Gets a value indicating whether Unknown13 is set.</summary>
    public required Unknown<bool> Unknown13 { get; init; }

    /// <inheritdoc/>
    public static AbyssObjects[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        // loading other required tables


        var fileToFind = Encoding.ASCII.GetBytes("Data/AbyssObjects.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AbyssObjects[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // only needed for debug

            // id
            (var id, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // minlevel
            (var minlevel, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // maxlevel
            (var maxlevel, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // spawnweight
            (var spawnweight, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // unknown0
            (var unknown0, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // metadatafile
            (var metadatafile, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // unknown1
            (var unknown1, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // daemonspawners
            (var daemonspawnersPrimaryKeys, offset) = SpecificationFileLoader.LoadPrimaryKeys(decompressedFile, offset, dataOffset);
            var daemonspawners = new MonsterVarieties[daemonspawnersPrimaryKeys.Length];
            for (var i = 0; i < daemonspawnersPrimaryKeys.Length; i++)
            {
                var key = daemonspawnersPrimaryKeys[i];
                var monstervarietiesEntry = specification.GetMonsterVarieties()[(int)key];
                daemonspawners[i] = monstervarietiesEntry;
            }

            // unknown2
            (var unknown2, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // unknown3
            (var unknown3, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // abyssaldepths
            (var abyssaldepthsPrimaryKey, offset) = SpecificationFileLoader.LoadPrimaryKey(decompressedFile, offset, dataOffset);
            WorldAreas? abyssaldepths = null;
            if (abyssaldepthsPrimaryKey is not null)
            {
                abyssaldepths = specification.GetWorldAreas()[(int)abyssaldepthsPrimaryKey];
            }

            // unknown4
            (var unknown4, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // unknown5
            (var unknown5, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // unknown6
            (var unknown6, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // unknown7
            (var unknown7, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // unknown8
            (var unknown8, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // unknown9
            (var unknown9, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // unknown10
            (var unknown10, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // unknown11
            (var unknown11, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // unknown12
            (var unknown12, offset) = SpecificationFileLoader.LoadUnknownInt(decompressedFile, offset, tableRecordLength);

            // unknown13
            (var unknown13, offset) = SpecificationFileLoader.LoadUnknownBoolean(decompressedFile, offset, tableRecordLength);

            var obj = new AbyssObjects()
            {
                Id = id,
                MinLevel = minlevel,
                MaxLevel = maxlevel,
                SpawnWeight = spawnweight,
                Unknown0 = unknown0,
                MetadataFile = metadatafile,
                Unknown1 = unknown1,
                DaemonSpawners = daemonspawners.AsReadOnly(),
                Unknown2 = unknown2,
                Unknown3 = unknown3,
                AbyssalDepths = abyssaldepths,
                Unknown4 = unknown4,
                Unknown5 = unknown5,
                Unknown6 = unknown6,
                Unknown7 = unknown7,
                Unknown8 = unknown8,
                Unknown9 = unknown9,
                Unknown10 = unknown10,
                Unknown11 = unknown11,
                Unknown12 = unknown12,
                Unknown13 = unknown13,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
