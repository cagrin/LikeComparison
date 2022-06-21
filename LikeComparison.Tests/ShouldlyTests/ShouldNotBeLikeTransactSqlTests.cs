namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.TransactSql;

    [TestClass]
    public partial class ShouldNotBeLikeTransactSqlTests
    {
        [TestMethod]
        public void ShouldNotBeLikeShouldPass()
        {
            "Hello".ShouldNotBeLike("ha_l%");
        }

        [TestMethod]
        public void ShouldNotBeLikeShouldThrowShouldAssertException()
        {
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldNotBeLike("Hel%"));

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