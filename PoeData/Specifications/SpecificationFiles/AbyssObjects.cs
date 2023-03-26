﻿using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing AbyssObjects.dat data.
/// </summary>
public sealed class AbyssObjects : ISpecificationFile<AbyssObjects>
{
    // private readonly Specification specification;

    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets min level.</summary>
    public int MinLevel { get; init; }

    /// <summary> Gets max level.</summary>
    public int MaxLevel { get; init; }

    /// <summary> Gets spawn weight.</summary>
    public int SpawnWeight { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown0 { get; init; }

    /// <summary> Gets metadata file.</summary>
    public required string MetadataFile { get; init; } // files .ot .otc

    /// <summary> Gets unknown.</summary>
    public int Unknown1 { get; init; }

    /// <summary> Gets daemon spawners.</summary>
    public required ReadOnlyCollection<MonsterVarieties> DaemonSpawners { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown2 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown3 { get; init; }

    /// <summary> Gets abyssal depths.</summary>
    public WorldAreas? AbyssalDepths { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown4 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown5 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown6 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown7 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown8 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown9 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown10 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown11 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown12 { get; init; }

    /// <summary>Gets a value indicating whether unknown is set.</summary>
    public bool Unknown13 { get; init; }

    ///// <summary>
    ///// Initializes a new instance of the <see cref="AbyssObjects"/> class.
    ///// </summary>
    ///// <param name="specification">Instance of <see cref="Specification"/> containing specification files.</param>
    // public AbyssObjects(Specification specification)
    // {
    //     this.specification = specification;
    // }

    /// <inheritdoc/>
    public static AbyssObjects[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        //var worldAreas = specification.GetWorldAreas();
        //var monsterVarieties = specification.GetMonsterVarieties();

        var fileToFind = Encoding.ASCII.GetBytes("Data/AbyssObjects.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var magicNumber = new byte[] { (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb' };
        var dataOffset = decompressedFile.IndexOfSubArray(magicNumber); // thats where the table ends?
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var abyssObjects = new AbyssObjects[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // only needed for debug
            (var id, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);
            (var minLevel, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var maxLevel, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var spawnWeight, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown0, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var metadataFile, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);
            (var unknown1, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            (var daemonSpawnersPrimaryKeys, offset) = SpecificationFileLoader.LoadPrimaryKeys(decompressedFile, offset, dataOffset);
            var daemonSpawners = new MonsterVarieties[daemonSpawnersPrimaryKeys.Length];
            //for (var i = 0; i < daemonSpawnersPrimaryKeys.Length; i++)
            //{
            //    var key = daemonSpawnersPrimaryKeys[i];
            //    var monsterVariety = monsterVarieties[key];
            //    daemonSpawners[i] = monsterVariety;
            //}

            (var unknown2, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown3, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            (var abyssalDepthsPrimaryKey, offset) = SpecificationFileLoader.LoadPrimaryKey(decompressedFile, offset, dataOffset);
            WorldAreas? abyssalDepth = null;
            // if (abyssalDepthsPrimaryKey is not null)
            // {
            //     abyssalDepth = worldAreas[(int)abyssalDepthsPrimaryKey];
            // }

            (var unknown4, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown5, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown6, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown7, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown8, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown9, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown10, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown11, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown12, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);
            (var unknown13, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            var abyssObject = new AbyssObjects()
            {
                Id = id,
                MinLevel = minLevel,
                MaxLevel = maxLevel,
                SpawnWeight = spawnWeight,
                Unknown0 = unknown0,
                MetadataFile = metadataFile,
                Unknown1 = unknown1,
                DaemonSpawners = daemonSpawners.AsReadOnly(),
                Unknown2 = unknown2,
                Unknown3 = unknown3,
                AbyssalDepths = abyssalDepth,
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
            abyssObjects[rowId] = abyssObject;
        }

        return abyssObjects;
    }

    private static string FindString(byte[] data, int dataOffset, int rowOffset)
    {
        var beginningOfTheSequence = new byte[] { (byte)'\x00', (byte)'\x00', (byte)'\x00', (byte)'\x00' };
        var start = dataOffset + rowOffset;

        var offsetNew = data.IndexOfSubArray(beginningOfTheSequence, start);

        var str = string.Empty;
        if (start == offsetNew)
        {
            return str;
        }

        while ((offsetNew - start) % 2 == 1)
        {
            offsetNew = data.IndexOfSubArray(beginningOfTheSequence, offsetNew + 1);
        }

        str = Encoding.Unicode.GetString(data, start, offsetNew - start);

        return str;
    }
}