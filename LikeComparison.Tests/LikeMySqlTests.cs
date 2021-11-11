namespace LikeComparison.Tests
{
    using Dapper;
    using DotNet.Testcontainers.Containers.Builders;
    using DotNet.Testcontainers.Containers.Configurations.Databases;
    using DotNet.Testcontainers.Containers.Modules.Databases;
    using LikeComparison.PostgreSql;
    using MySqlConnector;

    [TestClass]
    public class LikeMySqlTests
    {
        private static MySqlTestcontainer? testcontainer;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // docker run -e MYSQL_ROOT_PASSWORD=StrongP@ssw0rd! -p 3306:3306 -d mysql/mysql-server
            var testcontainersBuilder = new TestcontainersBuilder<MySqlTestcontainer>()
                .WithDatabase(new MySqlTestcontainerConfiguration()
                {
                    Database = "mysql",
                    Username = "mysql",
                    Password = "StrongP@ssw0rd!",
#if DEBUG
                })
                .WithImage("mariadb");
#else
                });
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
        [DataRow("aAB", "_%", 79860)]
        public void LikeMySqlComparision(string expressionLetters, string patternLetters, int combinations)
        {
            var cases = LikeTestCase.Generate(expressionLetters, patternLetters);

            Assert.AreEqual(combinations, cases.Count());

            Parallel.ForEachAsync(cases, new ParallelOptions() { MaxDegreeOfParallelism = 50 }, async (c, t) =>
            {
                string matchExpression = c[0].ToString();
                string pattern = c[1].ToString();

                var expected = await LikeMySqlOperatorAsync(matchExpression, pattern).ConfigureAwait(false);
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
                    throw;
                }
            }).Wait();
        }

        private static async Task<bool> LikeMySqlOperatorAsync(string matchExpression, string pattern)
        {
            string query = "SELECT CASE WHEN '" + matchExpression + "' LIKE '" + pattern + "' THEN 1 ELSE 0 END";

            using var connection = new MySqlConnection(testcontainer?.ConnectionString);

            return await connection.ExecuteScalarAsync<bool>(query).ConfigureAwait(false);
        }
    }
}