namespace LikeComparison.TransactSql
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static partial class IsLikeTransactSql
    {
        public static void IsLike(this Assert assert, string matchExpression, string pattern, string escape)
        {
            _ = assert;

            if (escape == null)
            {
                throw new ArgumentNullException(nameof(escape), "Value cannot be null.");
            }

            LikeOptions likeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, Escape = escape };

            if (!LikeString.Like(matchExpression, pattern, likeOptions))
            {
                throw new AssertFailedException($"Assert.That.IsLike failed. Expected that <{matchExpression}> is LIKE <{pattern}> ESCAPE <{escape}>, but actually it is not.");
            }
        }
    }
}