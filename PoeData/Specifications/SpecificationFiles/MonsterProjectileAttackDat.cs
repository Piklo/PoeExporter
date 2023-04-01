// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing MonsterProjectileAttack.dat data.
/// </summary>
public sealed partial class MonsterProjectileAttackDat : ISpecificationFile<MonsterProjectileAttackDat>
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets Projectile.</summary>
    public required int? Projectile { get; init; }

    /// <summary> Gets a value indicating whether Unknown20 is set.</summary>
    public required bool Unknown20 { get; init; }

    /// <summary> Gets a value indicating whether Unknown21 is set.</summary>
    public required bool Unknown21 { get; init; }

    /// <summary> Gets a value indicating whether Unknown22 is set.</summary>
    public required bool Unknown22 { get; init; }

    /// <summary> Gets Unknown23.</summary>
    public required int Unknown23 { get; init; }

    /// <inheritdoc/>
    public static MonsterProjectileAttackDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MonsterProjectileAttack.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterProjectileAttackDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetProjectilesDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Projectile
            (var projectileLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown21
            (var unknown21Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown22
            (var unknown22Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown23
            (var unknown23Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterProjectileAttackDat()
            {
                Id = idLoading,
                Projectile = projectileLoading,
                Unknown20 = unknown20Loading,
                Unknown21 = unknown21Loading,
                Unknown22 = unknown22Loading,
                Unknown23 = unknown23Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
