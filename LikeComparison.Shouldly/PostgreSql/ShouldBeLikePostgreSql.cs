namespace LikeComparison.PostgreSql
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldBeLikePostgreSql
    {
        public static void ShouldBeILike(this string? actual, string pattern)
        {
            actual.AssertAwesomely(v => LikePostgreSql.ILike(actual, pattern), actual, pattern);
        }

        public static void ShouldBeLike(this string? actual, string pattern)
        {
            actual.AssertAwesomely(v => LikePostgreSql.Like(actual, pattern), actual, pattern);
        }
    }
}