// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing NPCDialogueStyles.dat data.
/// </summary>
public sealed partial class NPCDialogueStylesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HeaderBaseFile.</summary>
    public required string HeaderBaseFile { get; init; }

    /// <summary> Gets ButtomFile.</summary>
    public required string ButtomFile { get; init; }

    /// <summary> Gets BannerFiles.</summary>
    public required ReadOnlyCollection<string> BannerFiles { get; init; }

    /// <summary> Gets HeaderFiles.</summary>
    public required ReadOnlyCollection<string> HeaderFiles { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int Unknown60 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required ReadOnlyCollection<int> Unknown76 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    /// <remarks> references <see cref="NPCDialogueStylesDat"/> on <see cref="Specification.GetNPCDialogueStylesDat"/> index.</remarks>
    public required int? Unknown92 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required string Unknown100 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required ReadOnlyCollection<int> Unknown108 { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required ReadOnlyCollection<int> Unknown124 { get; init; }

    /// <summary> Gets Unknown140.</summary>
    public required int Unknown140 { get; init; }

    /// <summary> Gets Unknown144.</summary>
    public required int Unknown144 { get; init; }

    /// <summary> Gets Unknown148.</summary>
    public required float Unknown148 { get; init; }

    /// <summary> Gets Unknown152.</summary>
    public required string Unknown152 { get; init; }

    /// <summary> Gets Unknown160.</summary>
    public required string Unknown160 { get; init; }

    /// <summary> Gets Unknown168.</summary>
    public required int Unknown168 { get; init; }

    /// <summary> Gets Unknown172.</summary>
    public required int Unknown172 { get; init; }

    /// <summary>
    /// Gets NPCDialogueStylesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of NPCDialogueStylesDat.</returns>
    internal static NPCDialogueStylesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/NPCDialogueStyles.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCDialogueStylesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeaderBaseFile
            (var headerbasefileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ButtomFile
            (var buttomfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BannerFiles
            (var tempbannerfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var bannerfilesLoading = tempbannerfilesLoading.AsReadOnly();

            // loading HeaderFiles
            (var tempheaderfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var headerfilesLoading = tempheaderfilesLoading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var tempunknown76Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown76Loading = tempunknown76Loading.AsReadOnly();

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var tempunknown108Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown108Loading = tempunknown108Loading.AsReadOnly();

            // loading Unknown124
            (var tempunknown124Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown124Loading = tempunknown124Loading.AsReadOnly();

            // loading Unknown140
            (var unknown140Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown152
            (var unknown152Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown160
            (var unknown160Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown168
            (var unknown168Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown172
            (var unknown172Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCDialogueStylesDat()
            {
                Id = idLoading,
                HeaderBaseFile = headerbasefileLoading,
                ButtomFile = buttomfileLoading,
                BannerFiles = bannerfilesLoading,
                HeaderFiles = headerfilesLoading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                Unknown92 = unknown92Loading,
                Unknown100 = unknown100Loading,
                Unknown108 = unknown108Loading,
                Unknown124 = unknown124Loading,
                Unknown140 = unknown140Loading,
                Unknown144 = unknown144Loading,
                Unknown148 = unknown148Loading,
                Unknown152 = unknown152Loading,
                Unknown160 = unknown160Loading,
                Unknown168 = unknown168Loading,
                Unknown172 = unknown172Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
