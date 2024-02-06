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
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("Hello")]
        public void IsNotLikeHello(string? matchExpression)
        {
            _ = Assert.ThrowsException<AssertFailedException>(() => Assert.IsTrue(matchExpression.Like("Hal*")));
        }

        [TestMethod]
        public void LikeThrowArgumentNullException()
        {
            _ = Assert.ThrowsException<ArgumentNullException>(() => Assert.IsTrue("Hello".Like(null!)));
        }
    }
}