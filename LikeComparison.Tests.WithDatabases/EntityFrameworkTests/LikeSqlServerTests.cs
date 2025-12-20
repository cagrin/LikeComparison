namespace LikeComparison.EntityFrameworkTests
{
    using LikeComparison.TestCases;
    using Microsoft.EntityFrameworkCore;
    using Testcontainers.MsSql;

    [TestClass]
    public partial class LikeSqlServerTests
    {
        private static MsSqlContainer testcontainer = null!;

        private static LikeSqlServerTestContext testcontext = null!;

        public static IEnumerable<object[]> WildcardCharactersAsLiterals => LikeTestCases.WildcardCharactersAsLiterals;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _ = context;

            testcontainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .Build();

            testcontainer.StartAsync().Wait();

            testcontext = new LikeSqlServerTestContext(testcontainer.GetConnectionString());

            _ = testcontext.Database.EnsureCreated();
            _ = testcontext.LikeTestResults.Add(new LikeTestResult());
            _ = testcontext.SaveChanges();
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            await testcontext.DisposeAsync().ConfigureAwait(false);
            await testcontainer.DisposeAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [DynamicData(nameof(WildcardCharactersAsLiterals))]
        public async Task WildcardCharactersAsLiteralsTests(string pattern, string matchExpression, bool expected)
        {
            var result = await testcontext.LikeTestResults.Select(x => new { Comparison = EF.Functions.Like(matchExpression, pattern) }).SingleAsync().ConfigureAwait(false);

            var actual = result.Comparison;

            Assert.AreEqual(expected, actual);
        }
    }
}