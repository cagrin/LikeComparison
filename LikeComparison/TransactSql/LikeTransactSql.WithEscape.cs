namespace LikeComparison.TransactSql
{
    public static partial class LikeTransactSql
    {
        public static bool Like(this string? matchExpression, string pattern, string escape)
        {
            ArgumentNullException.ThrowIfNull(escape);

            LikeOptions likeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, Escape = escape };

            return LikeString.Like(matchExpression, pattern, likeOptions);
        }

        public static string? LikeRegex(string pattern, string escape)
        {
            ArgumentNullException.ThrowIfNull(escape);

            LikeOptions likeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, Escape = escape };

            return LikeString.LikeRegex(pattern, likeOptions);
        }
    }
}