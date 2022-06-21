namespace LikeComparison.PostgreSql
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldBeLikePostgreSql
    {
        private static readonly LikeOptions ILikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, CaseSensitivity = CaseSensitivity.CaseInsensitive };

        private static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.TransactSql, CaseSensitivity = CaseSensitivity.CaseSensitive };

        public static void ShouldBeILike(this string actual, string pattern)
        {
            actual.AssertAwesomely(v => LikeString.Like(actual, pattern, ILikeOptions), actual, pattern);
        }

        public static void ShouldBeLike(this string actual, string pattern)
        {
            actual.AssertAwesomely(v => LikeString.Like(actual, pattern, LikeOptions), actual, pattern);
        }
    }
}