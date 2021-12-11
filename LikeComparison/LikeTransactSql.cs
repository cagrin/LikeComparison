namespace LikeComparison.TransactSql
{
    using System;

    // https://docs.microsoft.com/en-us/sql/t-sql/language-elements/like-transact-sql
    public static class LikeTransactSql
    {
        private static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql };

        public static bool Like(this string matchExpression, string pattern)
        {
            return LikeString.Like(matchExpression, pattern, LikeOptions);
        }

        public static bool Like(this string matchExpression, string pattern, string escape)
        {
            if (escape == null)
            {
                throw new ArgumentNullException(nameof(escape), "Value cannot be null.");
            }

            return LikeString.Like(matchExpression, pattern, LikeOptions);
        }

        public static string? LikeRegex(string pattern)
        {
            return LikeString.LikeRegex(pattern, LikeOptions);
        }

        public static string? LikeRegex(string pattern, string escape)
        {
            if (escape == null)
            {
                throw new ArgumentNullException(nameof(escape), "Value cannot be null.");
            }

            return LikeString.LikeRegex(pattern, LikeOptions);
        }
    }
}