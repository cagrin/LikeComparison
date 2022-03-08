namespace LikeComparison.VisualBasic
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static partial class IsLikeVisualBasic
    {
        private static readonly LikeOptions LikeOptions = new LikeOptions() { PatternStyle = PatternStyle.VisualBasic };

        public static void IsLike(this Assert assert, string matchExpression, string pattern)
        {
            _ = assert;

            if (!LikeString.Like(matchExpression, pattern, LikeOptions))
            {
                throw new AssertFailedException($"Assert.That.IsLike failed. Expected that <{matchExpression}> is LIKE <{pattern}>, but actually it is not.");
            }
        }
    }
}