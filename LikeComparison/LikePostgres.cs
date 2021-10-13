using System;

namespace LikeComparison.Postgres
{
    // https://www.postgresql.org/docs/current/functions-matching.html
    public static class LikePostgres
    {
        private static LikeOptions _likeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql };

        public static bool ILike(this string matchExpression, string pattern)
        {
            return LikeString.Like(matchExpression, pattern, _likeOptions);
        }

        public static bool Like(this string matchExpression, string pattern)
        {
            throw new NotImplementedException("Postgres.Like is not implemented yet.");
        }

        public static string? LikeRegex(string pattern)
        {
            return LikeString.LikeRegex(pattern, _likeOptions);
        }
    }
}