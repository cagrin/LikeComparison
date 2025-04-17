namespace LikeComparison.DatabaseTests
{
    using Dapper;
    using LikeComparison.TransactSql;
    using Microsoft.Data.SqlClient;
    using Testcontainers.MsSql;

    [TestClass]
    public partial class LikeTransactSqlTests
    {
        private static MsSqlContainer? testcontainer;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // docker run -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=StrongP@ssw0rd!' -p 1433:1433 -d mcr.microsoft.com/mssql/server
            _ = context;

            testcontainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server")
                .Build();

            testcontainer.StartAsync().Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            testcontainer?.DisposeAsync().AsTask().Wait();
        }

        [DataTestMethod] // https://docs.microsoft.com/en-us/sql/t-sql/language-elements/like-transact-sql?view=sql-server-ver15
        [DataRow("5[%]", "5%", true)]
        [DataRow("5[%]", "5", false)]
        [DataRow("[_]n", "_n", true)]
        [DataRow("[_]n", "n", false)]
        [DataRow("[a-cdf]", "-", false)]
        [DataRow("[a-cdf]", "a", true)]
        [DataRow("[a-cdf]", "b", true)]
        [DataRow("[a-cdf]", "c", true)]
        [DataRow("[a-cdf]", "d", true)]
        [DataRow("[a-cdf]", "e", false)]
        [DataRow("[a-cdf]", "f", true)]
        [DataRow("[-acdf]", "-", true)]
        [DataRow("[-acdf]", "a", true)]
        [DataRow("[-acdf]", "b", false)]
        [DataRow("[-acdf]", "c", true)]
        [DataRow("[-acdf]", "d", true)]
        [DataRow("[-acdf]", "e", false)]
        [DataRow("[-acdf]", "f", true)]
        [DataRow("[[]", "[", true)]
        [DataRow("]", "]", true)]
        [DataRow("abc[_]d%", "abc_d", true)]
        [DataRow("abc[_]d%", "abc_de", true)]
        [DataRow("abc[def]", "abcd", true)]
        [DataRow("abc[def]", "abce", true)]
        [DataRow("abc[def]", "abcf", true)]
        public async Task WildcardCharactersAsLiteralsTests(string pattern, string matchExpression, bool expected)
        {
            bool actual = await LikeTransactSqlAssert(matchExpression, pattern).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
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

                bool actual = await LikeTransactSqlAssert(matchExpression, pattern).ConfigureAwait(false);
                Assert.IsNotNull(actual);
            }).Wait();
        }

        private static async Task<bool> LikeTransactSqlAssert(string matchExpression, string pattern)
        {
            var expected = await LikeTransactSqlOperatorAsync(matchExpression, pattern).ConfigureAwait(false);
            var regex = LikeTransactSql.LikeRegex(pattern) ?? "<Null>";
            var message = $"Query:'{matchExpression}' LIKE '{pattern}'. Regex:{regex}";

            try
            {
                var actual = matchExpression.Like(pattern);
                Assert.AreEqual(expected, actual, message);
                return actual;
            }
            catch (Exception ex)
            {
                Assert.Fail($"{message}. Exception:{ex.Message}.");
                throw;
            }
        }

        private static async Task<bool> LikeTransactSqlOperatorAsync(string matchExpression, string pattern)
        {
            string query = $"SELECT CASE WHEN '{matchExpression}' LIKE '{pattern}' THEN 1 ELSE 0 END";

            using var connection = new SqlConnection(testcontainer?.GetConnectionString());

            return await connection.ExecuteScalarAsync<bool>(query).ConfigureAwait(false);
        }
    }
}