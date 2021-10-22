using Microsoft.VisualStudio.TestTools.UnitTesting;
using LikeComparison.PostgreSql;
using Dapper;
using Npgsql;

namespace LikeComparison.Tests
{
    [TestClass]
    public class LikePostgreSqlTests
    {
        [ClassInitialize]
        public static void InitLikePostgreSqlTests(TestContext context)
        {
            DockerPostgreSql.InitContainer(context);
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

            using(var connection = new NpgsqlConnection(DockerPostgreSql.ConnectionString))
            {
                return await connection.ExecuteScalarAsync<bool>(query).ConfigureAwait(false);
            }
        }
    }
}