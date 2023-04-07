// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Grandmasters.dat data.
/// </summary>
public sealed partial class GrandmastersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets GMFile.</summary>
    public required string GMFile { get; init; }

    /// <summary> Gets AISFile.</summary>
    public required string AISFile { get; init; }

    /// <summary> Gets ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys { get; init; }

    /// <summary> Gets CharacterLevel.</summary>
    public required int CharacterLevel { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <inheritdoc/>
    public static GrandmastersDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/Grandmasters.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrandmastersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GMFile
            (var gmfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AISFile
            (var aisfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading CharacterLevel
            (var characterlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrandmastersDat()
            {
                Id = idLoading,
                GMFile = gmfileLoading,
                AISFile = aisfileLoading,
                ModsKeys = modskeysLoading,
                CharacterLevel = characterlevelLoading,
                Unknown44 = unknown44Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
