using System;

namespace Blazorise.DataGrid.Template.Extensions
{
    public static class StringExtensions
    {
        public static bool Like(this string matchExpression, string pattern, StringComparison comparisonType)
        {
            if (pattern == null) throw new ArgumentNullException("Value cannot be null. (Parameter 'pattern')");

            if (pattern.Contains("*"))
            {
                int matched = 0;
                for (int i = 0; i < pattern.Length; )
                {
                    if (matched > matchExpression.Length)
                    {
                        return false;
                    }

                    string c = pattern[i++].ToString();
                    if (c == "*")
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
                                    bool inception = subExpression.Like(subPattern, comparisonType);
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
            return Like(matchExpression, pattern, StringComparison.OrdinalIgnoreCase);
        }
    }
}