using System;

namespace Blazorise.DataGrid.Template.Extensions
{
    public static class StringExtensions
    {
        public static bool Like(this string matchExpression, string pattern)
        {
            bool startsWithAsterix = pattern.StartsWith("*");
            bool endsWithAsterix = pattern.EndsWith("*");

            if (pattern.Length > 1)
            {
                if (startsWithAsterix && !endsWithAsterix)
                {
                    pattern = pattern.Substring(1);
                    return matchExpression.EndsWith(pattern);
                }
                else if (!startsWithAsterix && endsWithAsterix)
                {
                    pattern = pattern.Substring(0, pattern.Length - 1);
                    return matchExpression.StartsWith(pattern);
                }
            }

            if (pattern.Length > 2)
            {
                if (startsWithAsterix && endsWithAsterix)
                {
                    pattern = pattern.Substring(1, pattern.Length - 2);
                }
            }

            return matchExpression.Contains(pattern);
        }
    }
}