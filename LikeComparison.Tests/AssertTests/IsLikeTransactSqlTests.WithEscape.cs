namespace LikeComparison.AssertTests
{
    using LikeComparison.TransactSql;

    public partial class IsLikeTransactSqlTests
    {
        [TestMethod]
        public void IsLikeHelloWithEscape()
        {
            Assert.That.IsLike("Hello", "h_ll%", "/");
        }

        [TestMethod]
        public void IsLikeHelloWithNullEscape()
        {
            _ = Assert.ThrowsExactly<ArgumentNullException>(() => Assert.That.IsLike("Hello", "h_ll%", null!));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        public void IsNotLikeHelloWithEscape(string? matchExpression)
        {
            var ex = Assert.ThrowsExactly<AssertFailedException>(() => Assert.That.IsLike(matchExpression, "Hal%", "/"));

            Assert.AreEqual($"Assert.That.IsLike failed. Expected that <{matchExpression ?? "(null)"}> is LIKE <Hal%> ESCAPE </>, but actually it is not.", ex.Message);
        }
    }
}