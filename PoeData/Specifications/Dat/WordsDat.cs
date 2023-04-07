// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Words.dat data.
/// </summary>
public sealed partial class WordsDat
{
    /// <summary> Gets Wordlist.</summary>
    /// <remarks> references <see cref="WordlistsDat"/> on <see cref="Specification.GetWordlistsDat"/> index.</remarks>
    public required int Wordlist { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets SpawnWeight_Tags.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_Tags { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Text2.</summary>
    public required string Text2 { get; init; }

    /// <summary> Gets Inflection.</summary>
    public required string Inflection { get; init; }

    /// <summary>
    /// Gets WordsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of WordsDat.</returns>
    internal static WordsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Words.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WordsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Wordlist
            (var wordlistLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnWeight_Tags
            (var tempspawnweight_tagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagsLoading = tempspawnweight_tagsLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Text2
            (var text2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Inflection
            (var inflectionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WordsDat()
            {
                Wordlist = wordlistLoading,
                Text = textLoading,
                SpawnWeight_Tags = spawnweight_tagsLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                Unknown44 = unknown44Loading,
                Text2 = text2Loading,
                Inflection = inflectionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
