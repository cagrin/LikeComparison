namespace LikeComparison.TransactSql
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldBeLikeTransactSql
    {
        public static void ShouldBeLike(this string? actual, string pattern, string? customMessage = null)
        {
            actual.AssertAwesomely(v => LikeString.Like(actual, pattern, LikeTransactSql.LikeOptions), actual, pattern, customMessage);
        }
    }
}