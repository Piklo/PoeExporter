using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace PoeExporterGenerator.LuaGenerator;

/// <summary>
/// Class containing helper methods for <see cref="LuaStringGenerator"/> and <see cref="LuaConverterGenerator"/>.
/// </summary>
internal static class LuaStringHelpers
{
    private const string InterfaceName = "ILuaExporter";

    /// <summary>
    /// Gets class declarations which implement the required interface.
    /// </summary>
    /// <param name="context">context.</param>
    /// <returns>class declarations.</returns>
    public static IEnumerable<ClassDeclarationSyntax> GetClassDeclarations(GeneratorExecutionContext context)
    {
        var classDeclarations = context.Compilation.SyntaxTrees
            .SelectMany(syntaxTree => syntaxTree.GetRoot().DescendantNodes())
            .OfType<ClassDeclarationSyntax>();
        var classes = new HashSet<ClassDeclarationSyntax>();
        foreach (var classDeclaration in classDeclarations)
        {
            if (classDeclaration.BaseList is null)
            {
                continue;
            }

            var test = classDeclaration.BaseList.Types;
            foreach (var item in classDeclaration.BaseList.Types)
            {
                if (item.Type is GenericNameSyntax { Identifier.ValueText: InterfaceName })
                {
                    classes.Add(classDeclaration);
                    break;
                }
            }
        }

        return classes;
    }

    /// <summary>
    /// Gets namespace string.
    /// </summary>
    /// <param name="item">class to look for namespace in.</param>
    /// <returns>namespace string.</returns>
    /// <exception cref="NamespaceNotFoundException">throw if namespace was not found.</exception>
    public static string GetNamespace(ClassDeclarationSyntax item)
    {
        if (item.Parent is BaseNamespaceDeclarationSyntax namespaceDeclarationSyntax)
        {
            return namespaceDeclarationSyntax.Name.ToString();
        }

        throw new NamespaceNotFoundException();
    }

    /// <summary>
    /// Gets namespace strings.
    /// </summary>
    /// <param name="classes">class declarations.</param>
    /// <returns>set of namespace strings.</returns>
    public static HashSet<string> GetNamespaces(IReadOnlyList<ClassDeclarationSyntax> classes)
    {
        var results = new HashSet<string>();

        foreach (var item in classes)
        {
            var namespaceString = GetNamespace(item);
            results.Add(namespaceString);
        }

        return results;
    }
}
