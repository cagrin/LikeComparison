using Microsoft.VisualStudio.TestTools.UnitTesting;
using LikeComparison.PostgreSql;
using Dapper;
using Npgsql;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Configurations.Databases;
using DotNet.Testcontainers.Containers.Modules.Databases;

namespace LikeComparison.Tests
{
    [TestClass]
    public class LikePostgreSqlTests
    {
        private static PostgreSqlTestcontainer? _testcontainer;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // docker run -e POSTGRES_PASSWORD=StrongP@ssw0rd! -p 5432:5432 --name postgres -d postgres
            var testcontainersBuilder = new TestcontainersBuilder<PostgreSqlTestcontainer>()
                .WithDatabase(new PostgreSqlTestcontainerConfiguration("postgres")
                {
                    Database = "postgres",
                    Username = "postgres",
                    Password = "StrongP@ssw0rd!"
                });

            _testcontainer = testcontainersBuilder.Build();
            _testcontainer.StartAsync().Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _testcontainer?.DisposeAsync().AsTask().Wait();
        }

        [DataTestMethod]
        [DataRow("aAB", "_%", 79860)]
        public void LikePostgreSqlComparision(string expressionLetters, string patternLetters, int combinations)
        {
            var cases = LikeTestCase.Generate(expressionLetters, patternLetters);

            Assert.AreEqual(combinations, cases.Count());

            Parallel.ForEachAsync(cases, new ParallelOptions() { MaxDegreeOfParallelism = 80 }, async (c, t) =>
            {
                string matchExpression = c[0].ToString();
                string pattern = c[1].ToString();

                var expected = await LikePostgreSqlOperatorAsync(matchExpression, pattern).ConfigureAwait(false);
                var regex = LikePostgreSql.LikeRegex(pattern) ?? "<Null>";
                var message = $"Query:'{matchExpression}' ILIKE '{pattern}'. Regex:{regex}";

                try
                {
                    var actual = matchExpression.ILike(pattern);
                    Assert.AreEqual(expected, actual, message);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, message + $". Exception:{ex.Message}.");
                }

            }).Wait();
        }

        private async Task<bool> LikePostgreSqlOperatorAsync(string matchExpression, string pattern)
        {
            string query = "SELECT CASE WHEN '" + matchExpression + "' ILIKE '" + pattern + "' THEN 1 ELSE 0 END";

            using(var connection = new NpgsqlConnection(_testcontainer?.ConnectionString))
            {
                return await connection.ExecuteScalarAsync<bool>(query).ConfigureAwait(false);
            }
        }
    }
}