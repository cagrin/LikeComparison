namespace LikeComparison.PostgreSql
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldNotBeLikePostgreSql
    {
        public static void ShouldNotBeILike(this string? actual, string pattern)
        {
            actual.AssertAwesomely(v => !LikePostgreSql.ILike(actual, pattern), actual, pattern);
        }

        public static void ShouldNotBeLike(this string? actual, string pattern)
        {
            actual.AssertAwesomely(v => !LikePostgreSql.Like(actual, pattern), actual, pattern);
        }
    }
}