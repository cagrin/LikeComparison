namespace LikeComparison.TransactSql
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldNotBeLikeTransactSql
    {
        public static void ShouldNotBeLike(this string? actual, string pattern, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !LikeString.Like(actual, pattern, LikeTransactSql.LikeOptions), actual, pattern, customMessage);
        }
    }
}