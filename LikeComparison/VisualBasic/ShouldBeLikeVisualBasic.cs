namespace LikeComparison.VisualBasic
{
    using Shouldly;

    [ShouldlyMethods]
    public static partial class ShouldBeLikeVisualBasic
    {
        private static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.VisualBasic };

        public static void ShouldBeLike(this string actual, string pattern)
        {
            actual.AssertAwesomely(v => LikeString.Like(actual, pattern, LikeOptions), actual, pattern);
        }
    }
}