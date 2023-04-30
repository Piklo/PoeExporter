using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading;

namespace PoeExporterGenerator;

/// <summary>
/// Class with generic helper methods.
/// </summary>
internal static class Helpers
{
    /// <summary>
    /// Checks for attribute in a list of attributes.
    /// </summary>
    /// <param name="attributeLists">lists of attributes.</param>
    /// <param name="shortAttribute">short name of the attribute.</param>
    /// <param name="fullAttribute">long name of the attribute.</param>
    /// <param name="cancellationToken">cancellation token.</param>
    /// <returns>True if the attribute is found, false otherwise.</returns>
    internal static bool HasAttribute(
        SyntaxList<AttributeListSyntax> attributeLists,
        string shortAttribute,
        string fullAttribute,
        CancellationToken cancellationToken)
    {
        foreach (var attributes in attributeLists)
        {
            cancellationToken.ThrowIfCancellationRequested();
            foreach (var attribute in attributes.Attributes)
            {
                if (attribute.Name is not IdentifierNameSyntax identifierNameSyntax)
                {
                    continue;
                }

                if (identifierNameSyntax.Identifier.ValueText == shortAttribute || identifierNameSyntax.Identifier.ValueText == fullAttribute)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
