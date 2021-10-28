namespace LikeComparison.TransactSql
{
    // https://docs.microsoft.com/en-us/sql/t-sql/language-elements/like-transact-sql
    public static class LikeTransactSql
    {
        private static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql };

        public static bool Like(this string matchExpression, string pattern)
        {
            return LikeString.Like(matchExpression, pattern, LikeOptions);
        }

        public static string? LikeRegex(string pattern)
        {
            return LikeString.LikeRegex(pattern, LikeOptions);
        }
    }
}