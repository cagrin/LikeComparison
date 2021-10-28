namespace LikeComparison.PostgreSql
{
    // https://www.postgresql.org/docs/current/functions-matching.html
    public static class LikePostgreSql
    {
        private static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql };

        public static bool ILike(this string matchExpression, string pattern)
        {
            return LikeString.Like(matchExpression, pattern, LikeOptions);
        }

        public static string? LikeRegex(string pattern)
        {
            return LikeString.LikeRegex(pattern, LikeOptions);
        }
    }
}