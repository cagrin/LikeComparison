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
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldNotBeLike("Hel*"));

            var message =
"""
Should.Throw<ShouldAssertException>("Hello"
    should not be like
"Hel*"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}