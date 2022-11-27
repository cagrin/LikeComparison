namespace LikeComparison.TransactSql
{
    using System;
    using Shouldly;

    public static partial class ShouldBeLikeTransactSql
    {
        public static void ShouldBeLike(this string? actual, string pattern, string escape, string? customMessage = null)
        {
            if (escape == null)
            {
                throw new ArgumentNullException(nameof(escape), "Value cannot be null.");
            }

            LikeOptions likeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, Escape = escape };

            actual.AssertAwesomely(v => LikeString.Like(actual, pattern, likeOptions), actual, pattern, customMessage);
        }
    }
}