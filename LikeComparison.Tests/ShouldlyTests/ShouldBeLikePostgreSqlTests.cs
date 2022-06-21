namespace LikeComparison.ShouldlyTests
{
    using LikeComparison.PostgreSql;

    [TestClass]
    public class ShouldBeLikePostgreSqlTests
    {
        [TestMethod]
        public void IsILikeHello()
        {
            "Hello".ShouldBeILike("h_ll%");
        }

        [TestMethod]
        public void IsLikeHello()
        {
            "Hello".ShouldBeLike("H_ll%");
        }

        [TestMethod]
        public void IsNotILikeHello()
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
        public void IsNotLikeHello()
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