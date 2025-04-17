namespace LikeComparison.AssertTests
{
    using LikeComparison.TransactSql;

    [TestClass]
    public partial class IsLikeTransactSqlTests
    {
        [TestMethod]
        public void IsLikeHello()
        {
            Assert.That.IsLike("Hello", "h_ll%");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        public void IsNotLikeHello(string? matchExpression)
        {
            var ex = Assert.ThrowsExactly<AssertFailedException>(() => Assert.That.IsLike(matchExpression, "Hal%"));

            Assert.AreEqual($"Assert.That.IsLike failed. Expected that <{matchExpression ?? "(null)"}> is LIKE <Hal%>, but actually it is not.", ex.Message);
        }
    }
}