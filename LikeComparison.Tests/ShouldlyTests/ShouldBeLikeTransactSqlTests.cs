namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.TransactSql;

    [TestClass]
    public partial class ShouldBeLikeTransactSqlTests
    {
        [TestMethod]
        public void ShouldBeLikeShouldPass()
        {
            "Hello".ShouldBeLike("h_ll%");
        }

        [TestMethod]
        public void ShouldBeLikeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldBeLike("Hal%"));

            exception.Message.ShouldBe("Should.Throw<ShouldAssertException>(matchExpression\r\n    should be like\r\n\"Hal%\"\r\n    but was\r\n\"Hello\"");
        }
    }
}