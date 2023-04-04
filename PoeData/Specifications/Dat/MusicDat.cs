// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Music.dat data.
/// </summary>
public sealed partial class MusicDat : IDat<MusicDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SoundFile.</summary>
    public required string SoundFile { get; init; }

    /// <summary> Gets BankFile.</summary>
    public required string BankFile { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets a value indicating whether IsAvailableInHideout is set.</summary>
    public required bool IsAvailableInHideout { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Unknown37.</summary>
    public required string Unknown37 { get; init; }

    /// <summary> Gets MusicCategories.</summary>
    /// <remarks> references <see cref="MusicCategoriesDat"/> on <see cref="Specification.GetMusicCategoriesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MusicCategories { get; init; }

    /// <summary> Gets a value indicating whether Unknown61 is set.</summary>
    public required bool Unknown61 { get; init; }

    /// <summary> Gets Unknown62.</summary>
    public required int Unknown62 { get; init; }

    /// <inheritdoc/>
    public static MusicDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Music.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MusicDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SoundFile
            (var soundfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BankFile
            (var bankfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsAvailableInHideout
            (var isavailableinhideoutLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MusicCategories
            (var tempmusiccategoriesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var musiccategoriesLoading = tempmusiccategoriesLoading.AsReadOnly();

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MusicDat()
            {
                Id = idLoading,
                SoundFile = soundfileLoading,
                BankFile = bankfileLoading,
                HASH16 = hash16Loading,
                IsAvailableInHideout = isavailableinhideoutLoading,
                Name = nameLoading,
                Unknown37 = unknown37Loading,
                MusicCategories = musiccategoriesLoading,
                Unknown61 = unknown61Loading,
                Unknown62 = unknown62Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
