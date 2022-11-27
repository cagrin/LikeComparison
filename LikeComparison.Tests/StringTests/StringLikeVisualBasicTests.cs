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
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotLikeHello(string? matchExpression)
        {
            Assert.IsTrue(matchExpression.Like("Hal*"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LikeThrowArgumentNullException()
        {
            Assert.IsTrue("Hello".Like(null!));
        }
    }
}