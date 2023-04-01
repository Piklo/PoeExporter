﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing BuffVisualOrbs.dat data.
/// </summary>
public sealed partial class BuffVisualOrbsDat : ISpecificationFile<BuffVisualOrbsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BuffVisualOrbTypesKey.</summary>
    public required int? BuffVisualOrbTypesKey { get; init; }

    /// <summary> Gets BuffVisualOrbArtKeys.</summary>
    public required ReadOnlyCollection<int> BuffVisualOrbArtKeys { get; init; }

    /// <summary> Gets Player_BuffVisualOrbArtKeys.</summary>
    public required ReadOnlyCollection<int> Player_BuffVisualOrbArtKeys { get; init; }

    /// <summary> Gets BuffVisualOrbArtKeys2.</summary>
    public required ReadOnlyCollection<int> BuffVisualOrbArtKeys2 { get; init; }

    /// <inheritdoc/>
    public static BuffVisualOrbsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BuffVisualOrbs.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BuffVisualOrbsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBuffVisualOrbTypesDat();
            // specification.GetBuffVisualOrbArtDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffVisualOrbTypesKey
            (var buffvisualorbtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BuffVisualOrbArtKeys
            (var tempbuffvisualorbartkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var buffvisualorbartkeysLoading = tempbuffvisualorbartkeysLoading.AsReadOnly();

            // loading Player_BuffVisualOrbArtKeys
            (var tempplayer_buffvisualorbartkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var player_buffvisualorbartkeysLoading = tempplayer_buffvisualorbartkeysLoading.AsReadOnly();

            // loading BuffVisualOrbArtKeys2
            (var tempbuffvisualorbartkeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var buffvisualorbartkeys2Loading = tempbuffvisualorbartkeys2Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BuffVisualOrbsDat()
            {
                Id = idLoading,
                BuffVisualOrbTypesKey = buffvisualorbtypeskeyLoading,
                BuffVisualOrbArtKeys = buffvisualorbartkeysLoading,
                Player_BuffVisualOrbArtKeys = player_buffvisualorbartkeysLoading,
                BuffVisualOrbArtKeys2 = buffvisualorbartkeys2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}