// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AtlasNode.dat data.
/// </summary>
public sealed partial class AtlasNodeDat : IDat<AtlasNodeDat>
{
    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.GetItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <summary> Gets MapsKey.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.GetMapsDat"/> index.</remarks>
    public required int? MapsKey { get; init; }

    /// <summary> Gets FlavourTextKey.</summary>
    /// <remarks> references <see cref="FlavourTextDat"/> on <see cref="Specification.GetFlavourTextDat"/> index.</remarks>
    public required int? FlavourTextKey { get; init; }

    /// <summary> Gets AtlasNodeKeys.</summary>
    /// <remarks> references <see cref="AtlasNodeDat"/> on <see cref="Specification.GetAtlasNodeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AtlasNodeKeys { get; init; }

    /// <summary> Gets Tier0.</summary>
    public required int Tier0 { get; init; }

    /// <summary> Gets Tier1.</summary>
    public required int Tier1 { get; init; }

    /// <summary> Gets Tier2.</summary>
    public required int Tier2 { get; init; }

    /// <summary> Gets Tier3.</summary>
    public required int Tier3 { get; init; }

    /// <summary> Gets Tier4.</summary>
    public required int Tier4 { get; init; }

    /// <summary> Gets Unknown101.</summary>
    public required float Unknown101 { get; init; }

    /// <summary> Gets Unknown105.</summary>
    public required float Unknown105 { get; init; }

    /// <summary> Gets Unknown109.</summary>
    public required float Unknown109 { get; init; }

    /// <summary> Gets Unknown113.</summary>
    public required float Unknown113 { get; init; }

    /// <summary> Gets Unknown117.</summary>
    public required float Unknown117 { get; init; }

    /// <summary> Gets DDSFile.</summary>
    public required string DDSFile { get; init; }

    /// <summary> Gets a value indicating whether Unknown129 is set.</summary>
    public required bool Unknown129 { get; init; }

    /// <summary> Gets a value indicating whether NotOnAtlas is set.</summary>
    public required bool NotOnAtlas { get; init; }

    /// <inheritdoc/>
    public static AtlasNodeDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AtlasNode.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasNodeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ItemVisualIdentityKey
            (var itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MapsKey
            (var mapskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading FlavourTextKey
            (var flavourtextkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AtlasNodeKeys
            (var tempatlasnodekeysLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var atlasnodekeysLoading = tempatlasnodekeysLoading.AsReadOnly();

            // loading Tier0
            (var tier0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier1
            (var tier1Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier2
            (var tier2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier3
            (var tier3Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier4
            (var tier4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading DDSFile
            (var ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown129
            (var unknown129Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading NotOnAtlas
            (var notonatlasLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasNodeDat()
            {
                WorldAreasKey = worldareaskeyLoading,
                ItemVisualIdentityKey = itemvisualidentitykeyLoading,
                Unknown32 = unknown32Loading,
                MapsKey = mapskeyLoading,
                FlavourTextKey = flavourtextkeyLoading,
                AtlasNodeKeys = atlasnodekeysLoading,
                Tier0 = tier0Loading,
                Tier1 = tier1Loading,
                Tier2 = tier2Loading,
                Tier3 = tier3Loading,
                Tier4 = tier4Loading,
                Unknown101 = unknown101Loading,
                Unknown105 = unknown105Loading,
                Unknown109 = unknown109Loading,
                Unknown113 = unknown113Loading,
                Unknown117 = unknown117Loading,
                DDSFile = ddsfileLoading,
                Unknown129 = unknown129Loading,
                NotOnAtlas = notonatlasLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
