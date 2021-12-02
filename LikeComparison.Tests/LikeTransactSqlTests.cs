namespace LikeComparison.Tests
{
    using Dapper;
    using DotNet.Testcontainers.Containers.Builders;
    using DotNet.Testcontainers.Containers.Configurations.Databases;
    using DotNet.Testcontainers.Containers.Modules.Databases;
    using DotNet.Testcontainers.Containers.WaitStrategies;
    using LikeComparison.TransactSql;
    using Microsoft.Data.SqlClient;

    [TestClass]
    public class LikeTransactSqlTests
    {
        private static MsSqlTestcontainer? testcontainer;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // docker run -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=StrongP@ssw0rd!' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
            var testcontainersBuilder = new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithDatabase(new MsSqlTestcontainerConfiguration()
                {
                    Password = "StrongP@ssw0rd!",
                })
#if DEBUG
                .WithImage("mcr.microsoft.com/azure-sql-edge")
                .WithPortBinding(1433)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433));
#else
                .WithImage("mcr.microsoft.com/mssql/server:2019-latest");
#endif

            testcontainer = testcontainersBuilder.Build();
            testcontainer.StartAsync().Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            testcontainer?.DisposeAsync().AsTask().Wait();
        }

        [DataTestMethod]
        [DataRow("aAB", "%", 26620)]
        [DataRow("/\\", "_%", 9610)]
        [DataRow("*.", "_%", 9610)]
        [DataRow("#?", "_%", 9610)]
        [DataRow("ab", "[^]%", 47244)]
        [DataRow("[]", "[^]%", 48174)]
        [DataRow("^!", "[^]%", 48050)]
        [DataRow("ab", "[^]%", 47244)]
        public void LikeTransactSqlComparision(string expressionLetters, string patternLetters, int combinations)
        {
            var cases = LikeTestCase.Generate(expressionLetters, patternLetters);

            Assert.AreEqual(combinations, cases.Count());

            Parallel.ForEachAsync(cases, new ParallelOptions() { MaxDegreeOfParallelism = 50 }, async (c, t) =>
            {
                string matchExpression = c[0].ToString();
                string pattern = c[1].ToString();

                var expected = await LikeTransactSqlOperatorAsync(matchExpression, pattern).ConfigureAwait(false);
                var regex = LikeTransactSql.LikeRegex(pattern) ?? "<Null>";
                var message = $"Query:'{matchExpression}' LIKE '{pattern}'. Regex:{regex}";

                try
                {
                    var actual = matchExpression.Like(pattern);
                    Assert.AreEqual(expected, actual, message);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, message + $". Exception:{ex.Message}.");
                    throw;
                }
            }).Wait();
        }

        private static async Task<bool> LikeTransactSqlOperatorAsync(string matchExpression, string pattern)
        {
            string query = "SELECT CASE WHEN '" + matchExpression + "' LIKE '" + pattern + "' THEN 1 ELSE 0 END";

            using var connection = new SqlConnection(testcontainer?.ConnectionString + "TrustServerCertificate=True;");

            return await connection.ExecuteScalarAsync<bool>(query).ConfigureAwait(false);
        }
    }
}