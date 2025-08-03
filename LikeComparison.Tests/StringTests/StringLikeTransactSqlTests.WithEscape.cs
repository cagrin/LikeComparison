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
        public void IsLikeHelloWithNullEscape()
        {
            _ = Assert.ThrowsExactly<ArgumentNullException>(() => Assert.IsTrue("Hello".Like("h_ll%", null!)));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        public void IsNotLikeHelloWithEscape(string? matchExpression)
        {
            _ = Assert.ThrowsExactly<AssertFailedException>(() => Assert.IsTrue(matchExpression.Like("Hal%", "/")));
        }

        [TestMethod]
        public void LikeRegexEscape()
        {
            Assert.IsNotNull(LikeTransactSql.LikeRegex("h_ll%", "/"));
        }

        [TestMethod]
        public void LikeRegexThrowArgumentNullException()
        {
            _ = Assert.ThrowsExactly<ArgumentNullException>(() => Assert.IsNotNull(LikeTransactSql.LikeRegex("h_ll%", null!)));
        }

        [TestMethod]
        [DataRow("abcdef", "a%", "\\", true)]
        [DataRow("abcdef", "a%", "a", false)]
        [DataRow("abcdef", "EaEbEc%", "E", true)]
        [DataRow("abcdef", "%\\e_", "\\", true)]
        public void LikeTransactSqlWithEscapeSpecials(string matchExpression, string pattern, string escape, bool expected)
        {
            bool actual = matchExpression.Like(pattern, escape);

            Assert.AreEqual(expected, actual);
        }
    }
}