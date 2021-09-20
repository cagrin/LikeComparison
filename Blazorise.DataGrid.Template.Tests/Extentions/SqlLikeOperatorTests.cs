using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blazorise.DataGrid.Template.Extensions;
using System;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Blazorise.DataGrid.Template.Tests.Extensions
{
    [TestClass]
    public class SqlLikeOperatorTests
    {
        [TestMethod]
        public void SqlLikeOperator()
        {
            var cases = GenerateTestCases();

            Assert.AreEqual(cases.Count(), 363363);

            Parallel.ForEach(cases, c =>
            {
                string matchExpression = c[0].ToString();
                string pattern = c[1].ToString();

                var expected = SqlLikeOperator(matchExpression, pattern);
                var actual = StringLike.SqlLikeOperator(matchExpression, pattern);

                Assert.AreEqual(actual, expected);
            });
        }

        private bool SqlLikeOperator(string matchExpression, string pattern)
        {
            const string _connectionString = "Data Source=localhost;Initial Catalog=master;User Id=sa;Password=StrongP@ssw0rd!";
            const string _query = "SELECT CASE WHEN EXISTS(SELECT 1 FROM (SELECT @matchExpression AS matchExpression) Query WHERE Query.matchExpression LIKE @pattern) THEN 1 ELSE 0 END";

            using(var connection = new SqlConnection(_connectionString))
            {
                var result = connection.ExecuteScalar<bool>(_query, new { matchExpression = matchExpression, pattern = pattern});
                return result;
            }
        }

        private static IEnumerable<object[]> GenerateTestCases()
        {
            var chars = new List<string>() { "a", "A", "B", "%" };
            var combi = new List<string>();

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
                            foreach(var c5 in chars)
                            {
                                combi.Add(c1+c2+c3+c4+c5);
                            }
                        }
                    }
                }
            }

            var patterns = combi.Where(x => x.Contains("%")).ToArray();
            var matchExpressions = combi.Where(x => !x.Contains("%")).ToArray();

            foreach (var pattern in patterns)
            {
                foreach (var matchExpression in matchExpressions)
                {
                    yield return new object[]{ matchExpression, pattern };
                }
            }
        }
    }
}