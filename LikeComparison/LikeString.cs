namespace LikeComparison
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal static class LikeString
    {
        internal static bool Like(string matchExpression, string pattern, LikeOptions likeOptions)
        {
            return Like(matchExpression, pattern, escape: string.Empty, likeOptions.StringComparison, likeOptions.Wildcard, likeOptions.Single, likeOptions.Invert, likeOptions.Digits);
        }

        internal static bool Like(string matchExpression, string pattern, string escape, LikeOptions likeOptions)
        {
            return Like(matchExpression, pattern, escape, likeOptions.StringComparison, likeOptions.Wildcard, likeOptions.Single, likeOptions.Invert, likeOptions.Digits);
        }

        internal static string? LikeRegex(string pattern, LikeOptions likeOptions)
        {
            return LikeRegex(pattern, escape: string.Empty, likeOptions);
        }

        internal static string? LikeRegex(string pattern, string escape, LikeOptions likeOptions)
        {
            return LikeRegex(pattern, escape, likeOptions.StringComparison, likeOptions.Wildcard, likeOptions.Single, likeOptions.Invert, likeOptions.Digits);
        }

        private static string? LikeRegex(string pattern, string escape, StringComparison comparisonType, string wildcard, string single, string invert, string digits)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern), "Value cannot be null.");
            }

            string[] letters = pattern.ToCharArray().Select(x => x.ToString()).ToArray();

            string regexExpression = "^";

            bool insideMatchSingleCharacter = false;
            string lastLetter = string.Empty;
            foreach (string letter in letters)
            {
                if (letter == escape && !insideMatchSingleCharacter)
                {
                    lastLetter = escape;
                    continue;
                }

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
                else if (lastLetter == escape && !insideMatchSingleCharacter)
                {
                    lastLetter = Regex.Escape(letter);
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
                else if (letter == wildcard && !insideMatchSingleCharacter)
                {
                    lastLetter = ".*";
                }
                else if (letter == single && !insideMatchSingleCharacter)
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

                regexExpression += lastLetter;
            }

            if (insideMatchSingleCharacter)
            {
                return null;
            }

            if (regexExpression.Contains("<[><]>", comparisonType))
            {
                return null;
            }

            regexExpression = regexExpression.Replace("<[>", "[", comparisonType);
            regexExpression = regexExpression.Replace("<]>", "]", comparisonType);
            regexExpression = regexExpression.Replace("[^]", ".", comparisonType);

            return regexExpression + "$";
        }

        private static bool LikeParse(string matchExpression, string regexExpression, StringComparison comparisonType)
        {
            try
            {
                var options = comparisonType == StringComparison.OrdinalIgnoreCase ? RegexOptions.IgnoreCase : 0;
                var regex = new Regex(regexExpression, options);
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

        private static bool Like(this string matchExpression, string pattern, string escape, StringComparison comparisonType, string wildcard, string single, string invert, string digits)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern), "Value cannot be null.");
            }

            if (pattern.Contains(wildcard, comparisonType) || pattern.Contains(single, comparisonType) || pattern.Contains('[', comparisonType) || pattern.Contains(']', comparisonType) || pattern.Contains('^', comparisonType) || pattern.Contains(digits, comparisonType))
            {
                string? regexExpression = LikeRegex(pattern, escape, comparisonType, wildcard, single, invert, digits);

                if (regexExpression != null)
                {
                    return LikeParse(matchExpression, regexExpression, comparisonType);
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