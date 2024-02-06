namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.TransactSql;

    public partial class ShouldBeLikeTransactSqlTests
    {
        [TestMethod]
        public void ShouldBeLikeWithEscapeShouldPass()
        {
            "Hello".ShouldBeLike("h_ll%", escape: "/");
        }

        [TestMethod]
        public void ShouldBeLikeWithEscapeShouldThrowArgumentNullException()
        {
            _ = Should.Throw<ArgumentNullException>(() => "Hello".ShouldBeLike("h_ll%", escape: null!));
        }

        [TestMethod]
        public void ShouldBeLikeWithEscapeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldBeLike("Hal%", escape: "/"));

            exception.Message.ShouldBe("Should.Throw<ShouldAssertException>(matchExpression\r\n    should be like\r\n\"Hal%\"\r\n    but was\r\n\"Hello\"");
        }
    }
}