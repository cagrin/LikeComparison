namespace LikeComparison.TransactSql
{
    using System;

    public static partial class LikeTransactSql
    {
        public static bool Like(this string? matchExpression, string pattern, string escape)
        {
            if (escape == null)
            {
                throw new ArgumentNullException(nameof(escape), "Value cannot be null.");
            }

            LikeOptions likeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, Escape = escape };

            return LikeString.Like(matchExpression, pattern, likeOptions);
        }

        public static string? LikeRegex(string pattern, string escape)
        {
            if (escape == null)
            {
                throw new ArgumentNullException(nameof(escape), "Value cannot be null.");
            }

            LikeOptions likeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, Escape = escape };

            return LikeString.LikeRegex(pattern, likeOptions);
        }
    }
}