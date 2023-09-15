namespace LikeComparison.TestCases
{
    public static class LikeTestCases
    {
        // https://docs.microsoft.com/en-us/sql/t-sql/language-elements/like-transact-sql?view=sql-server-ver15
        public static IEnumerable<object[]> WildcardCharactersAsLiterals => new []
        {
            new object[] { string.Empty, string.Empty, true },
            ["5[%]", "5%", true],
            ["5[%]", "5", false],
            ["[_]n", "_n", true],
            ["[_]n", "n", false],
            ["[a-cdf]", "-", false],
            ["[a-cdf]", "a", true],
            ["[a-cdf]", "b", true],
            ["[a-cdf]", "c", true],
            ["[a-cdf]", "d", true],
            ["[a-cdf]", "e", false],
            ["[a-cdf]", "f", true],
            ["[-acdf]", "-", true],
            ["[-acdf]", "a", true],
            ["[-acdf]", "b", false],
            ["[-acdf]", "c", true],
            ["[-acdf]", "d", true],
            ["[-acdf]", "e", false],
            ["[-acdf]", "f", true],
            ["[[]", "[", true],
            ["]", "]", true],
            ["abc[_]d%", "abc_d", true],
            ["abc[_]d%", "abc_de", true],
            ["abc[def]", "abcd", true],
            ["abc[def]", "abce", true],
            ["abc[def]", "abcf", true],
        };
    }
}