namespace PoeData;

/// <summary>
/// Interface that config classes need to implement.
/// </summary>
public interface IConfig
{
    /// <summary>Gets poe installation directory.</summary>
    string PoePath { get; init; }
}