// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Animation.dat data.
/// </summary>
public sealed partial class AnimationDat : IDat<AnimationDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown9 is set.</summary>
    public required bool Unknown9 { get; init; }

    /// <summary> Gets a value indicating whether Unknown10 is set.</summary>
    public required bool Unknown10 { get; init; }

    /// <summary> Gets Mainhand_AnimationKey.</summary>
    /// <remarks> references <see cref="AnimationDat"/> on <see cref="AnimationDat.Id"/>.</remarks>
    public required string Mainhand_AnimationKey { get; init; }

    /// <summary> Gets Offhand_AnimationKey.</summary>
    /// <remarks> references <see cref="AnimationDat"/> on <see cref="AnimationDat.Id"/>.</remarks>
    public required string Offhand_AnimationKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown27 is set.</summary>
    public required bool Unknown27 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int? Unknown28 { get; init; }

    /// <inheritdoc/>
    public static AnimationDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Animation.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AnimationDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown9
            (var unknown9Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown10
            (var unknown10Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Mainhand_AnimationKey
            (var mainhand_animationkeyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Offhand_AnimationKey
            (var offhand_animationkeyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown27
            (var unknown27Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AnimationDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown9 = unknown9Loading,
                Unknown10 = unknown10Loading,
                Mainhand_AnimationKey = mainhand_animationkeyLoading,
                Offhand_AnimationKey = offhand_animationkeyLoading,
                Unknown27 = unknown27Loading,
                Unknown28 = unknown28Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
