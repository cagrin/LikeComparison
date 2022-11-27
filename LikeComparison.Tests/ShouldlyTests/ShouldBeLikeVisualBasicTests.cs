namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.VisualBasic;

    [TestClass]
    public class ShouldBeLikeVisualBasicTests
    {
        [TestMethod]
        public void ShouldBeLikeShouldPass()
        {
            "Hello".ShouldBeLike("h?ll*");
        }

        [TestMethod]
        public void ShouldBeLikeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldBeLike("Hal*"));

            var message =
"""
Should.Throw<ShouldAssertException>(matchExpression
    should be like
"Hal*"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}