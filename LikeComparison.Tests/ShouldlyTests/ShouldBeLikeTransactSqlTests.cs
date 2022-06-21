namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.TransactSql;

    [TestClass]
    public partial class ShouldBeLikeTransactSqlTests
    {
        [TestMethod]
        public void IsLikeHello()
        {
            "Hello".ShouldBeLike("h_ll%");
        }

        [TestMethod]
        public void IsNotLikeHello()
        {
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldBeLike("Hal%"));

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