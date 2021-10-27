using System.Collections.Generic;
using System.Linq;

namespace LikeComparison.Tests
{
    public static class LikeTestCase
    {
        private const StringComparison ignoreCase = StringComparison.OrdinalIgnoreCase;

        public static IEnumerable<string[]> Generate(string expressionChars, string patternChars)
        {
            if (expressionChars == null) throw new ArgumentNullException(nameof(expressionChars));

            var matchExpressions = Combinations(expressionChars).ToArray();
            var patterns = Combinations(expressionChars + patternChars).Where(x => patternChars.Any(y => x.Contains(y, ignoreCase))).ToArray();

            foreach (var matchExpression in matchExpressions)
            {
                foreach (var pattern in patterns)
                {
                    yield return new string[]{ matchExpression, pattern };
                }
            }
        }

        private static string[] Combinations(string letters)
        {
            string[] chars = letters.ToCharArray().Select(x => x.ToString()).ToArray();
            var combi = new List<string>();

            combi.Add(string.Empty);
            foreach (var c1 in chars)
            {
                combi.Add(c1);
                foreach (var c2 in chars)
                {
                    combi.Add(c1 + c2);
                    foreach (var c3 in chars)
                    {
                        combi.Add(c1 + c2 + c3);
                        foreach (var c4 in chars)
                        {
                            combi.Add(c1 + c2 + c3 + c4);
                        }
                    }
                }
            }

            return combi.ToArray();
        }
    }
}