namespace LikeComparison.VisualBasic
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldBeLikeVisualBasic
    {
        public static void ShouldBeLike(this string? actual, string pattern)
        {
            actual.AssertAwesomely(v => LikeVisualBasic.Like(actual, pattern), actual, pattern);
        }
    }
}