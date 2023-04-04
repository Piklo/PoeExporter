// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AtlasPrimordialBosses.dat data.
/// </summary>
public sealed partial class AtlasPrimordialBossesDat : ISpecificationFile<AtlasPrimordialBossesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required int Unknown12 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets InfluenceComplete.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required int? InfluenceComplete { get; init; }

    /// <summary> Gets MiniBossInvitation.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? MiniBossInvitation { get; init; }

    /// <summary> Gets BossInvitation.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BossInvitation { get; init; }

    /// <summary> Gets PickUpKey.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? PickUpKey { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int? Unknown88 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required int? Unknown104 { get; init; }

    /// <summary> Gets Tag.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required int? Tag { get; init; }

    /// <summary> Gets Altar.</summary>
    /// <remarks> references <see cref="MiscObjectsDat"/> on <see cref="Specification.GetMiscObjectsDat"/> index.</remarks>
    public required int? Altar { get; init; }

    /// <summary> Gets AltarActivated.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required int? AltarActivated { get; init; }

    /// <inheritdoc/>
    public static AtlasPrimordialBossesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AtlasPrimordialBosses.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasPrimordialBossesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading InfluenceComplete
            (var influencecompleteLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MiniBossInvitation
            (var minibossinvitationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BossInvitation
            (var bossinvitationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PickUpKey
            (var pickupkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Tag
            (var tagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Altar
            (var altarLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AltarActivated
            (var altaractivatedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasPrimordialBossesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                InfluenceComplete = influencecompleteLoading,
                MiniBossInvitation = minibossinvitationLoading,
                BossInvitation = bossinvitationLoading,
                PickUpKey = pickupkeyLoading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
                Tag = tagLoading,
                Altar = altarLoading,
                AltarActivated = altaractivatedLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
