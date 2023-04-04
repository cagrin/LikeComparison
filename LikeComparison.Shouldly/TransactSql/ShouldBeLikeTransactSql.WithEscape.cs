namespace LikeComparison.TransactSql
{
    using Shouldly;

    public static partial class ShouldBeLikeTransactSql
    {
        public static void ShouldBeLike(this string? actual, string pattern, string escape, string? customMessage = null)
        {
            ArgumentNullException.ThrowIfNull(escape);

            actual.AssertAwesomely(v => LikeTransactSql.Like(actual, pattern, escape), actual, pattern, customMessage);
        }
    }
}