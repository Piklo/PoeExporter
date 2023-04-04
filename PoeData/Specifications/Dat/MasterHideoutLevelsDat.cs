// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MasterHideoutLevels.dat data.
/// </summary>
public sealed partial class MasterHideoutLevelsDat : ISpecificationFile<MasterHideoutLevelsDat>
{
    /// <summary> Gets NPCMasterKey.</summary>
    /// <remarks> references <see cref="NPCMasterDat"/> on <see cref="Specification.GetNPCMasterDat"/> index.</remarks>
    public required int? NPCMasterKey { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets MissionsRequired.</summary>
    public required int MissionsRequired { get; init; }

    /// <inheritdoc/>
    public static MasterHideoutLevelsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MasterHideoutLevels.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MasterHideoutLevelsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading NPCMasterKey
            (var npcmasterkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MissionsRequired
            (var missionsrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MasterHideoutLevelsDat()
            {
                NPCMasterKey = npcmasterkeyLoading,
                Level = levelLoading,
                MissionsRequired = missionsrequiredLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
