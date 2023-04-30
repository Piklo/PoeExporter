namespace PoeData;

/// <summary>
/// Interface that config classes need to implement.
/// </summary>
public interface IConfig
{
    /// <summary>Gets poe installation directory.</summary>
    string PoePath { get; init; }

    /// <summary>Gets a value indicating whether <see cref="DataLoader"/> should cache read files.</summary>
    bool CacheDataLoader { get; init; }
}