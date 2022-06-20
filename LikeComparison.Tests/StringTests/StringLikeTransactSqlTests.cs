namespace LikeComparison.AssertTests
{
    using LikeComparison.TransactSql;

    [TestClass]
    public partial class StringLikeTransactSqlTests
    {
        [TestMethod]
        public void IsLikeHello()
        {
            Assert.IsTrue("Hello".Like("h_ll%"));
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
            Assert.IsNotNull(LikeTransactSql.LikeRegex("h_ll%"));
        }
    }
}