[assembly: Parallelize(Workers = 8, Scope = ExecutionScope.MethodLevel)]
namespace LikeComparison.Tests
{
    using LikeComparison.VisualBasic;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;

    [TestClass]
    public class LikeVisualBasicTests
    {
        [DataTestMethod]
        [DataRow("abcdef", null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LikeThrowArgumentNullException(string matchExpression, string pattern)
        {
            matchExpression.Like(pattern);
        }

        [DataTestMethod]
        [DataRow("", "")]
        [DataRow("", "*")]
        [DataRow("abcdef", "*")]
        [DataRow("abcdef", "**")]
        [DataRow("abcdef", "a*")]
        [DataRow("abcdef", "*f")]
        [DataRow("abcdef", "*cd*")]
        [DataRow("abcdef", "a*f")]
        [DataRow("abcdef", "A*F")]
        public void LikeAreMatched(string matchExpression, string pattern)
        {
            bool actual = matchExpression.Like(pattern);
            Assert.IsTrue(actual);
        }

        [DataTestMethod]
        [DataRow("abcdef", "")]
        [DataRow("abcdef", "abc")]
        [DataRow("abcdef", "*a")]
        [DataRow("abcdef", "**z")]
        public void LikeAreNotMatched(string matchExpression, string pattern)
        {
            bool actual = matchExpression.Like(pattern);
            Assert.IsFalse(actual);
        }

        [DataTestMethod]
        // https://support.microsoft.com/en-us/office/like-operator-b2f7ef03-9085-4ffb-9829-eef18358e931
        [DataRow("a*a", "aa", true)]
        [DataRow("a*a", "aBa", true)]
        [DataRow("a*a", "aBBBa", true)]
        [DataRow("a*a", "aBC", false)]
        [DataRow("*ab*", "abc", true)]
        [DataRow("*ab*", "AABB", true)]
        [DataRow("*ab*", "Xab", true)]
        [DataRow("*ab*", "aZb", false)]
        [DataRow("*ab*", "bac", false)]
        [DataRow("a[*]a", "a*a", true)]
        [DataRow("a[*]a", "aaa", false)]
        [DataRow("ab*", "abcdefg", true)]
        [DataRow("ab*", "abc", true)]
        [DataRow("ab*", "cab", false)]
        [DataRow("ab*", "aab", false)]
        [DataRow("a?a", "aaa", true)]
        [DataRow("a?a", "a3a", true)]
        [DataRow("a?a", "aBa", true)]
        [DataRow("a?a", "aBBBa", false)]
        [DataRow("a#a", "a0a", true)]
        [DataRow("a#a", "a1a", true)]
        [DataRow("a#a", "a2a", true)]
        [DataRow("a#a", "aaa", false)]
        [DataRow("a#a", "a10a", false)]
        [DataRow("[a-z]", "f", true)]
        [DataRow("[a-z]", "p", true)]
        [DataRow("[a-z]", "j", true)]
        [DataRow("[a-z]", "2", false)]
        [DataRow("[a-z]", "&", false)]
        [DataRow("[!a-z]", "9", true)]
        [DataRow("[!a-z]", "&", true)]
        [DataRow("[!a-z]", "%", true)]
        [DataRow("[!a-z]", "b", false)]
        [DataRow("[!a-z]", "a", false)]
        [DataRow("[!0-9]", "A", true)]
        [DataRow("[!0-9]", "a", true)]
        [DataRow("[!0-9]", "&", true)]
        [DataRow("[!0-9]", "~", true)]
        [DataRow("[!0-9]", "0", false)]
        [DataRow("[!0-9]", "1", false)]
        [DataRow("[!0-9]", "9", false)]
        [DataRow("a[!b-m]#", "An9", true)]
        [DataRow("a[!b-m]#", "az0", true)]
        [DataRow("a[!b-m]#", "a99", true)]
        [DataRow("a[!b-m]#", "abc", false)]
        [DataRow("a[!b-m]#", "aj0", false)]
        public void MicrosoftAccessTests(string pattern, string matchExpression, bool expected)
        {
            bool actual = matchExpression.Like(pattern);
            Assert.AreEqual(actual, expected);
        }

        [DataTestMethod]
        // https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator
        [DataRow("F", "F", true)]
        [DataRow("f", "F", true)] // Option Compare Text (true) vs Binary (false)
        [DataRow("FFF", "F", false)]
        [DataRow("a*a", "aBBBa", true)]
        [DataRow("[A-Z]", "F", true)]
        [DataRow("[!A-Z]", "F", false)]
        [DataRow("a#a", "a2a", true)]
        [DataRow("a[L-P]#[!c-e]", "aM5b", true)]
        [DataRow("B?T*", "BAT123khg", true)]
        [DataRow("B?T*", "CAT123khg", false)]
        public void VisualBasicTests(string pattern, string matchExpression, bool expected)
        {
            bool actual = matchExpression.Like(pattern);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void VisualBasicCoreTests()
        {
            Assert.IsTrue(LikeOperator.LikeString("-1d", "-#*", CompareMethod.Binary), "1");
            Assert.IsTrue(LikeOperator.LikeString("1", "#", CompareMethod.Binary), "2");
            Assert.IsFalse(LikeOperator.LikeString("12", "#", CompareMethod.Binary), "3");
            Assert.IsFalse(LikeOperator.LikeString("aa", "?", CompareMethod.Binary), "4");
            Assert.IsTrue(LikeOperator.LikeString("F", "F", CompareMethod.Binary), "5");
            Assert.IsTrue(LikeOperator.LikeString("F", "F", CompareMethod.Text), "6");
            Assert.IsFalse(LikeOperator.LikeString("F", "f", CompareMethod.Binary), "7");
            Assert.IsTrue(LikeOperator.LikeString("F", "f", CompareMethod.Text), "8");
            Assert.IsFalse(LikeOperator.LikeString("F", "FFF", CompareMethod.Binary), "9");
            Assert.IsTrue(LikeOperator.LikeString("aBBBa", "a*a", CompareMethod.Binary), "10");
            Assert.IsTrue(LikeOperator.LikeString("F", "[A-Z]", CompareMethod.Binary), "11");
            Assert.IsFalse(LikeOperator.LikeString("F", "[!A-Z]", CompareMethod.Binary), "12");
            Assert.IsTrue(LikeOperator.LikeString("a2a", "a#a", CompareMethod.Binary), "13");
            Assert.IsTrue(LikeOperator.LikeString("aM5b", "a[L-P]#[!c-e]", CompareMethod.Binary), "14");
            Assert.IsTrue(LikeOperator.LikeString("BAT123khg", "B?T*", CompareMethod.Binary), "15");
            Assert.IsFalse(LikeOperator.LikeString("CAT123khg", "B?T*", CompareMethod.Binary), "16");
        }
    }
}
