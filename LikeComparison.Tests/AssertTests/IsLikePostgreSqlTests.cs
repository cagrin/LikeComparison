namespace LikeComparison.AssertTests
{
    using LikeComparison.PostgreSql;

    [TestClass]
    public class IsLikePostgreSqlTests
    {
        [TestMethod]
        public void IsILikeHello()
        {
            Assert.That.IsILike("Hello", "h_ll%");
        }

        [TestMethod]
        public void IsLikeHello()
        {
            Assert.That.IsLike("Hello", "H_ll%");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        public void IsNotILikeHello(string? matchExpression)
        {
            var ex = Assert.ThrowsExactly<AssertFailedException>(() => Assert.That.IsILike(matchExpression, "Hal%"));

            Assert.AreEqual($"Assert.That.IsILike failed. Expected that <{matchExpression ?? "(null)"}> is ILIKE <Hal%>, but actually it is not.", ex.Message);
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