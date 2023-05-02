﻿using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PoeExporterGenerator;

/// <summary>
/// Class used to generate to DatJsonExporter methods.
/// </summary>
[Generator]
internal sealed class DatJsonExporterGenerator : IIncrementalGenerator
{
    private const string SpecificationClassName = "Specification";
    private const string DatJsonExporterClassName = "DatJsonExporter";
    private const string DatJsonExporterNamespace = "PoeExporter.JsonExporters";
    private const string SpecificationVarName = "specification";
    private const string LoggerFieldName = "logger";
    private const string ExceptionCounterFieldName = "exceptionCounter";
    private const string ThrowOnExceptionFieldName = "throwOnException";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var loadMethodNames = context.CompilationProvider.Select(GetLoadMethodNames);

        context.RegisterSourceOutput(loadMethodNames, Output);
    }

    private static IReadOnlyList<string> GetLoadMethodNames(Compilation compilation, CancellationToken cancellationToken)
    {
        var specification = GetSpecification(compilation);

        var members = specification.GetMembers();

        var result = new List<string>();

        foreach (var member in members)
        {
            if (!member.Name.StartsWith("Load"))
            {
                continue;
            }

            result.Add(member.Name);
        }

        return result.ToArray();
    }

    private static ITypeSymbol GetSpecification(Compilation compilation)
    {
        var poeData = compilation.GlobalNamespace.GetNamespaceMembers()
            .Where(x => x.Name == "PoeData").First();

        var types = compilation.SourceModule.ReferencedAssemblySymbols.SelectMany(a =>
        {
            try
            {
                var main = a.Identity.Name
                .Split('.')
                .Aggregate(a.GlobalNamespace, (s, c) => s.GetNamespaceMembers().Single(m => m.Name.Equals(c)));

                return GetAllTypes(main);
            }
            catch
            {
                return Enumerable.Empty<ITypeSymbol>();
            }
        });

        var specification = types.Where(x => x.Name == SpecificationClassName).First();
        return specification;
    }

    // https://stackoverflow.com/a/68733955
    private static IEnumerable<ITypeSymbol> GetAllTypes(INamespaceSymbol root)
    {
        foreach (var namespaceOrTypeSymbol in root.GetMembers())
        {
            if (namespaceOrTypeSymbol is INamespaceSymbol @namespace)
            {
                foreach (var nested in GetAllTypes(@namespace))
                {
                    yield return nested;
                }
            }
            else if (namespaceOrTypeSymbol is ITypeSymbol type)
            {
                yield return type;
            }
        }
    }

    private void Output(SourceProductionContext context, IReadOnlyList<string> loadMethods)
    {
        var builder = new StringBuilder();
        builder.AppendLine($$"""
            // <auto-generated/>
            using System;

            namespace {{DatJsonExporterNamespace}};

            internal sealed partial class {{DatJsonExporterClassName}}
            {
            """);

        AddLoadMethods(builder, loadMethods);
        AddRunAllMethod(builder, loadMethods);

        builder.AppendLine("}"); // class end bracket

        var source = builder.ToString();

        // Add the source code to the compilation
        context.AddSource($"{DatJsonExporterClassName}.g.cs", source);
    }

    private void AddLoadMethods(StringBuilder builder, IReadOnlyList<string> loadMethods)
    {
        foreach (var method in loadMethods)
        {
            AddLoadMethod(builder, method);
        }
    }

    private static void AddLoadMethod(StringBuilder builder, string method)
    {
        var fileName = method.Substring(4);
        builder.AppendLine($$"""

                private void {{method}}()
                {
                    try
                    {
                        var res = {{SpecificationVarName}}.{{method}}();
                        Save(res, "{{fileName}}");
                    }
                    catch (Exception e)
                    {
                        {{LoggerFieldName}}.Error("failed to load {name}\n{error}", "{{method}}", e);
                        {{ExceptionCounterFieldName}}++;

                        if ({{ThrowOnExceptionFieldName}})
                        {
                            throw e;
                        }
                    }
                }
            """);
    }

    private void AddRunAllMethod(StringBuilder builder, IReadOnlyList<string> methods)
    {
        builder.AppendLine($$"""

                private partial void RunAll()
                {
                    // {{LoggerFieldName}}.Debug("running code generated code");
            """);

        foreach (var method in methods)
        {
            builder.AppendLine($"""
                        {method}();
                """);
        }

        builder.AppendLine("""
                }
            """);
    }
}
