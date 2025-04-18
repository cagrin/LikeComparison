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

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        public void IsNotLikeHello(string? matchExpression)
        {
            _ = Assert.ThrowsExactly<AssertFailedException>(() => Assert.IsTrue(matchExpression.Like("Hal%")));
        }

        [TestMethod]
        public void LikeRegex()
        {
            Assert.IsNotNull(LikeTransactSql.LikeRegex("h_ll%"));
        }

        [TestMethod]
        public void LikeRegexNull()
        {
            Assert.IsFalse("Hello".Like("h_ll[["));
        }
    }
}