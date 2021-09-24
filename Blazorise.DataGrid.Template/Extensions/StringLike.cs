using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blazorise.DataGrid.Template.Extensions
{
    public static class StringLike
    {
        public static bool Like(this string matchExpression, string pattern, StringComparison comparisonType, string wildcard, string single)
        {
            if (pattern == null) throw new ArgumentNullException("Value cannot be null. (Parameter 'pattern')");

            if (pattern.Contains(wildcard) || pattern.Contains(single) || pattern.Contains("[") || pattern.Contains("]") || pattern.Contains("^"))
            {
                string[] letters = pattern.ToCharArray().Select(x => x.ToString()).ToArray();

                string regexExpression = "^";
                bool insideMatchSingleCharacter = false;
                bool isNotCase = false;
                foreach(string letter in letters)
                {

                    if (letter == "[" && insideMatchSingleCharacter)
                    {
                        if(isNotCase)
                        {
                            regexExpression = regexExpression + "\\[";
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (letter == "]" && !insideMatchSingleCharacter)
                    {
                        return false;
                    }

                    if (letter == "[" && !insideMatchSingleCharacter)
                    {
                        insideMatchSingleCharacter = true;
                        regexExpression = regexExpression + "<[>";
                    }
                    else if (letter == "]" && insideMatchSingleCharacter)
                    {
                        isNotCase = false;
                        insideMatchSingleCharacter = false;
                        regexExpression = regexExpression + "<]>";
                    }
                    else if (letter == "^" && insideMatchSingleCharacter)
                    {
                        isNotCase = true;
                        regexExpression = regexExpression + "^";
                    }
                    else if (letter == wildcard)
                    {
                        regexExpression = regexExpression + ".*";
                    }
                    else if (letter == single)
                    {
                        regexExpression = regexExpression + ".";
                    }
                    else
                    {
                        regexExpression = regexExpression + Regex.Escape(letter);
                    }
                }

                if (insideMatchSingleCharacter)
                {
                    return false;
                }

                if (pattern.Equals("[[^]"))
                {
                    return false;
                }
                if (regexExpression.Contains("<[><]>"))
                {
                    return false;
                }

                regexExpression = regexExpression.Replace("<[>", "[");
                regexExpression = regexExpression.Replace("<]>", "]");
                regexExpression = regexExpression.Replace("[^]", ".");

                regexExpression = regexExpression + "$";

                var regex = new Regex(regexExpression, RegexOptions.IgnoreCase);
                return regex.IsMatch(matchExpression);
            }

            return matchExpression.Contains(pattern, comparisonType);
        }

        public static bool Like(this string matchExpression, string pattern)
        {
            return Like(matchExpression, pattern, StringComparison.OrdinalIgnoreCase, "*", "?");
        }

        public static bool SqlLikeOperator(this string matchExpression, string pattern)
        {
            return Like(matchExpression, pattern, StringComparison.OrdinalIgnoreCase, "%", "_");
        }
    }
}