namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.PostgreSql;

    [TestClass]
    public class ShouldBeLikePostgreSqlTests
    {
        [TestMethod]
        public void ShouldBeILikeShouldPass()
        {
            "Hello".ShouldBeILike("h_ll%");
        }

        [TestMethod]
        public void ShouldBeLikeShouldPass()
        {
            "Hello".ShouldBeLike("H_ll%");
        }

        [TestMethod]
        public void ShouldBeILikeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldBeILike("Hal%"));

            exception.Message.ShouldBe("Should.Throw<ShouldAssertException>(matchExpression\r\n    should be i like\r\n\"Hal%\"\r\n    but was\r\n\"Hello\"");
        }

        [TestMethod]
        public void ShouldBeLikeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldBeLike("Hal%"));

            exception.Message.ShouldBe("Should.Throw<ShouldAssertException>(matchExpression\r\n    should be like\r\n\"Hal%\"\r\n    but was\r\n\"Hello\"");
        }
    }
}