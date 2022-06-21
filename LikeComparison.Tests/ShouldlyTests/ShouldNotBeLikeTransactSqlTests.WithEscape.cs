namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.TransactSql;

    public partial class ShouldNotBeLikeTransactSqlTests
    {
        [TestMethod]
        public void ShouldNotBeLikeWithEscapeShouldPass()
        {
            "Hello".ShouldNotBeLike("ha_l%", escape: "/");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldNotBeLikeWithEscapeShouldThrowArgumentNullException()
        {
            "Hello".ShouldNotBeLike("ha_l%", escape: null!);
        }

        [TestMethod]
        public void ShouldNotBeLikeWithEscapeShouldThrowShouldAssertException()
        {
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldNotBeLike("Hel%", escape: "/"));

            var message =
"""
Should.Throw<ShouldAssertException>("Hello"
    should not be like
"Hel%"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}