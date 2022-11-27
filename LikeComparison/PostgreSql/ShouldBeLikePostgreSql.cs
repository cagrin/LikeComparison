namespace LikeComparison.PostgreSql
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldBeLikePostgreSql
    {
        public static void ShouldBeILike(this string? actual, string pattern)
        {
            actual.AssertAwesomely(v => LikeString.Like(actual, pattern, LikePostgreSql.ILikeOptions), actual, pattern);
        }

        public static void ShouldBeLike(this string? actual, string pattern)
        {
            actual.AssertAwesomely(v => LikeString.Like(actual, pattern, LikePostgreSql.LikeOptions), actual, pattern);
        }
    }
}