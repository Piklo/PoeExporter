// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing TalkingPetNPCAudio.dat data.
/// </summary>
public sealed partial class TalkingPetNPCAudioDat
{
    /// <summary> Gets Unknown0.</summary>
    /// <remarks> references <see cref="TalkingPetAudioEventsDat"/> on <see cref="Specification.GetTalkingPetAudioEventsDat"/> index.</remarks>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    /// <remarks> references <see cref="TalkingPetsDat"/> on <see cref="Specification.GetTalkingPetsDat"/> index.</remarks>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required ReadOnlyCollection<int> Unknown32 { get; init; }

    /// <inheritdoc/>
    public static TalkingPetNPCAudioDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/TalkingPetNPCAudio.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TalkingPetNPCAudioDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var tempunknown32Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown32Loading = tempunknown32Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TalkingPetNPCAudioDat()
            {
                Unknown0 = unknown0Loading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
