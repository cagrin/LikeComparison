#if NET48
namespace LikeComparison
{
    using System.Text.RegularExpressions;
    using StringComparison = System.StringComparison;

    internal static partial class LikeString
    {
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return source?.IndexOf(value, comparisonType) >= 0; // Instead of: return source?.Contains(value);
        }

        public static bool Contains(this string source, char value, StringComparison comparisonType)
        {
            return source.IndexOf(value.ToString(), comparisonType) >= 0; // Instead of: return source?.Contains(value.ToString());
        }

        public static string Replace(this string source, string oldValue, string? newValue, StringComparison comparisonType)
        {
            return Regex.Replace(source, Regex.Escape(oldValue), newValue?.Replace("$", "$$"), comparisonType == StringComparison.OrdinalIgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None); // Instead of: return source.Replace(oldValue, newValue);
        }
    }
}
#endif