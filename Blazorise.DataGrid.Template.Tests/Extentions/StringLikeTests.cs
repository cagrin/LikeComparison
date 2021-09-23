using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blazorise.DataGrid.Template.Extensions;
using System;

[assembly: Parallelize(Workers = 8, Scope = ExecutionScope.MethodLevel)]
namespace Blazorise.DataGrid.Template.Tests.Extensions
{
    [TestClass]
    public class StringLikeTests
    {
        [DataTestMethod]
        [DataRow("abcdef", null)]
        [ExpectedException(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'pattern')")]
        public void StringLikeThrowArgumentNullException(string matchExression, string pattern)
        {
            matchExression.Like(pattern);
        }

        [DataTestMethod]
        [DataRow("", "")]
        [DataRow("", "*")]
        [DataRow("abcdef", "")]
        [DataRow("abcdef", "*")]
        [DataRow("abcdef", "**")]
        [DataRow("abcdef", "abc")]
        [DataRow("abcdef", "a*")]
        [DataRow("abcdef", "*f")]
        [DataRow("abcdef", "*cd*")]
        [DataRow("abcdef", "a*f")]
        [DataRow("abcdef", "A*F")]
        public void StringLikeAreMatched(string matchExression, string pattern)
        {
            bool actual = matchExression.Like(pattern);
            Assert.IsTrue(actual);
        }

        [DataTestMethod]
        [DataRow("abcdef", "*a")]
        [DataRow("abcdef", "**z")]
        public void StringLikeAreNotMatched(string matchExression, string pattern)
        {
            bool actual = matchExression.Like(pattern);
            Assert.IsFalse(actual);
        }

        [DataTestMethod]
        //https://support.microsoft.com/en-us/office/like-operator-b2f7ef03-9085-4ffb-9829-eef18358e931
        [DataRow("a*a", "aa", true)]
        [DataRow("a*a", "aBa", true)]
        [DataRow("a*a", "aBBBa", true)]
        [DataRow("a*a", "aBC", false)]
        [DataRow("*ab*", "abc", true)]
        [DataRow("*ab*", "AABB", true)]
        [DataRow("*ab*", "Xab", true)]
        [DataRow("*ab*", "aZb", false)]
        [DataRow("*ab*", "bac", false)]
        [DataRow("ab*", "abcdefg", true)]
        [DataRow("ab*", "abc", true)]
        [DataRow("ab*", "cab", false)]
        [DataRow("ab*", "aab", false)]
        [DataRow("a?a", "aaa", true)]
        [DataRow("a?a", "a3a", true)]
        [DataRow("a?a", "aBa", true)]
        [DataRow("a?a", "aBBBa", false)]
        public void MicrosoftAccessTests(string pattern, string matchExpression, bool expected)
        {
            bool actual = matchExpression.Like(pattern);
            Assert.AreEqual(actual, expected);
        }
    }
}
