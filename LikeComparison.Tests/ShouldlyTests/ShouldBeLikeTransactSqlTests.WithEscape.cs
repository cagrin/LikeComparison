namespace LikeComparison.AssertTests
{
    using LikeComparison.TransactSql;

    public partial class ShouldBeLikeTransactSqlTests
    {
        [TestMethod]
        public void IsLikeHelloWithEscape()
        {
            "Hello".ShouldBeLike("h_ll%", "/");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsLikeHelloWithNullEscape()
        {
            "Hello".ShouldBeLike("h_ll%", null!);
        }

        [TestMethod]
        public void IsNotLikeHelloWithEscape()
        {
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldBeLike("Hal%", "/"));

            var message =
"""
Should.Throw<ShouldAssertException>("Hello"
    should be like
"Hal%"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}