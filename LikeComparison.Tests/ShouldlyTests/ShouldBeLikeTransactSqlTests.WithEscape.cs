namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.TransactSql;

    public partial class ShouldBeLikeTransactSqlTests
    {
        [TestMethod]
        public void IsLikeHelloWithEscape()
        {
            "Hello".ShouldBeLike("h_ll%", escape: "/");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsLikeHelloWithNullEscape()
        {
            "Hello".ShouldBeLike("h_ll%", escape: null!);
        }

        [TestMethod]
        public void IsNotLikeHelloWithEscape()
        {
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldBeLike("Hal%", escape: "/"));

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