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

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotILikeHello(string? matchExpression)
        {
            Assert.IsTrue(matchExpression.ILike("Hal%"));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotLikeHello(string? matchExpression)
        {
            Assert.IsTrue(matchExpression.Like("Hal%"));
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