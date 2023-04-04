namespace LikeComparison.TransactSql
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static partial class IsLikeTransactSql
    {
        public static void IsLike(this Assert assert, string? matchExpression, string pattern, string escape)
        {
            _ = assert;

            ArgumentNullException.ThrowIfNull(escape);

            if (!LikeTransactSql.Like(matchExpression, pattern, escape))
            {
                throw new AssertFailedException($"Assert.That.IsLike failed. Expected that <{matchExpression ?? "(null)"}> is LIKE <{pattern}> ESCAPE <{escape}>, but actually it is not.");
            }
        }
    }
}