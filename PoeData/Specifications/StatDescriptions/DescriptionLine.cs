using System.Diagnostics;
using System.Globalization;
using System.Numerics;

namespace PoeData.Specifications.StatDescriptions;

internal sealed record DescriptionLine
{
    private readonly static DescriptionFormatter Formatter = new();
    private readonly Func<object, bool>[] predicates;

    /// <summary>Gets description line.</summary>
    internal string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DescriptionLine"/> class.
    /// </summary>
    /// <param name="descriptionLine">stat description line.</param>
    internal DescriptionLine(string descriptionLine)
    {
        Value = descriptionLine;
        predicates = Array.Empty<Func<object, bool>>();
    }

    internal bool IsMatching(IReadOnlyList<object> values)
    {
        if (values.Count != predicates.Length)
        {
            return false;
        }

        for (var i = 0; i < values.Count; i++)
        {
            var predicate = predicates[i];
            var value = values[i];
            var result = predicate(value);

            if (!result)
            {
                return false;
            }
        }

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
