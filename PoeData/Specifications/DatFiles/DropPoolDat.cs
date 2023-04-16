﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DropPool.dat data.
/// </summary>
public sealed partial class DropPoolDat
{
    /// <summary> Gets Group.</summary>
    public required string Group { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required ReadOnlyCollection<int> Unknown12 { get; init; }

    /// <summary> Gets WeightHardmode.</summary>
    public required int WeightHardmode { get; init; }

    /// <summary>
    /// Gets DropPoolDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of DropPoolDat.</returns>
    internal static DropPoolDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/DropPool.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DropPoolDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Group
            (var groupLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var tempunknown12Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown12Loading = tempunknown12Loading.AsReadOnly();

            // loading WeightHardmode
            (var weighthardmodeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DropPoolDat()
            {
                Group = groupLoading,
                Weight = weightLoading,
                Unknown12 = unknown12Loading,
                WeightHardmode = weighthardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}