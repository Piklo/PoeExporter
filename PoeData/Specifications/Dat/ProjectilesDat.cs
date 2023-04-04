// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Projectiles.dat data.
/// </summary>
public sealed partial class ProjectilesDat : IDat<ProjectilesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets AOFiles.</summary>
    public required ReadOnlyCollection<string> AOFiles { get; init; }

    /// <summary> Gets LoopAnimationIds.</summary>
    public required ReadOnlyCollection<string> LoopAnimationIds { get; init; }

    /// <summary> Gets ImpactAnimationIds.</summary>
    public required ReadOnlyCollection<string> ImpactAnimationIds { get; init; }

    /// <summary> Gets ProjectileSpeed.</summary>
    public required int ProjectileSpeed { get; init; }

    /// <summary> Gets a value indicating whether Unknown60 is set.</summary>
    public required bool Unknown60 { get; init; }

    /// <summary> Gets Unknown61.</summary>
    public required int Unknown61 { get; init; }

    /// <summary> Gets a value indicating whether Unknown65 is set.</summary>
    public required bool Unknown65 { get; init; }

    /// <summary> Gets a value indicating whether Unknown66 is set.</summary>
    public required bool Unknown66 { get; init; }

    /// <summary> Gets InheritsFrom.</summary>
    public required string InheritsFrom { get; init; }

    /// <summary> Gets Unknown75.</summary>
    public required int Unknown75 { get; init; }

    /// <summary> Gets Unknown79.</summary>
    public required int? Unknown79 { get; init; }

    /// <summary> Gets Unknown95.</summary>
    public required int Unknown95 { get; init; }

    /// <summary> Gets a value indicating whether Unknown99 is set.</summary>
    public required bool Unknown99 { get; init; }

    /// <summary> Gets a value indicating whether Unknown100 is set.</summary>
    public required bool Unknown100 { get; init; }

    /// <summary> Gets Stuck_AOFile.</summary>
    public required ReadOnlyCollection<string> Stuck_AOFile { get; init; }

    /// <summary> Gets Bounce_AOFile.</summary>
    public required string Bounce_AOFile { get; init; }

    /// <summary> Gets Unknown125.</summary>
    public required int Unknown125 { get; init; }

    /// <summary> Gets Unknown129.</summary>
    public required int Unknown129 { get; init; }

    /// <summary> Gets Unknown133.</summary>
    public required int Unknown133 { get; init; }

    /// <summary> Gets Unknown137.</summary>
    public required int Unknown137 { get; init; }

    /// <summary> Gets Unknown141.</summary>
    public required int? Unknown141 { get; init; }

    /// <summary> Gets Unknown157.</summary>
    public required int? Unknown157 { get; init; }

    /// <summary> Gets Unknown173.</summary>
    public required int Unknown173 { get; init; }

    /// <summary> Gets Unknown177.</summary>
    public required int Unknown177 { get; init; }

    /// <summary> Gets Unknown181.</summary>
    public required int Unknown181 { get; init; }

    /// <summary> Gets Unknown185.</summary>
    public required int Unknown185 { get; init; }

    /// <summary> Gets Unknown189.</summary>
    public required int Unknown189 { get; init; }

    /// <summary> Gets Unknown193.</summary>
    public required ReadOnlyCollection<string> Unknown193 { get; init; }

    /// <summary> Gets a value indicating whether Unknown209 is set.</summary>
    public required bool Unknown209 { get; init; }

    /// <summary> Gets Unknown210.</summary>
    public required ReadOnlyCollection<int> Unknown210 { get; init; }

    /// <inheritdoc/>
    public static ProjectilesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Projectiles.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ProjectilesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AOFiles
            (var tempaofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofilesLoading = tempaofilesLoading.AsReadOnly();

            // loading LoopAnimationIds
            (var temploopanimationidsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var loopanimationidsLoading = temploopanimationidsLoading.AsReadOnly();

            // loading ImpactAnimationIds
            (var tempimpactanimationidsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var impactanimationidsLoading = tempimpactanimationidsLoading.AsReadOnly();

            // loading ProjectileSpeed
            (var projectilespeedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading InheritsFrom
            (var inheritsfromLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown75
            (var unknown75Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown95
            (var unknown95Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Stuck_AOFile
            (var tempstuck_aofileLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var stuck_aofileLoading = tempstuck_aofileLoading.AsReadOnly();

            // loading Bounce_AOFile
            (var bounce_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown125
            (var unknown125Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown129
            (var unknown129Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown133
            (var unknown133Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown137
            (var unknown137Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown157
            (var unknown157Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown173
            (var unknown173Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown177
            (var unknown177Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown181
            (var unknown181Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown185
            (var unknown185Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown189
            (var unknown189Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown193
            (var tempunknown193Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown193Loading = tempunknown193Loading.AsReadOnly();

            // loading Unknown209
            (var unknown209Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown210
            (var tempunknown210Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown210Loading = tempunknown210Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ProjectilesDat()
            {
                Id = idLoading,
                AOFiles = aofilesLoading,
                LoopAnimationIds = loopanimationidsLoading,
                ImpactAnimationIds = impactanimationidsLoading,
                ProjectileSpeed = projectilespeedLoading,
                Unknown60 = unknown60Loading,
                Unknown61 = unknown61Loading,
                Unknown65 = unknown65Loading,
                Unknown66 = unknown66Loading,
                InheritsFrom = inheritsfromLoading,
                Unknown75 = unknown75Loading,
                Unknown79 = unknown79Loading,
                Unknown95 = unknown95Loading,
                Unknown99 = unknown99Loading,
                Unknown100 = unknown100Loading,
                Stuck_AOFile = stuck_aofileLoading,
                Bounce_AOFile = bounce_aofileLoading,
                Unknown125 = unknown125Loading,
                Unknown129 = unknown129Loading,
                Unknown133 = unknown133Loading,
                Unknown137 = unknown137Loading,
                Unknown141 = unknown141Loading,
                Unknown157 = unknown157Loading,
                Unknown173 = unknown173Loading,
                Unknown177 = unknown177Loading,
                Unknown181 = unknown181Loading,
                Unknown185 = unknown185Loading,
                Unknown189 = unknown189Loading,
                Unknown193 = unknown193Loading,
                Unknown209 = unknown209Loading,
                Unknown210 = unknown210Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
