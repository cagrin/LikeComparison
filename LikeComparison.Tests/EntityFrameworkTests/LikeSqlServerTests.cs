namespace LikeComparison.EntityFrameworkTests
{
    using Microsoft.EntityFrameworkCore;
    using Testcontainers.MsSql;

    [TestClass]
    public partial class LikeSqlServerTests
    {
        private static MsSqlContainer? testcontainer;

        private static LikeTestContext? testcontext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _ = context;

            testcontainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server")
                .Build();

            testcontainer.StartAsync().Wait();

            testcontext = new LikeTestContext(testcontainer.GetConnectionString()!);

            _ = testcontext.Database.EnsureCreated();
            _ = testcontext.LikeTestResults.Add(new LikeTestResult() { TestCase = DateTime.Now });
            _ = testcontext.SaveChanges();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            testcontext?.Dispose();
            testcontainer?.DisposeAsync().AsTask().Wait();
        }

        [DataTestMethod] // https://docs.microsoft.com/en-us/sql/t-sql/language-elements/like-transact-sql?view=sql-server-ver15
        [DataRow("5[%]", "5%", true)]
        [DataRow("5[%]", "5", false)]
        [DataRow("[_]n", "_n", true)]
        [DataRow("[_]n", "n", false)]
        [DataRow("[a-cdf]", "-", false)]
        [DataRow("[a-cdf]", "a", true)]
        [DataRow("[a-cdf]", "b", true)]
        [DataRow("[a-cdf]", "c", true)]
        [DataRow("[a-cdf]", "d", true)]
        [DataRow("[a-cdf]", "e", false)]
        [DataRow("[a-cdf]", "f", true)]
        [DataRow("[-acdf]", "-", true)]
        [DataRow("[-acdf]", "a", true)]
        [DataRow("[-acdf]", "b", false)]
        [DataRow("[-acdf]", "c", true)]
        [DataRow("[-acdf]", "d", true)]
        [DataRow("[-acdf]", "e", false)]
        [DataRow("[-acdf]", "f", true)]
        [DataRow("[[]", "[", true)]
        [DataRow("]", "]", true)]
        [DataRow("abc[_]d%", "abc_d", true)]
        [DataRow("abc[_]d%", "abc_de", true)]
        [DataRow("abc[def]", "abcd", true)]
        [DataRow("abc[def]", "abce", true)]
        [DataRow("abc[def]", "abcf", true)]
        public void WildcardCharactersAsLiteralsTests(string pattern, string matchExpression, bool expected)
        {
            var result = testcontext?.LikeTestResults.Select(x => new { Comparison = EF.Functions.Like(matchExpression, pattern) });

            var actual = result?.Single().Comparison;

            Assert.AreEqual(expected, actual);
        }
    }
}