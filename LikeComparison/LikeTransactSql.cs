namespace LikeComparison.TransactSql
{
    // https://docs.microsoft.com/en-us/sql/t-sql/language-elements/like-transact-sql
    public static class LikeTransactSql
    {
        private static LikeOptions _likeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql };

        public static bool Like(this string matchExpression, string pattern)
        {
            return LikeString.Like(matchExpression, pattern, _likeOptions);
        }

        public static string? LikeRegex(string pattern)
        {
            return LikeString.LikeRegex(pattern, _likeOptions);
        }
    }
}