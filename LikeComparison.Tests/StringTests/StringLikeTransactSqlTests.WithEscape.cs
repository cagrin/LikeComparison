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

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotLikeHelloWithEscape(string? matchExpression)
        {
            Assert.IsTrue(matchExpression.Like("Hal%", "/"));
        }

        [TestMethod]
        public void LikeRegexEscape()
        {
            Assert.IsNotNull(LikeTransactSql.LikeRegex("h_ll%", "/"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LikeRegexThrowArgumentNullException()
        {
            Assert.IsNotNull(LikeTransactSql.LikeRegex("h_ll%", null!));
        }

        [DataTestMethod]
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