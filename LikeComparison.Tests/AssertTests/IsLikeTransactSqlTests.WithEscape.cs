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
        public void IsNotLikeHelloWithEscape()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => Assert.That.IsLike("Hello", "Hal%", "/"));

            Assert.AreEqual("Assert.That.IsLike failed. Expected that <Hello> is LIKE <Hal%> ESCAPE </>, but actually it is not.", ex.Message);
        }
    }
}