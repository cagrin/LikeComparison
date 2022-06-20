namespace LikeComparison.AssertTests
{
    using LikeComparison.TransactSql;

    public partial class StringLikeTransactSqlTests
    {
        [TestMethod]
        public void IsLikeHelloWithEscape()
        {
            Assert.IsTrue("Hello".Like("h_ll%", "/"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsLikeHelloWithNullEscape()
        {
            Assert.IsTrue("Hello".Like("h_ll%", null!));
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotLikeHelloWithEscape()
        {
            Assert.IsTrue("Hello".Like("Hal%", "/"));
        }

        [TestMethod]
        public void TestRegexEscape()
        {
            Assert.IsNotNull(LikeTransactSql.LikeRegex("h_ll%", "/"));
        }
    }
}