namespace LikeComparison.PostgreSql
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class IsLikePostgreSql
    {
        public static void IsILike(this Assert assert, string? matchExpression, string pattern)
        {
            _ = assert;

            if (!LikePostgreSql.ILike(matchExpression, pattern))
            {
                throw new AssertFailedException($"Assert.That.IsILike failed. Expected that <{matchExpression ?? "(null)"}> is ILIKE <{pattern}>, but actually it is not.");
            }
        }

        public static void IsLike(this Assert assert, string? matchExpression, string pattern)
        {
            _ = assert;

            if (!LikePostgreSql.Like(matchExpression, pattern))
            {
                throw new AssertFailedException($"Assert.That.IsLike failed. Expected that <{matchExpression ?? "(null)"}> is LIKE <{pattern}>, but actually it is not.");
            }
        }
    }
}