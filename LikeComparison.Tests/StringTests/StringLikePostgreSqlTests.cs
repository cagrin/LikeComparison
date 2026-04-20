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

            Assert.IsTrue("Hello".Like("Hello"));
            Assert.IsFalse("Hello".Like("World"));
            Assert.IsTrue("Hello".Like("H_llo"));
            Assert.IsFalse("Hello".Like("He__o_"));
            Assert.IsTrue("Hello".Like("He%"));
            Assert.IsTrue("Hello".Like("%lo"));
            Assert.IsTrue("Hello".Like("%ell%"));
            Assert.IsTrue("Hello".Like("H_%o"));
            Assert.IsFalse("Hello".Like("H_%a"));
            Assert.IsTrue("Hello".Like("H%"));
            Assert.IsTrue("Hello".Like("%"));

            Assert.IsFalse("Hello".Like(string.Empty));
            Assert.IsTrue(string.Empty.Like("%"));
            Assert.IsTrue(string.Empty.Like(string.Empty));

            string input = null!;
            Assert.IsFalse(input.Like("H%"));
            Assert.IsFalse("Hello".Like(null!));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        public void IsNotILikeHello(string? matchExpression)
        {
            _ = Assert.ThrowsExactly<AssertFailedException>(() => Assert.IsTrue(matchExpression.ILike("Hal%")));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        public void IsNotLikeHello(string? matchExpression)
        {
            _ = Assert.ThrowsExactly<AssertFailedException>(() => Assert.IsTrue(matchExpression.Like("Hal%")));
        }

        [TestMethod]
        public void ILikeRegex()
        {
            Assert.IsNotNull(LikePostgreSql.ILikeRegex("h_ll%"));
        }

        [TestMethod]
        public void LikeRegex()
        {
            Assert.IsNotNull(LikePostgreSql.LikeRegex("H_ll%"));
        }
    }
}