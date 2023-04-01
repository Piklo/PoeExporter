// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing AreaInfluenceDoodads.dat data.
/// </summary>
public sealed partial class AreaInfluenceDoodadsDat : ISpecificationFile<AreaInfluenceDoodadsDat>
{
    /// <summary> Gets StatsKey.</summary>
    public required int? StatsKey { get; init; }

    /// <summary> Gets StatValue.</summary>
    public required int StatValue { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required float Unknown20 { get; init; }

    /// <summary> Gets AOFiles.</summary>
    public required ReadOnlyCollection<string> AOFiles { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary> Gets Unknown45.</summary>
    public required string Unknown45 { get; init; }

    /// <summary> Gets Unknown53.</summary>
    public required int? Unknown53 { get; init; }

    /// <inheritdoc/>
    public static AreaInfluenceDoodadsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AreaInfluenceDoodads.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AreaInfluenceDoodadsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetStatsDat();

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatValue
            (var statvalueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading AOFiles
            (var tempaofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofilesLoading = tempaofilesLoading.AsReadOnly();

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown53
            (var unknown53Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AreaInfluenceDoodadsDat()
            {
                StatsKey = statskeyLoading,
                StatValue = statvalueLoading,
                Unknown20 = unknown20Loading,
                AOFiles = aofilesLoading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown45 = unknown45Loading,
                Unknown53 = unknown53Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
