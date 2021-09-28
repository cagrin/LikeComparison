using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blazorise.DataGrid.Template.Extensions;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Blazorise.DataGrid.Template.Tests.Extensions
{
    [TestClass]
    public class LikeTransactSqlTests
    {
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
            var cases = GenerateTestCases(expressionLetters, patternLetters);

            Assert.AreEqual(combinations, cases.Count());

            Parallel.ForEachAsync(cases, new ParallelOptions() { MaxDegreeOfParallelism = 100 }, async (c, t) =>
            {
                string matchExpression = c[0].ToString();
                string pattern = c[1].ToString();

                var expected = await LikeTransactSqlOperatorAsync(matchExpression, pattern).ConfigureAwait(false);
                var regex = LikeString.LikeRegex(pattern, new LikeOptions(PatternStyle.TransactSql)) ?? "<Null>";
                var message = $"Query:'{matchExpression}' LIKE '{pattern}'. Regex:{regex}";

                try
                {
                    var actual = LikeString.Like(matchExpression, pattern, new LikeOptions(PatternStyle.TransactSql));
                    Assert.AreEqual(expected, actual, message);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, message + $". Exception:{ex.Message}.");
                }

            }).Wait();
        }

        private async Task<bool> LikeTransactSqlOperatorAsync(string matchExpression, string pattern)
        {
            const string _connectionString = "Data Source=localhost;Initial Catalog=master;User Id=sa;Password=StrongP@ssw0rd!";
            string query = "SELECT CASE WHEN '" + matchExpression + "' LIKE '" + pattern + "' THEN 1 ELSE 0 END";

            using(var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteScalarAsync<bool>(query).ConfigureAwait(false);
            }
        }

        private static IEnumerable<object[]> GenerateTestCases(string expressionChars, string patternChars)
        {
            var matchExpressions = Combinations(expressionChars).ToArray();
            var patterns = Combinations(expressionChars + patternChars).Where(x => patternChars.Any(y => x.Contains(y))).ToArray();

            foreach (var matchExpression in matchExpressions)
            {
                foreach (var pattern in patterns)
                {
                    yield return new object[]{ matchExpression, pattern };
                }
            }
        }

        private static string[] Combinations(string letters)
        {
            string[] chars = letters.ToCharArray().Select(x => x.ToString()).ToArray();
            var combi = new List<string>();

            combi.Add(string.Empty);
            foreach(var c1 in chars)
            {
                combi.Add(c1);
                foreach(var c2 in chars)
                {
                    combi.Add(c1+c2);
                    foreach(var c3 in chars)
                    {
                        combi.Add(c1+c2+c3);
                        foreach(var c4 in chars)
                        {
                            combi.Add(c1+c2+c3+c4);
                        }
                    }
                }
            }

            return combi.ToArray();
        }
    }
}