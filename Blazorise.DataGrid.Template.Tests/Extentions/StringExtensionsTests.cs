using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blazorise.DataGrid.Template.Extensions;

namespace Blazorise.DataGrid.Template.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [DataTestMethod]
        [DataRow("abcdef", "*", false)]
        [DataRow("abcdef", "abc", true)]
        [DataRow("abcdef", "a*", true)]
        [DataRow("abcdef", "*f", true)]
        [DataRow("abcdef", "*cd*", true)]
        [DataRow("abcdef", "**", false)]
        public void StringLikeTest(string matchExression, string pattern, bool expected)
        {
            bool actual = matchExression.Like(pattern);
            Assert.AreEqual(actual, expected);
        }
    }
}
