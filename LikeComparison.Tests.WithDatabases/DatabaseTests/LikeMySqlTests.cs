namespace LikeComparison.DatabaseTests
{
    using Dapper;
    using LikeComparison.PostgreSql;
    using MySqlConnector;
    using Testcontainers.MySql;

    [TestClass]
    public class LikeMySqlTests
    {
        private static MySqlContainer? testcontainer;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // docker run -e MYSQL_ROOT_PASSWORD=StrongP@ssw0rd! -p 3306:3306 -d mysql/mysql-server
            _ = context;

            testcontainer = new MySqlBuilder()
                .WithImage("mysql")
                .Build();

            testcontainer.StartAsync().Wait();
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            if (testcontainer != null)
            {
                await testcontainer.DisposeAsync().ConfigureAwait(false);
            }
        }

        [TestMethod]
        [DataRow("aAB", "_%", 79860)]
        public void LikeMySqlComparision(string expressionLetters, string patternLetters, int combinations)
        {
            var cases = LikeTestCase.Generate(expressionLetters, patternLetters);

            Assert.AreEqual(combinations, cases.Count());

            Parallel.ForEachAsync(cases, new ParallelOptions() { MaxDegreeOfParallelism = 50 }, async (c, t) =>
            {
                string matchExpression = c[0].ToString();
                string pattern = c[1].ToString();

                await LikeMySqlAssert(matchExpression, pattern).ConfigureAwait(false);
            }).Wait();
        }

        private static async Task LikeMySqlAssert(string matchExpression, string pattern)
        {
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
                Assert.Fail($"{message}. Exception:{ex.Message}.");
                throw;
            }
        }

        private static async Task<bool> LikeMySqlOperatorAsync(string matchExpression, string pattern)
        {
            string query = $"SELECT CASE WHEN '{matchExpression}' LIKE '{pattern}' THEN 1 ELSE 0 END";

            using var connection = new MySqlConnection(testcontainer?.GetConnectionString());

            return await connection.ExecuteScalarAsync<bool>(query).ConfigureAwait(false);
        }
    }
}