namespace LikeComparison.PostgreSql
{
    // https://www.postgresql.org/docs/current/functions-matching.html
    public static class LikePostgreSql
    {
        internal static readonly LikeOptions ILikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, CaseSensitivity = CaseSensitivity.CaseInsensitive };

        internal static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, CaseSensitivity = CaseSensitivity.CaseSensitive };

        public static bool ILike(this string? matchExpression, string pattern)
        {
            return LikeString.Like(matchExpression, pattern, ILikeOptions);
        }

        public static bool Like(this string? matchExpression, string pattern)
        {
            return LikeString.Like(matchExpression, pattern, LikeOptions);
        }

        public static string? ILikeRegex(string pattern)
        {
            return LikeString.LikeRegex(pattern, ILikeOptions);
        }

        public static string? LikeRegex(string pattern)
        {
            return LikeString.LikeRegex(pattern, LikeOptions);
        }
    }
}