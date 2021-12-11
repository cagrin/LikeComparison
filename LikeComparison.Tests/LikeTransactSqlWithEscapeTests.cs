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
    public class LikeTransactSqlWithEscapeTests
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
        [DataRow("abcdef", "a%", "\\", true)]
        public async Task LikeTransactSqlWithEscapeSpecials(string matchExpression, string pattern, string escape, bool expected)
        {
            bool actual = await LikeTransactSqlWithEscapeAssert(matchExpression, pattern, escape).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("aAB", "_%", 79860, "\\")]
        public void LikeTransactSqlWithEscapeComparision(string expressionLetters, string patternLetters, int combinations, string escape)
        {
            var cases = LikeTestCase.Generate(expressionLetters, patternLetters);

            Assert.AreEqual(combinations, cases.Count());

            Parallel.ForEachAsync(cases, new ParallelOptions() { MaxDegreeOfParallelism = 50 }, async (c, t) =>
            {
                string matchExpression = c[0].ToString();
                string pattern = c[1].ToString();

                await LikeTransactSqlWithEscapeAssert(matchExpression, pattern, escape).ConfigureAwait(false);
            }).Wait();
        }

        private static async Task<bool> LikeTransactSqlWithEscapeAssert(string matchExpression, string pattern, string escape)
        {
            var expected = await LikeTransactSqlOperatorWithEscapeAsync(matchExpression, pattern, escape).ConfigureAwait(false);
            var regex = LikeTransactSql.LikeRegex(pattern) ?? "<Null>";
            var message = $"Query:'{matchExpression}' LIKE '{pattern}' ESCAPE '{escape}'. Regex:{regex}";

            try
            {
                var actual = matchExpression.Like(pattern);
                Assert.AreEqual(expected, actual, message);
                return actual;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, message + $". Exception:{ex.Message}.");
                throw;
            }
        }

        private static async Task<bool> LikeTransactSqlOperatorWithEscapeAsync(string matchExpression, string pattern, string escape)
        {
            string query = "SELECT CASE WHEN '" + matchExpression + "' LIKE '" + pattern + "' ESCAPE '" + escape + "' THEN 1 ELSE 0 END";

            using var connection = new SqlConnection(testcontainer?.ConnectionString + "TrustServerCertificate=True;");

            return await connection.ExecuteScalarAsync<bool>(query).ConfigureAwait(false);
        }
    }
}