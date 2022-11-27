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

            var message =
"""
Should.Throw<ShouldAssertException>(matchExpression
    should be like
"Hal%"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}