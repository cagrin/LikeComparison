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

            var message =
"""
Should.Throw<ShouldAssertException>(matchExpression
    should not be i like
"Hel%"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }

        [TestMethod]
        public void ShouldNotBeLikeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldNotBeLike("Hel%"));

            var message =
"""
Should.Throw<ShouldAssertException>(matchExpression
    should not be like
"Hel%"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}