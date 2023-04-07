// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MavenJewelRadiusKeystones.dat data.
/// </summary>
public sealed partial class MavenJewelRadiusKeystonesDat
{
    /// <summary> Gets Keystone.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.GetPassiveSkillsDat"/> index.</remarks>
    public required int? Keystone { get; init; }

    /// <summary>
    /// Gets MavenJewelRadiusKeystonesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MavenJewelRadiusKeystonesDat.</returns>
    internal static MavenJewelRadiusKeystonesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MavenJewelRadiusKeystones.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MavenJewelRadiusKeystonesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Keystone
            (var keystoneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MavenJewelRadiusKeystonesDat()
            {
                Keystone = keystoneLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
