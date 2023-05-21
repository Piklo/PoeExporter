namespace SpecificationGenerator;

internal readonly record struct LineOfCode(string Value, int Indentation)
{
    /// <summary>
    /// Splits a string of code into list of <see cref="LineOfCode"/>.
    /// </summary>
    /// <param name="code">code to split</param>
    /// <returns>split code.</returns>
    public static IReadOnlyList<LineOfCode> Split(string code)
    {
        var resulsts = new List<LineOfCode>();
        var lines = code.Split("\r\n");

        foreach (var line in lines)
        {
            var trimmed = line.TrimStart();
            var difference = line.Length - trimmed.Length;
            var indentation = difference / 4;
            var parsedLine = new LineOfCode(trimmed, indentation);
            resulsts.Add(parsedLine);
        }

        return resulsts;
    }
}
