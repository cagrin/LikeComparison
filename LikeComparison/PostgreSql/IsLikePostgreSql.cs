namespace LikeComparison.PostgreSql
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class IsLikePostgreSql
    {
        private static readonly LikeOptions ILikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, CaseSensitivity = CaseSensitivity.CaseInsensitive };

        private static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, CaseSensitivity = CaseSensitivity.CaseSensitive };

        public static void IsILike(this Assert assert, string matchExpression, string pattern)
        {
            _ = assert;

            if (!LikeString.Like(matchExpression, pattern, ILikeOptions))
            {
                throw new AssertFailedException($"Assert.That.IsILike failed. Expected that <{matchExpression}> is ILIKE <{pattern}>, but actually it is not.");
            }
        }

        public static void IsLike(this Assert assert, string matchExpression, string pattern)
        {
            _ = assert;

            if (!LikeString.Like(matchExpression, pattern, LikeOptions))
            {
                throw new AssertFailedException($"Assert.That.IsLike failed. Expected that <{matchExpression}> is LIKE <{pattern}>, but actually it is not.");
            }
        }
    }
}