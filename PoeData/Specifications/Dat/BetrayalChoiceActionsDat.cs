// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BetrayalChoiceActions.dat data.
/// </summary>
public sealed partial class BetrayalChoiceActionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BetrayalChoicesKey.</summary>
    /// <remarks> references <see cref="BetrayalChoicesDat"/> on <see cref="Specification.GetBetrayalChoicesDat"/> index.</remarks>
    public required int? BetrayalChoicesKey { get; init; }

    /// <summary> Gets ClientStringsKey.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.GetClientStringsDat"/> index.</remarks>
    public required int? ClientStringsKey { get; init; }

    /// <inheritdoc/>
    public static BetrayalChoiceActionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/BetrayalChoiceActions.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalChoiceActionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BetrayalChoicesKey
            (var betrayalchoiceskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ClientStringsKey
            (var clientstringskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalChoiceActionsDat()
            {
                Id = idLoading,
                BetrayalChoicesKey = betrayalchoiceskeyLoading,
                ClientStringsKey = clientstringskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
