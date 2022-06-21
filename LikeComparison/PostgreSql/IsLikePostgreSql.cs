namespace LikeComparison.PostgreSql
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class IsLikePostgreSql
    {
        public static void IsILike(this Assert assert, string matchExpression, string pattern)
        {
            _ = assert;

            if (!LikeString.Like(matchExpression, pattern, LikePostgreSql.ILikeOptions))
            {
                throw new AssertFailedException($"Assert.That.IsILike failed. Expected that <{matchExpression}> is ILIKE <{pattern}>, but actually it is not.");
            }
        }

        public static void IsLike(this Assert assert, string matchExpression, string pattern)
        {
            _ = assert;

            if (!LikeString.Like(matchExpression, pattern, LikePostgreSql.LikeOptions))
            {
                throw new AssertFailedException($"Assert.That.IsLike failed. Expected that <{matchExpression}> is LIKE <{pattern}>, but actually it is not.");
            }
        }
    }
}