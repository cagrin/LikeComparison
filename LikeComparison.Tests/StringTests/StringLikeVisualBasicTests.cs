namespace LikeComparison.AssertTests
{
    using LikeComparison.VisualBasic;

    [TestClass]
    public class StringLikeVisualBasicTests
    {
        [TestMethod]
        public void IsLikeHello()
        {
            Assert.IsTrue("Hello".Like("h?ll*"));

            Assert.IsTrue("Hello".Like("Hello"));
            Assert.IsFalse("Hello".Like("World"));
            Assert.IsTrue("Hello".Like("H?llo"));
            Assert.IsFalse("Hello".Like("He??o?"));
            Assert.IsTrue("Hello".Like("He*"));
            Assert.IsTrue("Hello".Like("*lo"));
            Assert.IsTrue("Hello".Like("*ell*"));
            Assert.IsTrue("Hello".Like("H?*o"));
            Assert.IsFalse("Hello".Like("H?*a"));
            Assert.IsTrue("Hello".Like("H*"));
            Assert.IsTrue("Hello".Like("*"));

            Assert.IsFalse("Hello".Like(string.Empty));
            Assert.IsTrue(string.Empty.Like("*"));
            Assert.IsTrue(string.Empty.Like(string.Empty));

            string input = null!;
            Assert.IsFalse(input.Like("H*"));
            Assert.IsFalse("Hello".Like(null!));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        public void IsNotLikeHello(string? matchExpression)
        {
            _ = Assert.ThrowsExactly<AssertFailedException>(() => Assert.IsTrue(matchExpression.Like("Hal*")));
        }
    }
}