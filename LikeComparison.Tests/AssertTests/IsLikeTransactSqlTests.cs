namespace LikeComparison.AssertTests
{
    using LikeComparison.TransactSql;

    [TestClass]
    public class IsLikeTransactSqlTests
    {
        [TestMethod]
        public void IsLikeHello()
        {
            Assert.That.IsLike("Hello", "h_ll%");
        }

        [TestMethod]
        public void IsNotLikeHello()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => Assert.That.IsLike("Hello", "Hal%"));

            Assert.AreEqual("Assert.That.IsLike failed. Expected that <Hello> is LIKE <Hal%>, but actually it is not.", ex.Message);
        }
    }
}
