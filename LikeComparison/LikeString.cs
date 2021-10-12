using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LikeComparison
{
    /*
        https://github.com/dotnet/vblang/blob/main/spec/expressions.md#like-operator
        https://github.com/dotnet/docs/blob/main/docs/visual-basic/language-reference/operators/like-operator.md
        https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.compilerservices.operators.likestring?view=netframework-4.8
        https://docs.microsoft.com/en-us/office/vba/language/reference/user-interface-help/like-operator
        https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.compilerservices.likeoperator.likestring?view=net-5.0
    */
    internal static class LikeString
    {
        internal static bool Like(string matchExpression, string pattern, LikeOptions likeOptions)
        {
            return Like(matchExpression, pattern, likeOptions.StringComparison, likeOptions.Wildcard, likeOptions.Single, likeOptions.Invert, likeOptions.Digits);
        }

        internal static string? LikeRegex(string pattern, LikeOptions likeOptions)
        {
            return LikeRegex(pattern, likeOptions.Wildcard, likeOptions.Single, likeOptions.Invert, likeOptions.Digits);
        }

        private static string? LikeRegex(string pattern, string wildcard, string single, string invert, string digits)
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
                        lastLetter = "\\" + letter;
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

        private static bool LikeParse(string matchExpression, string regexExpression)
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
#if NETSTANDARD2_1 || NETCOREAPP3_1_OR_GREATER
            catch (Exception ex)
#else
            catch (RegexParseException ex)
#endif
            {
                if (ex.Message.Contains("range in reverse order"))
                {
                    return false;
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private static bool Like(this string matchExpression, string pattern, StringComparison comparisonType, string wildcard, string single, string invert, string digits)
        {
            if (pattern == null) throw new ArgumentNullException("Value cannot be null. (Parameter 'pattern')");

            if (pattern.Contains(wildcard) || pattern.Contains(single) || pattern.Contains("[") || pattern.Contains("]") || pattern.Contains("^") || pattern.Contains(digits))
            {
                string? regexExpression = LikeRegex(pattern, wildcard, single, invert, digits);

                if (regexExpression != null)
                {
                    return LikeParse(matchExpression, regexExpression);
                }
                else
                {
                    return false;
                }
            }

            return matchExpression.Equals(pattern, comparisonType);
        }
    }
}