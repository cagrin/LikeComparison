using System;

namespace LikeComparison.VisualBasic
{
    // https://support.microsoft.com/en-us/office/like-operator-b2f7ef03-9085-4ffb-9829-eef18358e931
    // https://docs.microsoft.com/en-us/office/vba/language/reference/user-interface-help/like-operator
    public static class LikeVisualBasic
    {
        private static LikeOptions _likeOptions = new LikeOptions() { PatternStyle = PatternStyle.VisualBasic };

        public static bool Like(this string matchExpression, string pattern)
        {
            return LikeString.Like(matchExpression, pattern, _likeOptions);
        }

        public static string? LikeRegex(string pattern)
        {
            return LikeString.LikeRegex(pattern, _likeOptions);
        }
    }
}