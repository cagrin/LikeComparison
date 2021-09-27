using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blazorise.DataGrid.Template.Extensions
{
    /*
        https://github.com/dotnet/vblang/blob/main/spec/expressions.md#like-operator
        https://github.com/dotnet/docs/blob/main/docs/visual-basic/language-reference/operators/like-operator.md
        https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.compilerservices.operators.likestring?view=netframework-4.8
    */
    public static class StringLike
    {
        public static bool Like(this string matchExpression, string pattern, StringComparison comparisonType, string wildcard, string single, string invert, string digits)
        {
            if (pattern == null) throw new ArgumentNullException("Value cannot be null. (Parameter 'pattern')");

            if (pattern.Contains(wildcard) || pattern.Contains(single) || pattern.Contains("[") || pattern.Contains("]") || pattern.Contains("^") || pattern.Contains(digits))
            {
                string regexExpression = SqlLikeRegex(pattern, wildcard, single, invert, digits);

                return SqlLikeParse(matchExpression, regexExpression);
            }

            return matchExpression.Equals(pattern, comparisonType);
        }

        public static bool Like(this string matchExpression, string pattern)
        {
            return Like(matchExpression, pattern, StringComparison.OrdinalIgnoreCase, "*", "?", "!", "#");
        }

        public static bool SqlLikeOperator(this string matchExpression, string pattern)
        {
            return Like(matchExpression, pattern, StringComparison.OrdinalIgnoreCase, "%", "_", "^", "[0-9]");
        }

        public static string SqlLikeRegex(string pattern, string wildcard = "%", string single = "_", string invert = "^", string digits = "#")
        {
            string[] letters = pattern.ToCharArray().Select(x => x.ToString()).ToArray();

            string regexExpression = "^";

            bool insideMatchSingleCharacter = false;
            string lastLetter = string.Empty;
            foreach(string letter in letters)
            {
                if (letter == "[" && insideMatchSingleCharacter)
                {
                    lastLetter = "\\[";
                }
                else if (letter == "]" && !insideMatchSingleCharacter)
                {
                    lastLetter = "\\]";
                }
                else if (letter == "[" && !insideMatchSingleCharacter)
                {
                    insideMatchSingleCharacter = true;
                    lastLetter = "<[>";
                }
                else if (letter == "]" && insideMatchSingleCharacter)
                {
                    insideMatchSingleCharacter = false;
                    lastLetter = "<]>";
                }
                else if (letter == invert && insideMatchSingleCharacter)
                {
                    if(lastLetter == "<[>")
                    {
                        lastLetter = "^";
                    }
                    else
                    {
                        lastLetter = "\\^";
                    }
                }
                else if (letter == wildcard)
                {
                    lastLetter = ".*";
                }
                else if (letter == single)
                {
                    lastLetter = ".";
                }
                else if (letter == digits && !insideMatchSingleCharacter)
                {
                    lastLetter = "<[>0-9<]>";
                }
                else
                {
                    lastLetter = Regex.Escape(letter);
                }

                regexExpression = regexExpression + lastLetter;
            }

            if (insideMatchSingleCharacter)
            {
                return null;
            }

            if (regexExpression.Contains("<[><]>"))
            {
                return null;
            }

            regexExpression = regexExpression.Replace("<[>", "[");
            regexExpression = regexExpression.Replace("<]>", "]");
            regexExpression = regexExpression.Replace("[^]", ".");

            return regexExpression + "$";
        }

        private static bool SqlLikeParse(string matchExpression, string regexExpression)
        {
            if (regexExpression == null)
            {
                return false;
            }

            try
            {
                var regex = new Regex(regexExpression, RegexOptions.IgnoreCase);
                return regex.IsMatch(matchExpression);
            }
            catch (RegexParseException ex)
            {
                if (ex.Message.Contains("range in reverse order"))
                {
                    return false;
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}