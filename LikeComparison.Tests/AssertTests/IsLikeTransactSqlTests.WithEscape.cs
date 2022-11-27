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
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsLikeHelloWithNullEscape()
        {
            Assert.That.IsLike("Hello", "h_ll%", null!);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        public void IsNotLikeHelloWithEscape(string? matchExpression)
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => Assert.That.IsLike(matchExpression, "Hal%", "/"));

            Assert.AreEqual($"Assert.That.IsLike failed. Expected that <{matchExpression ?? "(null)"}> is LIKE <Hal%> ESCAPE </>, but actually it is not.", ex.Message);
        }
    }
}