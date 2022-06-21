namespace LikeComparison.VisualBasic
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldNotBeLikeVisualBasic
    {
        private static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.VisualBasic };

        public static void ShouldNotBeLike(this string actual, string pattern)
        {
            actual.AssertAwesomely(v => !LikeString.Like(actual, pattern, LikeOptions), actual, pattern);
        }
    }
}