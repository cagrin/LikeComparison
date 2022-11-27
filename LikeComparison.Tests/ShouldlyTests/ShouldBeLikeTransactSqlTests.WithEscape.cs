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
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldBeLikeWithEscapeShouldThrowArgumentNullException()
        {
            "Hello".ShouldBeLike("h_ll%", escape: null!);
        }

        [TestMethod]
        public void ShouldBeLikeWithEscapeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldBeLike("Hal%", escape: "/"));

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