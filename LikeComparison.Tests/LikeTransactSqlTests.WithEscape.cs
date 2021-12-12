namespace LikeComparison.Tests
{
    using Dapper;
    using LikeComparison.TransactSql;
    using Microsoft.Data.SqlClient;

    public partial class LikeTransactSqlTests
    {
        [DataTestMethod]
        [DataRow("a%", null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LikeTransactSqlRegexWithEscapeThrowArgumentNullException(string pattern, string escape)
        {
            LikeTransactSql.LikeRegex(pattern, escape);
        }

        [DataTestMethod]
        [DataRow("abcdef", "a%", null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LikeTransactSqlWithEscapeThrowArgumentNullException(string matchExpression, string pattern, string escape)
        {
            matchExpression.Like(pattern, escape);
        }

        [DataTestMethod]
        [DataRow("abcdef", "a%", "\\", true)]
        [DataRow("abcdef", "a%", "a", false)]
        [DataRow("abcdef", "EaEbEc%", "E", true)]
        [DataRow("abcdef", "%\\e_", "\\", true)]
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
            var regex = LikeTransactSql.LikeRegex(pattern, escape) ?? "<Null>";
            var message = $"Query:'{matchExpression}' LIKE '{pattern}' ESCAPE '{escape}'. Regex:{regex}";

            try
            {
                var actual = matchExpression.Like(pattern, escape);
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