// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing TradeMarketCategory.dat data.
/// </summary>
public sealed partial class TradeMarketCategoryDat : ISpecificationFile<TradeMarketCategoryDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets StyleFlag.</summary>
    /// <remarks> references <see cref="TradeMarketCategoryStyleFlagDat"/> on <see cref="Specification.GetTradeMarketCategoryStyleFlagDat"/> index.</remarks>
    public required int StyleFlag { get; init; }

    /// <summary> Gets Group.</summary>
    /// <remarks> references <see cref="TradeMarketCategoryGroupsDat"/> on <see cref="Specification.GetTradeMarketCategoryGroupsDat"/> index.</remarks>
    public required int? Group { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required ReadOnlyCollection<int> Unknown36 { get; init; }

    /// <summary> Gets a value indicating whether Unknown52 is set.</summary>
    public required bool Unknown52 { get; init; }

    /// <summary> Gets a value indicating whether IsDisabled is set.</summary>
    public required bool IsDisabled { get; init; }

    /// <inheritdoc/>
    public static TradeMarketCategoryDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/TradeMarketCategory.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TradeMarketCategoryDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StyleFlag
            (var styleflagLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Group
            (var groupLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown36
            (var tempunknown36Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown36Loading = tempunknown36Loading.AsReadOnly();

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsDisabled
            (var isdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TradeMarketCategoryDat()
            {
                Id = idLoading,
                Name = nameLoading,
                StyleFlag = styleflagLoading,
                Group = groupLoading,
                Unknown36 = unknown36Loading,
                Unknown52 = unknown52Loading,
                IsDisabled = isdisabledLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
