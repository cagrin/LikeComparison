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

        [TestMethod]
        public void IsNotILikeHello()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => Assert.That.IsILike("Hello", "Hal%"));

            Assert.AreEqual("Assert.That.IsILike failed. Expected that <Hello> is ILIKE <Hal%>, but actually it is not.", ex.Message);
        }

        [TestMethod]
        public void IsNotLikeHello()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => Assert.That.IsLike("Hello", "Hal%"));

            Assert.AreEqual("Assert.That.IsLike failed. Expected that <Hello> is LIKE <Hal%>, but actually it is not.", ex.Message);
        }
    }
}