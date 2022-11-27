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
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldNotBeLike("Hel%", escape: "/"));

            var message =
"""
Should.Throw<ShouldAssertException>(matchExpression
    should not be like
"Hel%"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}