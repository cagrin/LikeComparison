namespace LikeComparison.VisualBasic
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldNotBeLikeVisualBasic
    {
        public static void ShouldNotBeLike(this string? actual, string pattern)
        {
            actual.AssertAwesomely(v => !LikeVisualBasic.Like(actual, pattern), actual, pattern);
        }
    }
}