namespace LikeComparison.StringTests
{
    using LikeComparison.VisualBasic;

    [TestClass]
    public class LikeRegexTests
    {
        [DataTestMethod]
        [DataRow(null)]
        public void LikeRegexThrowArgumentNullException(string pattern)
        {
            _ = Assert.ThrowsExactly<ArgumentNullException>(() =>
            {
                var regex = LikeVisualBasic.LikeRegex(pattern);
                Assert.IsNotNull(regex);
            });
        }

        [DataTestMethod]
        [DataRow("[[!]", "^[\\[\\!]$")]
        [DataRow("aaa]", "^aaa\\]$")]
        [DataRow("a[aa", "Null")]
        [DataRow("a[]a", "Null")]
        [DataRow("a[!]a", "^a.a$")]
        [DataRow("[a-z]", "^[a-z]$")]
        [DataRow("[!0-9]", "^[^0-9]$")]
        [DataRow("a[!b-m]?", "^a[^b-m].$")]
        [DataRow("a[-]", "^a[-]$")]
        [DataRow("a[-]b", "^a[-]b$")]
        [DataRow("a[-b]c", "^a[-b]c$")]
        [DataRow("a[b-]c", "^a[b-]c$")]
        [DataRow("a[!b-]c", "^a[^b-]c$")]
        [DataRow("a[b-!]c", "^a[b-\\!]c$")]
        [DataRow("aa-", "^aa-$")]
        [DataRow("a#a", "^a[0-9]a$")]
        [DataRow("a[!b-m]#", "^a[^b-m][0-9]$")]
        public void LikeRegexSpecials(string pattern, string excepted)
        {
            var actual = LikeVisualBasic.LikeRegex(pattern) ?? "Null";
            Assert.AreEqual(excepted, actual);
        }

        [DataTestMethod]
        [DataRow("a[-]b", "a-b", true)]
        [DataRow("a[-]b", "aab", false)]
        [DataRow("a[-b]c", "abc", true)]
        [DataRow("a[-b]c", "a-b", false)]
        [DataRow("a[b-]c", "abc", true)]
        [DataRow("a[^b-]c", "a^c", true)]
        [DataRow("a[b-^]c", "a^c", false)]
        [DataRow("[a-z]", "a", true)]
        [DataRow("[z-a]", "a", false)]
        [DataRow("aa-", "aa-", true)]
        [DataRow("aa-", "aaa-", false)]
        public void LikeVisualBasicSpecials(string pattern, string matchExpression, bool expected)
        {
            bool actual = matchExpression.Like(pattern);
            Assert.AreEqual(expected, actual);
        }
    }
}