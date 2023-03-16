namespace LikeComparison.TransactSql
{
    using Shouldly;

    public static partial class ShouldNotBeLikeTransactSql
    {
        public static void ShouldNotBeLike(this string? actual, string pattern, string escape, string? customMessage = null)
        {
            ArgumentNullException.ThrowIfNull(escape);

            LikeOptions likeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, Escape = escape };

            actual.AssertAwesomely(v => !LikeString.Like(actual, pattern, likeOptions), actual, pattern, customMessage);
        }
    }
}