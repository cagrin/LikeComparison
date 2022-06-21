namespace LikeComparison.PostgreSql
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldNotBeLikePostgreSql
    {
        public static void ShouldNotBeILike(this string actual, string pattern)
        {
            actual.AssertAwesomely(v => !LikeString.Like(actual, pattern, LikePostgreSql.ILikeOptions), actual, pattern);
        }

        public static void ShouldNotBeLike(this string actual, string pattern)
        {
            actual.AssertAwesomely(v => !LikeString.Like(actual, pattern, LikePostgreSql.LikeOptions), actual, pattern);
        }
    }
}