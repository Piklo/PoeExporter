// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing NPCTextAudio.dat data.
/// </summary>
public sealed partial class NPCTextAudioDat : ISpecificationFile<NPCTextAudioDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    public required int? CharactersKey { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets Mono_AudioFile.</summary>
    public required string Mono_AudioFile { get; init; }

    /// <summary> Gets Stereo_AudioFile.</summary>
    public required string Stereo_AudioFile { get; init; }

    /// <summary> Gets a value indicating whether HasStereo is set.</summary>
    public required bool HasStereo { get; init; }

    /// <summary> Gets a value indicating whether Unknown49 is set.</summary>
    public required bool Unknown49 { get; init; }

    /// <summary> Gets Video.</summary>
    public required string Video { get; init; }

    /// <summary> Gets Unknown58.</summary>
    public required int Unknown58 { get; init; }

    /// <summary> Gets Unknown62.</summary>
    public required int Unknown62 { get; init; }

    /// <summary> Gets Unknown66.</summary>
    public required int Unknown66 { get; init; }

    /// <summary> Gets Unknown70.</summary>
    public required ReadOnlyCollection<int> Unknown70 { get; init; }

    /// <summary> Gets Unknown86.</summary>
    public required int? Unknown86 { get; init; }

    /// <inheritdoc/>
    public static NPCTextAudioDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/NPCTextAudio.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCTextAudioDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetCharactersDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKey
            (var characterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Mono_AudioFile
            (var mono_audiofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Stereo_AudioFile
            (var stereo_audiofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HasStereo
            (var hasstereoLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Video
            (var videoLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown58
            (var unknown58Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown70
            (var tempunknown70Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown70Loading = tempunknown70Loading.AsReadOnly();

            // loading Unknown86
            (var unknown86Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCTextAudioDat()
            {
                Id = idLoading,
                CharactersKey = characterskeyLoading,
                Text = textLoading,
                Mono_AudioFile = mono_audiofileLoading,
                Stereo_AudioFile = stereo_audiofileLoading,
                HasStereo = hasstereoLoading,
                Unknown49 = unknown49Loading,
                Video = videoLoading,
                Unknown58 = unknown58Loading,
                Unknown62 = unknown62Loading,
                Unknown66 = unknown66Loading,
                Unknown70 = unknown70Loading,
                Unknown86 = unknown86Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
