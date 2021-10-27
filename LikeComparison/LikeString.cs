using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LikeComparison
{
    internal static class LikeString
    {
        private const StringComparison ignoreCase = StringComparison.OrdinalIgnoreCase;

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
            foreach (string letter in letters)
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
                    if (lastLetter == "<[>")
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

            if (regexExpression.Contains("<[><]>", ignoreCase))
            {
                return null;
            }

            regexExpression = regexExpression.Replace("<[>", "[", ignoreCase);
            regexExpression = regexExpression.Replace("<]>", "]", ignoreCase);
            regexExpression = regexExpression.Replace("[^]", ".", ignoreCase);

            return regexExpression + "$";
        }

        private static bool LikeParse(string matchExpression, string regexExpression)
        {
            try
            {
                var regex = new Regex(regexExpression, RegexOptions.IgnoreCase);
                return regex.IsMatch(matchExpression);
            }
#if NET5_0_OR_GREATER
            catch (RegexParseException)
            {
                return false;
            }
#else
            catch (Exception)
            {
                throw;
            }
#endif
        }

        private static bool Like(this string matchExpression, string pattern, StringComparison comparisonType, string wildcard, string single, string invert, string digits)
        {
            if (pattern == null) throw new ArgumentNullException(nameof(pattern), "Value cannot be null.");

            if (pattern.Contains(wildcard, ignoreCase) || pattern.Contains(single, ignoreCase) || pattern.Contains('[', ignoreCase) || pattern.Contains(']', ignoreCase) || pattern.Contains('^', ignoreCase) || pattern.Contains(digits, ignoreCase))
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