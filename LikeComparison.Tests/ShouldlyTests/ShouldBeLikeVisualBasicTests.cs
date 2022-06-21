namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.VisualBasic;

    [TestClass]
    public class ShouldBeLikeVisualBasicTests
    {
        [TestMethod]
        public void IsLikeHello()
        {
            "Hello".ShouldBeLike("h?ll*");
        }

        [TestMethod]
        public void IsNotLikeHello()
        {
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldBeLike("Hal*"));

            var message =
"""
Should.Throw<ShouldAssertException>("Hello"
    should be like
"Hal*"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}