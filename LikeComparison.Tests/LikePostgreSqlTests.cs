namespace LikeComparison.Tests
{
    using Dapper;
    using DotNet.Testcontainers.Containers.Builders;
    using DotNet.Testcontainers.Containers.Configurations.Databases;
    using DotNet.Testcontainers.Containers.Modules.Databases;
    using LikeComparison.PostgreSql;
    using Npgsql;

    [TestClass]
    public class LikePostgreSqlTests
    {
        private static PostgreSqlTestcontainer? testcontainer;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // docker run -e POSTGRES_PASSWORD=StrongP@ssw0rd! -p 5432:5432 -d postgres
            var testcontainersBuilder = new TestcontainersBuilder<PostgreSqlTestcontainer>()
                .WithDatabase(new PostgreSqlTestcontainerConfiguration()
                {
                    Database = "postgres",
                    Username = "postgres",
                    Password = "StrongP@ssw0rd!",
                })
                .WithImage("postgres");

            testcontainer = testcontainersBuilder.Build();
            testcontainer.StartAsync().Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            testcontainer?.DisposeAsync().AsTask().Wait();
        }

        [DataTestMethod]
        [DataRow("aAB", "_%", 79860, "ILIKE")]
        [DataRow("aAB", "_%", 79860, "LIKE")]
        public void LikePostgreSqlComparision(string expressionLetters, string patternLetters, int combinations, string likeOperator)
        {
            var cases = LikeTestCase.Generate(expressionLetters, patternLetters);

            Assert.AreEqual(combinations, cases.Count());

            Parallel.ForEachAsync(cases, new ParallelOptions() { MaxDegreeOfParallelism = 50 }, async (c, t) =>
            {
                string matchExpression = c[0].ToString();
                string pattern = c[1].ToString();

                await LikePostgreSqlAssert(matchExpression, pattern, likeOperator).ConfigureAwait(false);
            }).Wait();
        }

        private static async Task LikePostgreSqlAssert(string matchExpression, string pattern, string likeOperator)
        {
            var expected = await LikePostgreSqlOperatorAsync(matchExpression, pattern, likeOperator).ConfigureAwait(false);
            var regex = (likeOperator == "ILIKE" ? LikePostgreSql.ILikeRegex(pattern) : LikePostgreSql.LikeRegex(pattern)) ?? "<Null>";
            var message = $"Query:'{matchExpression}' {likeOperator} '{pattern}'. Regex:{regex}";

            try
            {
                var actual = likeOperator == "ILIKE" ? matchExpression.ILike(pattern) : matchExpression.Like(pattern);
                Assert.AreEqual(expected, actual, message);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, $"{message}. Exception:{ex.Message}.");
                throw;
            }
        }

        private static async Task<bool> LikePostgreSqlOperatorAsync(string matchExpression, string pattern, string likeOperator)
        {
            string query = $"SELECT CASE WHEN '{matchExpression}' {likeOperator} '{pattern}' THEN 1 ELSE 0 END";

            using var connection = new NpgsqlConnection(testcontainer?.ConnectionString);

            return await connection.ExecuteScalarAsync<bool>(query).ConfigureAwait(false);
        }
    }
}