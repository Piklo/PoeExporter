// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing CharacterEventTextAudio.dat data.
/// </summary>
public sealed partial class CharacterEventTextAudioDat : IDat<CharacterEventTextAudioDat>
{
    /// <summary> Gets Event.</summary>
    /// <remarks> references <see cref="CharacterAudioEventsDat"/> on <see cref="Specification.GetCharacterAudioEventsDat"/> index.</remarks>
    public required int? Event { get; init; }

    /// <summary> Gets Character.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.GetCharactersDat"/> index.</remarks>
    public required int? Character { get; init; }

    /// <summary> Gets TextAudio.</summary>
    /// <remarks> references <see cref="CharacterTextAudioDat"/> on <see cref="Specification.GetCharacterTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TextAudio { get; init; }

    /// <inheritdoc/>
    public static CharacterEventTextAudioDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/CharacterEventTextAudio.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterEventTextAudioDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Event
            (var eventLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Character
            (var characterLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading TextAudio
            (var temptextaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var textaudioLoading = temptextaudioLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterEventTextAudioDat()
            {
                Event = eventLoading,
                Character = characterLoading,
                TextAudio = textaudioLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
