// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SafehouseBYOCrafting.dat data.
/// </summary>
public sealed partial class SafehouseBYOCraftingDat
{
    /// <summary> Gets BetrayalJobsKey.</summary>
    /// <remarks> references <see cref="BetrayalJobsDat"/> on <see cref="Specification.LoadBetrayalJobsDat"/> index.</remarks>
    public required int? BetrayalJobsKey { get; init; }

    /// <summary> Gets BetrayalTargetsKey.</summary>
    /// <remarks> references <see cref="BetrayalTargetsDat"/> on <see cref="Specification.LoadBetrayalTargetsDat"/> index.</remarks>
    public required int? BetrayalTargetsKey { get; init; }

    /// <summary> Gets Rank.</summary>
    public required int Rank { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets ServerCommand.</summary>
    public required string ServerCommand { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required ReadOnlyCollection<int> Unknown52 { get; init; }

    /// <summary>
    /// Gets SafehouseBYOCraftingDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SafehouseBYOCraftingDat.</returns>
    internal static SafehouseBYOCraftingDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SafehouseBYOCrafting.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SafehouseBYOCraftingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BetrayalJobsKey
            (var betrayaljobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BetrayalTargetsKey
            (var betrayaltargetskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Rank
            (var rankLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ServerCommand
            (var servercommandLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown52
            (var tempunknown52Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown52Loading = tempunknown52Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SafehouseBYOCraftingDat()
            {
                BetrayalJobsKey = betrayaljobskeyLoading,
                BetrayalTargetsKey = betrayaltargetskeyLoading,
                Rank = rankLoading,
                Description = descriptionLoading,
                ServerCommand = servercommandLoading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
