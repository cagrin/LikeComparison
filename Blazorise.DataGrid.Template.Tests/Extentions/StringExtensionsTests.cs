using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blazorise.DataGrid.Template.Extensions;
using System;

namespace Blazorise.DataGrid.Template.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [DataTestMethod]
        [DataRow("abcdef", null)]
        [ExpectedException(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'pattern')")]
        public void StringLikeThrowArgumentNullException(string matchExression, string pattern)
        {
            matchExression.Like(pattern);
        }

        [DataTestMethod]
        [DataRow("abcdef", "")]
        [DataRow("abcdef", "abc")]
        [DataRow("abcdef", "a*")]
        [DataRow("abcdef", "*f")]
        [DataRow("abcdef", "*cd*")]
        public void StringLikeAreMatched(string matchExression, string pattern)
        {
            bool actual = matchExression.Like(pattern);
            Assert.IsTrue(actual);
        }

        [DataTestMethod]
        [DataRow("abcdef", "*")]
        [DataRow("abcdef", "**")]
        [DataRow("abcdef", "a*f")]
        [DataRow("abcdef", "*a*f*")]
        public void StringLikeAreNotMatched(string matchExression, string pattern)
        {
            bool actual = matchExression.Like(pattern);
            Assert.IsFalse(actual);
        }
    }
}
