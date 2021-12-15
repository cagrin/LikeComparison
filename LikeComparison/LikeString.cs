namespace LikeComparison
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal static partial class LikeString
    {
        internal static bool Like(string matchExpression, string pattern, LikeOptions likeOptions)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern), "Value cannot be null.");
            }

            return Like(matchExpression, pattern, likeOptions.Escape, likeOptions.StringComparison, likeOptions.Wildcard, likeOptions.Single, likeOptions.Invert, likeOptions.Digits);
        }

        internal static string? LikeRegex(string pattern, LikeOptions likeOptions)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern), "Value cannot be null.");
            }

            return LikeRegex(pattern, likeOptions.Escape, likeOptions.StringComparison, likeOptions.Wildcard, likeOptions.Single, likeOptions.Invert, likeOptions.Digits);
        }

        private static bool Like(this string matchExpression, string pattern, string escape, StringComparison comparisonType, string wildcard, string single, string invert, string digits)
        {
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

        private static string? LikeRegex(string pattern, string escape, StringComparison comparisonType, string wildcard, string single, string invert, string digits)
        {
            string[] letters = pattern.ToCharArray().Select(x => x.ToString()).ToArray();

            string regexExpression = "^";
            bool escaped = !string.IsNullOrEmpty(escape);

            bool insideMatchSingleCharacter = false;
            string lastLetter = string.Empty;
            foreach (string letter in letters)
            {
                if (!insideMatchSingleCharacter)
                {
                    if (escaped && letter == escape)
                    {
                        lastLetter = escape;
                        continue;
                    }

                    if (escaped && lastLetter == escape)
                    {
                        lastLetter = Regex.Escape(letter);
                    }
                    else if (letter == "]")
                    {
                        lastLetter = "\\]";
                    }
                    else if (letter == "[")
                    {
                        insideMatchSingleCharacter = true;
                        lastLetter = "<[>";
                    }
                    else if (letter == wildcard)
                    {
                        lastLetter = ".*";
                    }
                    else if (letter == single)
                    {
                        lastLetter = ".";
                    }
                    else if (letter == digits)
                    {
                        lastLetter = "<[>0-9<]>";
                    }
                    else
                    {
                        lastLetter = Regex.Escape(letter);
                    }
                }
                else
                {
                    if (letter == "[")
                    {
                        lastLetter = "\\[";
                    }
                    else if (letter == "]")
                    {
                        insideMatchSingleCharacter = false;
                        lastLetter = "<]>";
                    }
                    else if (letter == invert)
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
                    else
                    {
                        lastLetter = Regex.Escape(letter);
                    }
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
    }
}