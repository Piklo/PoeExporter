using PoeData.Specifications.StatDescriptions.DescriptionLinePredicates;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;

namespace PoeData.Specifications.StatDescriptions;

internal sealed record DescriptionLine
{
    private readonly static DescriptionFormatter Formatter = new();
    private readonly IDescriptionLinePredicate[] predicates;

    /// <summary>Gets description line.</summary>
    internal string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DescriptionLine"/> class.
    /// </summary>
    /// <param name="descriptionLine">stat description line.</param>
    internal DescriptionLine(string descriptionLine)
    {
        Value = descriptionLine;

        var quotationIndex = descriptionLine.IndexOf('"');
        var lastQuotationIndex = descriptionLine.LastIndexOf('"');
        var predicatesString = descriptionLine[..quotationIndex];
        predicates = ParsePredicates(predicatesString);
        var text = descriptionLine[(quotationIndex + 1)..lastQuotationIndex];
        var rest = descriptionLine[(lastQuotationIndex + 1)..];
    }

    private static IDescriptionLinePredicate[] ParsePredicates(string predicatesString)
    {
        var predicateStrings = predicatesString.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var predicates = new IDescriptionLinePredicate[predicateStrings.Length];

        for (var i = 0; i < predicates.Length; i++)
        {
            var str = predicateStrings[i];
            if (str == "#")
            {
                predicates[i] = new PredicateAnyValue();
            }
            else if (str.StartsWith('!'))
            {
                var value = int.Parse(str[1..]);
                predicates[i] = new PredicateNotSpecificValue(value);
            }
            else if (str.StartsWith('#'))
            {
                var max = int.Parse(str[2..]);
                predicates[i] = new PredicateLessThan(max);
            }
            else if (str.EndsWith('#'))
            {
                var min = int.Parse(str[..str.IndexOf('|')]);
                predicates[i] = new PredicateGreaterThan(min);
            }
            else if (str.Contains('|'))
            {
                var index = str.IndexOf('|');
                var min = int.Parse(str[..index]);
                var max = int.Parse(str[(index + 1)..]);
                predicates[i] = new PredicateInRange(min, max);
            }
            else
            {
                var value = int.Parse(str);
                predicates[i] = new PredicateSpecificValue(value);
            }
        }

        return predicates;
    }

    internal bool IsMatching(IReadOnlyList<object> values)
    {
        if (values.Count != predicates.Length)
        {
            return false;
        }

        // TOOD fix me
        //for (var i = 0; i < values.Count; i++)
        //{
        //    var predicate = predicates[i];
        //    var value = values[i];
        //    var result = predicate.Matches(value);

        //    if (!result)
        //    {
        //        return false;
        //    }
        //}

        return true;
    }

    internal string Format(IReadOnlyList<object> values)
    {
        return string.Format(Formatter, Value, values);
    }

    private sealed class DescriptionFormatter : ICustomFormatter, IFormatProvider
    {
        public string Format(string? format, object? arg, IFormatProvider? formatProvider)
        {
            if (format == "+d")
            {
                if (arg is byte b)
                {
                    return ParseValue(b);
                }
                else if (arg is sbyte sb)
                {
                    return ParseValue(sb);
                }
                else if (arg is short s)
                {
                    return ParseValue(s);
                }
                else if (arg is ushort us)
                {
                    return ParseValue(us);
                }
                else if (arg is int i)
                {
                    return ParseValue(i);
                }
                else if (arg is uint ui)
                {
                    return ParseValue(ui);
                }
                else if (arg is long l)
                {
                    return ParseValue(l);
                }
                else if (arg is ulong ul)
                {
                    return ParseValue(ul);
                }
                else
                {
                    throw new UnreachableException();
                }
            }

            return DefaultFormat(format, arg);
        }

        private static string ParseValue<T>(T value)
            where T : notnull, INumber<T>
        {
            if (value > T.Zero)
            {
                return $"+{value}";
            }
            else
            {
#pragma warning disable CS8603 // Possible null reference return. // ???
                return value.ToString();
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public object? GetFormat(Type? formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        private static string DefaultFormat(string? format, object? arg)
        {
            if (arg is IFormattable value)
            {
                return value.ToString(format, CultureInfo.CurrentCulture);
            }
            else if (arg is not null)
            {
#pragma warning disable CS8603 // Possible null reference return. // ???
                return arg.ToString();
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
