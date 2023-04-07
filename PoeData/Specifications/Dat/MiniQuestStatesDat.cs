// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MiniQuestStates.dat data.
/// </summary>
public sealed partial class MiniQuestStatesDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required ReadOnlyCollection<int> Unknown4 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required ReadOnlyCollection<int> Unknown20 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <inheritdoc/>
    public static MiniQuestStatesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/MiniQuestStates.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MiniQuestStatesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var tempunknown4Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown4Loading = tempunknown4Loading.AsReadOnly();

            // loading Unknown20
            (var tempunknown20Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown20Loading = tempunknown20Loading.AsReadOnly();

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MiniQuestStatesDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
