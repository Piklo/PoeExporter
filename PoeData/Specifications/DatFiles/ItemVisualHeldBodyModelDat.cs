// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemVisualHeldBodyModel.dat data.
/// </summary>
public sealed partial class ItemVisualHeldBodyModelDat
{
    /// <summary> Gets ItemVisualIdentity.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentity { get; init; }

    /// <summary> Gets MarauderAnimatedObject.</summary>
    public required string MarauderAnimatedObject { get; init; }

    /// <summary> Gets RangerAnimatedObject.</summary>
    public required string RangerAnimatedObject { get; init; }

    /// <summary> Gets WitchAnimatedObject.</summary>
    public required string WitchAnimatedObject { get; init; }

    /// <summary> Gets DuelistAnimatedObject.</summary>
    public required string DuelistAnimatedObject { get; init; }

    /// <summary> Gets TemplarAnimatedObject.</summary>
    public required string TemplarAnimatedObject { get; init; }

    /// <summary> Gets ShadowAnimatedObject.</summary>
    public required string ShadowAnimatedObject { get; init; }

    /// <summary> Gets ScionAnimatedObject.</summary>
    public required string ScionAnimatedObject { get; init; }

    /// <summary> Gets MarauderBone.</summary>
    public required string MarauderBone { get; init; }

    /// <summary> Gets RangerBone.</summary>
    public required string RangerBone { get; init; }

    /// <summary> Gets WitchBone.</summary>
    public required string WitchBone { get; init; }

    /// <summary> Gets DuelistBone.</summary>
    public required string DuelistBone { get; init; }

    /// <summary> Gets TemplarBone.</summary>
    public required string TemplarBone { get; init; }

    /// <summary> Gets ShadowBone.</summary>
    public required string ShadowBone { get; init; }

    /// <summary> Gets ScionBone.</summary>
    public required string ScionBone { get; init; }

    /// <summary>
    /// Gets ItemVisualHeldBodyModelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ItemVisualHeldBodyModelDat.</returns>
    internal static ItemVisualHeldBodyModelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ItemVisualHeldBodyModel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemVisualHeldBodyModelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ItemVisualIdentity
            (var itemvisualidentityLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MarauderAnimatedObject
            (var marauderanimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RangerAnimatedObject
            (var rangeranimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WitchAnimatedObject
            (var witchanimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DuelistAnimatedObject
            (var duelistanimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TemplarAnimatedObject
            (var templaranimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShadowAnimatedObject
            (var shadowanimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ScionAnimatedObject
            (var scionanimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MarauderBone
            (var marauderboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RangerBone
            (var rangerboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WitchBone
            (var witchboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DuelistBone
            (var duelistboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TemplarBone
            (var templarboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShadowBone
            (var shadowboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ScionBone
            (var scionboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemVisualHeldBodyModelDat()
            {
                ItemVisualIdentity = itemvisualidentityLoading,
                MarauderAnimatedObject = marauderanimatedobjectLoading,
                RangerAnimatedObject = rangeranimatedobjectLoading,
                WitchAnimatedObject = witchanimatedobjectLoading,
                DuelistAnimatedObject = duelistanimatedobjectLoading,
                TemplarAnimatedObject = templaranimatedobjectLoading,
                ShadowAnimatedObject = shadowanimatedobjectLoading,
                ScionAnimatedObject = scionanimatedobjectLoading,
                MarauderBone = marauderboneLoading,
                RangerBone = rangerboneLoading,
                WitchBone = witchboneLoading,
                DuelistBone = duelistboneLoading,
                TemplarBone = templarboneLoading,
                ShadowBone = shadowboneLoading,
                ScionBone = scionboneLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
