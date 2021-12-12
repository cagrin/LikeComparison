namespace LikeComparison.TransactSql
{
    using System;

    public static partial class LikeTransactSql
    {
        public static bool Like(this string matchExpression, string pattern, string escape)
        {
            if (escape == null)
            {
                throw new ArgumentNullException(nameof(escape), "Value cannot be null.");
            }

            return LikeString.Like(matchExpression, pattern, escape, LikeOptions);
        }

        public static string? LikeRegex(string pattern, string escape)
        {
            if (escape == null)
            {
                throw new ArgumentNullException(nameof(escape), "Value cannot be null.");
            }

            return LikeString.LikeRegex(pattern, escape, LikeOptions);
        }
    }
}