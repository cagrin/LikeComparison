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
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldNotBeILike("Hel%"));

            var message =
"""
Should.Throw<ShouldAssertException>("Hello"
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
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldNotBeLike("Hel%"));

            var message =
"""
Should.Throw<ShouldAssertException>("Hello"
    should not be like
"Hel%"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}