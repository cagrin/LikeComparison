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

            var message =
"""
Should.Throw<ShouldAssertException>(matchExpression
    should be i like
"Hal%"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }

        [TestMethod]
        public void ShouldBeLikeShouldThrowShouldAssertException()
        {
            string? matchExpression = "Hello";
            var exception = Should.Throw<ShouldAssertException>(() => matchExpression.ShouldBeLike("Hal%"));

            var message =
"""
Should.Throw<ShouldAssertException>(matchExpression
    should be like
"Hal%"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}