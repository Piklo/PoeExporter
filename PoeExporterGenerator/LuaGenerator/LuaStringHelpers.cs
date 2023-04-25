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
}
