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
        public void ShouldNotBeLikeWithEscapeShouldThrowArgumentNullException()
        {
            _ = Should.Throw<ArgumentNullException>(() => "Hello".ShouldNotBeLike("ha_l%", escape: null!));
        }

        [TestMethod]
        public void ShouldNotBeLikeWithEscapeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldNotBeLike("Hel%", escape: "/"));

            exception.Message.ShouldBe("Should.Throw<ShouldAssertException>(matchExpression\r\n    should not be like\r\n\"Hel%\"\r\n    but was\r\n\"Hello\"");
        }
    }
}