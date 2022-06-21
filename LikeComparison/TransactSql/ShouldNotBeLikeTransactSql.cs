namespace LikeComparison.TransactSql
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldNotBeLikeTransactSql
    {
        private static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql };

        public static void ShouldNotBeLike(this string actual, string pattern, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !LikeString.Like(actual, pattern, LikeOptions), actual, pattern, customMessage);
        }
    }
}