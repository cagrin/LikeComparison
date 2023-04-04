namespace LikeComparison.TransactSql
{
    using Shouldly;

    public static partial class ShouldNotBeLikeTransactSql
    {
        public static void ShouldNotBeLike(this string? actual, string pattern, string escape, string? customMessage = null)
        {
            ArgumentNullException.ThrowIfNull(escape);

            actual.AssertAwesomely(v => !LikeTransactSql.Like(actual, pattern, escape), actual, pattern, customMessage);
        }
    }
}