namespace LikeComparison.AssertTests
{
    using LikeComparison.PostgreSql;

    [TestClass]
    public class StringLikePostgreSqlTests
    {
        [TestMethod]
        public void IsILikeHello()
        {
            Assert.IsTrue("Hello".ILike("h_ll%"));
        }

        [TestMethod]
        public void IsLikeHello()
        {
            Assert.IsTrue("Hello".Like("H_ll%"));
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotILikeHello()
        {
            Assert.IsTrue("Hello".ILike("Hal%"));
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotLikeHello()
        {
            Assert.IsTrue("Hello".Like("Hal%"));
        }

        [TestMethod]
        public void TestRegex()
        {
            Assert.IsNotNull(LikePostgreSql.ILikeRegex("h_ll%"));
            Assert.IsNotNull(LikePostgreSql.LikeRegex("H_ll%"));
        }
    }
}