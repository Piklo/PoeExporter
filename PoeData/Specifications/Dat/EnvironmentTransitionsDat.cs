// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing EnvironmentTransitions.dat data.
/// </summary>
public sealed partial class EnvironmentTransitionsDat : IDat<EnvironmentTransitionsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets OTFiles.</summary>
    public required ReadOnlyCollection<string> OTFiles { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required ReadOnlyCollection<string> Unknown24 { get; init; }

    /// <inheritdoc/>
    public static EnvironmentTransitionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/EnvironmentTransitions.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EnvironmentTransitionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OTFiles
            (var tempotfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var otfilesLoading = tempotfilesLoading.AsReadOnly();

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EnvironmentTransitionsDat()
            {
                Id = idLoading,
                OTFiles = otfilesLoading,
                Unknown24 = unknown24Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
