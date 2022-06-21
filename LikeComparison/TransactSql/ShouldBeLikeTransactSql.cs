namespace LikeComparison.TransactSql
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldBeLikeTransactSql
    {
        private static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql };

        public static void ShouldBeLike(this string actual, string pattern)
        {
            actual.AssertAwesomely(v => LikeString.Like(actual, pattern, LikeOptions), actual, pattern);
        }
    }
}