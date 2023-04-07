// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ExecuteGEAL.dat data.
/// </summary>
public sealed partial class ExecuteGEALDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets MiscAnimated.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscAnimated { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether Unknown48 is set.</summary>
    public required bool Unknown48 { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required int Unknown49 { get; init; }

    /// <summary> Gets a value indicating whether Unknown53 is set.</summary>
    public required bool Unknown53 { get; init; }

    /// <summary> Gets a value indicating whether Unknown54 is set.</summary>
    public required bool Unknown54 { get; init; }

    /// <summary> Gets Unknown55.</summary>
    public required ReadOnlyCollection<int> Unknown55 { get; init; }

    /// <summary> Gets Unknown71.</summary>
    public required int Unknown71 { get; init; }

    /// <summary> Gets Unknown75.</summary>
    public required int Unknown75 { get; init; }

    /// <summary> Gets Unknown79.</summary>
    public required int Unknown79 { get; init; }

    /// <summary> Gets Unknown83.</summary>
    public required int Unknown83 { get; init; }

    /// <summary> Gets Unknown87.</summary>
    public required int Unknown87 { get; init; }

    /// <summary> Gets a value indicating whether Unknown91 is set.</summary>
    public required bool Unknown91 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required string Unknown92 { get; init; }

    /// <summary> Gets a value indicating whether Unknown100 is set.</summary>
    public required bool Unknown100 { get; init; }

    /// <summary> Gets Unknown101.</summary>
    public required int Unknown101 { get; init; }

    /// <summary> Gets a value indicating whether Unknown105 is set.</summary>
    public required bool Unknown105 { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required int Unknown106 { get; init; }

    /// <summary> Gets Unknown110.</summary>
    public required int Unknown110 { get; init; }

    /// <summary> Gets a value indicating whether Unknown114 is set.</summary>
    public required bool Unknown114 { get; init; }

    /// <summary> Gets Unknown115.</summary>
    public required int Unknown115 { get; init; }

    /// <summary> Gets MetadataIDs.</summary>
    public required ReadOnlyCollection<string> MetadataIDs { get; init; }

    /// <summary> Gets ScriptCommand.</summary>
    public required string ScriptCommand { get; init; }

    /// <summary> Gets Unknown143.</summary>
    public required string Unknown143 { get; init; }

    /// <summary> Gets Unknown151.</summary>
    public required string Unknown151 { get; init; }

    /// <summary> Gets Unknown159.</summary>
    public required string Unknown159 { get; init; }

    /// <summary> Gets Unknown167.</summary>
    public required string Unknown167 { get; init; }

    /// <summary> Gets Unknown175.</summary>
    public required int Unknown175 { get; init; }

    /// <summary> Gets a value indicating whether Unknown179 is set.</summary>
    public required bool Unknown179 { get; init; }

    /// <summary> Gets a value indicating whether Unknown180 is set.</summary>
    public required bool Unknown180 { get; init; }

    /// <summary> Gets Unknown181.</summary>
    public required ReadOnlyCollection<int> Unknown181 { get; init; }

    /// <summary> Gets Unknown197.</summary>
    public required int Unknown197 { get; init; }

    /// <summary>
    /// Gets ExecuteGEALDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ExecuteGEALDat.</returns>
    internal static ExecuteGEALDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ExecuteGEAL.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExecuteGEALDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscAnimated
            (var tempmiscanimatedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var miscanimatedLoading = tempmiscanimatedLoading.AsReadOnly();

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown53
            (var unknown53Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown54
            (var unknown54Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown55
            (var tempunknown55Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown55Loading = tempunknown55Loading.AsReadOnly();

            // loading Unknown71
            (var unknown71Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown75
            (var unknown75Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown83
            (var unknown83Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown110
            (var unknown110Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown114
            (var unknown114Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MetadataIDs
            (var tempmetadataidsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var metadataidsLoading = tempmetadataidsLoading.AsReadOnly();

            // loading ScriptCommand
            (var scriptcommandLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown143
            (var unknown143Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown151
            (var unknown151Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown159
            (var unknown159Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown167
            (var unknown167Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown175
            (var unknown175Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown179
            (var unknown179Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown180
            (var unknown180Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown181
            (var tempunknown181Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown181Loading = tempunknown181Loading.AsReadOnly();

            // loading Unknown197
            (var unknown197Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExecuteGEALDat()
            {
                Unknown0 = unknown0Loading,
                MiscAnimated = miscanimatedLoading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown49 = unknown49Loading,
                Unknown53 = unknown53Loading,
                Unknown54 = unknown54Loading,
                Unknown55 = unknown55Loading,
                Unknown71 = unknown71Loading,
                Unknown75 = unknown75Loading,
                Unknown79 = unknown79Loading,
                Unknown83 = unknown83Loading,
                Unknown87 = unknown87Loading,
                Unknown91 = unknown91Loading,
                Unknown92 = unknown92Loading,
                Unknown100 = unknown100Loading,
                Unknown101 = unknown101Loading,
                Unknown105 = unknown105Loading,
                Unknown106 = unknown106Loading,
                Unknown110 = unknown110Loading,
                Unknown114 = unknown114Loading,
                Unknown115 = unknown115Loading,
                MetadataIDs = metadataidsLoading,
                ScriptCommand = scriptcommandLoading,
                Unknown143 = unknown143Loading,
                Unknown151 = unknown151Loading,
                Unknown159 = unknown159Loading,
                Unknown167 = unknown167Loading,
                Unknown175 = unknown175Loading,
                Unknown179 = unknown179Loading,
                Unknown180 = unknown180Loading,
                Unknown181 = unknown181Loading,
                Unknown197 = unknown197Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
