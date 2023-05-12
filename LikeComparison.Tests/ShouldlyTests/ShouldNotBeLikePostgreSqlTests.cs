namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.PostgreSql;

    [TestClass]
    public class ShouldNotBeLikePostgreSqlTests
    {
        [TestMethod]
        public void ShouldNotBeILikeShouldPass()
        {
            "Hello".ShouldNotBeILike("ha_l%");
        }

        [TestMethod]
        public void ShouldNotBeLikeShouldPass()
        {
            "Hello".ShouldNotBeLike("Ha_l%");
        }

        [TestMethod]
        public void ShouldNotBeILikeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldNotBeILike("Hel%"));

            exception.Message.ShouldBe("Should.Throw<ShouldAssertException>(matchExpression\r\n    should not be i like\r\n\"Hel%\"\r\n    but was\r\n\"Hello\"");
        }

        [TestMethod]
        public void ShouldNotBeLikeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldNotBeLike("Hel%"));

            exception.Message.ShouldBe("Should.Throw<ShouldAssertException>(matchExpression\r\n    should not be like\r\n\"Hel%\"\r\n    but was\r\n\"Hello\"");
        }
    }
}