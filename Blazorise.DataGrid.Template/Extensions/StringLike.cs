using System;

namespace Blazorise.DataGrid.Template.Extensions
{
    public static class StringLike
    {
        public static bool Like(this string matchExpression, string pattern, StringComparison comparisonType, string wildcard, string single)
        {
            if (pattern == null) throw new ArgumentNullException("Value cannot be null. (Parameter 'pattern')");

            if (pattern.Contains(wildcard) || pattern.Contains(single))
            {
                string newPattern = pattern.Replace(wildcard+wildcard, wildcard);
                while (newPattern != pattern)
                {
                    pattern = newPattern;
                    newPattern = pattern.Replace(wildcard+wildcard, wildcard);
                }

                int matched = 0;
                for (int i = 0; i < pattern.Length; )
                {
                    if (matched > matchExpression.Length)
                    {
                        return false;
                    }

                    string c = pattern[i++].ToString();
                    if (c == wildcard)
                    {
                        if (i < pattern.Length)
                        {
                            string next = pattern[i].ToString();
                            int j = matchExpression.IndexOf(next, matched, comparisonType);
                            if (j < 0)
                            {
                                return false;
                            }
                            else
                            {
                                if(matchExpression.Length > j)
                                {
                                    string subExpression = matchExpression.Substring(j + 1);
                                    string subPattern = pattern.Substring(i - 1);
                                    bool inception = subExpression.Like(subPattern, comparisonType, wildcard, single);
                                    if (inception)
                                    {
                                        return true;
                                    }
                                }
                            }

                            matched = j;
                        }
                        else
                        {
                            matched = matchExpression.Length;
                            break;
                        }
                    }
                    else if (c == single)
                    {
                        matched++;
                    }
                    else
                    {
                        if (matched >= matchExpression.Length)
                        {
                            return false;
                        }

                        string m = matchExpression[matched].ToString();
                        bool areEqual = m.Equals(c, comparisonType);

                        if (!areEqual)
                        {
                            return false;
                        }

                        matched++;
                    }
                }

                return (matched == matchExpression.Length);
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