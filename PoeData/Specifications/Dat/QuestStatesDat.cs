// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing QuestStates.dat data.
/// </summary>
public sealed partial class QuestStatesDat : ISpecificationFile<QuestStatesDat>
{
    /// <summary> Gets QuestKey.</summary>
    /// <remarks> references <see cref="QuestDat"/> on <see cref="Specification.GetQuestDat"/> index.</remarks>
    public required int? QuestKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets QuestStates.</summary>
    public required ReadOnlyCollection<int> QuestStates { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required ReadOnlyCollection<int> Unknown36 { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets a value indicating whether Unknown60 is set.</summary>
    public required bool Unknown60 { get; init; }

    /// <summary> Gets Message.</summary>
    public required string Message { get; init; }

    /// <summary> Gets MapPinsKeys1.</summary>
    /// <remarks> references <see cref="MapPinsDat"/> on <see cref="Specification.GetMapPinsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MapPinsKeys1 { get; init; }

    /// <summary> Gets Unknown85.</summary>
    public required int Unknown85 { get; init; }

    /// <summary> Gets MapPinsTexts.</summary>
    public required ReadOnlyCollection<string> MapPinsTexts { get; init; }

    /// <summary> Gets MapPinsKeys2.</summary>
    /// <remarks> references <see cref="MapPinsDat"/> on <see cref="Specification.GetMapPinsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MapPinsKeys2 { get; init; }

    /// <summary> Gets Unknown121.</summary>
    public required ReadOnlyCollection<int> Unknown121 { get; init; }

    /// <summary> Gets a value indicating whether Unknown137 is set.</summary>
    public required bool Unknown137 { get; init; }

    /// <summary> Gets Unknown138.</summary>
    public required ReadOnlyCollection<int> Unknown138 { get; init; }

    /// <summary> Gets Unknown154.</summary>
    public required ReadOnlyCollection<int> Unknown154 { get; init; }

    /// <summary> Gets Unknown170.</summary>
    public required int Unknown170 { get; init; }

    /// <summary> Gets SoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.GetSoundEffectsDat"/> index.</remarks>
    public required int? SoundEffect { get; init; }

    /// <inheritdoc/>
    public static QuestStatesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/QuestStates.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestStatesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading QuestKey
            (var questkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading QuestStates
            (var tempqueststatesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var queststatesLoading = tempqueststatesLoading.AsReadOnly();

            // loading Unknown36
            (var tempunknown36Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown36Loading = tempunknown36Loading.AsReadOnly();

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Message
            (var messageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MapPinsKeys1
            (var tempmappinskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var mappinskeys1Loading = tempmappinskeys1Loading.AsReadOnly();

            // loading Unknown85
            (var unknown85Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MapPinsTexts
            (var tempmappinstextsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var mappinstextsLoading = tempmappinstextsLoading.AsReadOnly();

            // loading MapPinsKeys2
            (var tempmappinskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var mappinskeys2Loading = tempmappinskeys2Loading.AsReadOnly();

            // loading Unknown121
            (var tempunknown121Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown121Loading = tempunknown121Loading.AsReadOnly();

            // loading Unknown137
            (var unknown137Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown138
            (var tempunknown138Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown138Loading = tempunknown138Loading.AsReadOnly();

            // loading Unknown154
            (var tempunknown154Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown154Loading = tempunknown154Loading.AsReadOnly();

            // loading Unknown170
            (var unknown170Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SoundEffect
            (var soundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestStatesDat()
            {
                QuestKey = questkeyLoading,
                Unknown16 = unknown16Loading,
                QuestStates = queststatesLoading,
                Unknown36 = unknown36Loading,
                Text = textLoading,
                Unknown60 = unknown60Loading,
                Message = messageLoading,
                MapPinsKeys1 = mappinskeys1Loading,
                Unknown85 = unknown85Loading,
                MapPinsTexts = mappinstextsLoading,
                MapPinsKeys2 = mappinskeys2Loading,
                Unknown121 = unknown121Loading,
                Unknown137 = unknown137Loading,
                Unknown138 = unknown138Loading,
                Unknown154 = unknown154Loading,
                Unknown170 = unknown170Loading,
                SoundEffect = soundeffectLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
