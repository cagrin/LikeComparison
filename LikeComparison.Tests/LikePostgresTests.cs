using Microsoft.VisualStudio.TestTools.UnitTesting;
using LikeComparison.Postgres;
using Dapper;
using Npgsql;

namespace LikeComparison.Tests
{
    [TestClass]
    public class LikePostgresTests
    {
        [DataTestMethod]
        [DataRow("aAB", "_%", 79860)]
        public void LikePostgresComparision(string expressionLetters, string patternLetters, int combinations)
        {
            var cases = LikeTestCase.Generate(expressionLetters, patternLetters);

            Assert.AreEqual(combinations, cases.Count());

            Parallel.ForEachAsync(cases, new ParallelOptions() { MaxDegreeOfParallelism = 80 }, async (c, t) =>
            {
                string matchExpression = c[0].ToString();
                string pattern = c[1].ToString();

                var expected = await LikePostgresOperatorAsync(matchExpression, pattern).ConfigureAwait(false);
                var regex = LikePostgres.LikeRegex(pattern) ?? "<Null>";
                var message = $"Query:'{matchExpression}' ILIKE '{pattern}'. Regex:{regex}";

                try
                {
                    var actual = matchExpression.Like(pattern);
                    Assert.AreEqual(expected, actual, message);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, message + $". Exception:{ex.Message}.");
                }

            }).Wait();
        }

        private async Task<bool> LikePostgresOperatorAsync(string matchExpression, string pattern)
        {
            // docker run -e POSTGRES_PASSWORD=StrongP@ssw0rd! -p 5432:5432 --name postgres -d postgres
            string connectionString = "User ID=postgres;Password=StrongP@ssw0rd!;Host=localhost;Port=5432;";
            string query = "SELECT CASE WHEN '" + matchExpression + "' ILIKE '" + pattern + "' THEN 1 ELSE 0 END";

            using(var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<bool>(query).ConfigureAwait(false);
            }
        }
    }
}