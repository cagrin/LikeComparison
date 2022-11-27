namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.VisualBasic;

    [TestClass]
    public class ShouldNotBeLikeVisualBasicTests
    {
        [TestMethod]
        public void ShouldNotBeLikeShouldPass()
        {
            "Hello".ShouldNotBeLike("ha?l*");
        }

        [TestMethod]
        public void ShouldNotBeLikeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldNotBeLike("Hel*"));

            var message =
"""
Should.Throw<ShouldAssertException>(matchExpression
    should not be like
"Hel*"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}