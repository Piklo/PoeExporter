// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing CharacterAudioEvents.dat data.
/// </summary>
public sealed partial class CharacterAudioEventsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Event.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? Event { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets Goddess_CharacterTextAudioKeys.</summary>
    /// <remarks> references <see cref="CharacterTextAudioDat"/> on <see cref="Specification.GetCharacterTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Goddess_CharacterTextAudioKeys { get; init; }

    /// <summary> Gets JackTheAxe_CharacterTextAudioKeys.</summary>
    /// <remarks> references <see cref="CharacterTextAudioDat"/> on <see cref="Specification.GetCharacterTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> JackTheAxe_CharacterTextAudioKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown64 is set.</summary>
    public required bool Unknown64 { get; init; }

    /// <summary> Gets a value indicating whether Unknown65 is set.</summary>
    public required bool Unknown65 { get; init; }

    /// <inheritdoc/>
    public static CharacterAudioEventsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/CharacterAudioEvents.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterAudioEventsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Event
            (var eventLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Goddess_CharacterTextAudioKeys
            (var tempgoddess_charactertextaudiokeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var goddess_charactertextaudiokeysLoading = tempgoddess_charactertextaudiokeysLoading.AsReadOnly();

            // loading JackTheAxe_CharacterTextAudioKeys
            (var tempjacktheaxe_charactertextaudiokeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var jacktheaxe_charactertextaudiokeysLoading = tempjacktheaxe_charactertextaudiokeysLoading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterAudioEventsDat()
            {
                Id = idLoading,
                Event = eventLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Goddess_CharacterTextAudioKeys = goddess_charactertextaudiokeysLoading,
                JackTheAxe_CharacterTextAudioKeys = jacktheaxe_charactertextaudiokeysLoading,
                Unknown64 = unknown64Loading,
                Unknown65 = unknown65Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
