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
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldBeILike("Hal%"));

            var message =
"""
Should.Throw<ShouldAssertException>("Hello"
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
            var exception = Should.Throw<ShouldAssertException>(() => "Hello".ShouldBeLike("Hal%"));

            var message =
"""
Should.Throw<ShouldAssertException>("Hello"
    should be like
"Hal%"
    but was
"Hello"
""";

            exception.Message.ShouldBe(message);
        }
    }
}