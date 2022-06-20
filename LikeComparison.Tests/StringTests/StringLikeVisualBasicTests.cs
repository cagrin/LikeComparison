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

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotLikeHello()
        {
            Assert.IsTrue("Hello".Like("Hal*"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LikeThrowArgumentNullException()
        {
            Assert.IsTrue("Hello".Like(null!));
        }
    }
}