// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UniqueChests.dat data.
/// </summary>
public sealed partial class UniqueChestsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets WordsKey.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.LoadWordsDat"/> index.</remarks>
    public required int? WordsKey { get; init; }

    /// <summary> Gets FlavourTextKey.</summary>
    /// <remarks> references <see cref="FlavourTextDat"/> on <see cref="Specification.LoadFlavourTextDat"/> index.</remarks>
    public required int? FlavourTextKey { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required ReadOnlyCollection<int> Unknown64 { get; init; }

    /// <summary> Gets AOFile.</summary>
    public required string AOFile { get; init; }

    /// <summary> Gets a value indicating whether Unknown88 is set.</summary>
    public required bool Unknown88 { get; init; }

    /// <summary> Gets Unknown89.</summary>
    public required ReadOnlyCollection<int> Unknown89 { get; init; }

    /// <summary> Gets AppearanceChestsKey.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.LoadChestsDat"/> index.</remarks>
    public required int? AppearanceChestsKey { get; init; }

    /// <summary> Gets ChestsKey.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.LoadChestsDat"/> index.</remarks>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets Unknown137.</summary>
    public required ReadOnlyCollection<int> Unknown137 { get; init; }

    /// <summary>
    /// Gets UniqueChestsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of UniqueChestsDat.</returns>
    internal static UniqueChestsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/UniqueChests.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WordsKey
            (var wordskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading FlavourTextKey
            (var flavourtextkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var tempunknown64Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown64Loading = tempunknown64Loading.AsReadOnly();

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown89
            (var tempunknown89Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown89Loading = tempunknown89Loading.AsReadOnly();

            // loading AppearanceChestsKey
            (var appearancechestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown137
            (var tempunknown137Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown137Loading = tempunknown137Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueChestsDat()
            {
                Id = idLoading,
                WordsKey = wordskeyLoading,
                FlavourTextKey = flavourtextkeyLoading,
                MinLevel = minlevelLoading,
                ModsKeys = modskeysLoading,
                SpawnWeight = spawnweightLoading,
                Unknown64 = unknown64Loading,
                AOFile = aofileLoading,
                Unknown88 = unknown88Loading,
                Unknown89 = unknown89Loading,
                AppearanceChestsKey = appearancechestskeyLoading,
                ChestsKey = chestskeyLoading,
                Unknown137 = unknown137Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
