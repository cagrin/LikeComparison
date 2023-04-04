namespace LikeComparison.VisualBasic
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static partial class IsLikeVisualBasic
    {
        public static void IsLike(this Assert assert, string? matchExpression, string pattern)
        {
            _ = assert;

            if (!LikeVisualBasic.Like(matchExpression, pattern))
            {
                throw new AssertFailedException($"Assert.That.IsLike failed. Expected that <{matchExpression ?? "(null)"}> is LIKE <{pattern}>, but actually it is not.");
            }
        }
    }
}