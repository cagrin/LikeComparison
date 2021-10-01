using System;

namespace LikeComparison
{
    public class LikeOptions
    {
        public LikeOptions(PatternStyle patternStyle = PatternStyle.TransactSql, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            PatternStyle = patternStyle;
            StringComparison = stringComparison;
        }

        public PatternStyle PatternStyle { get; set; }

        public StringComparison StringComparison { get; set; }

        public string Wildcard { get => PatternStyle == PatternStyle.VisualBasic ? "*": "%"; }

        public string Single { get => PatternStyle == PatternStyle.VisualBasic ? "?": "_"; }

        public string Invert { get => PatternStyle == PatternStyle.VisualBasic ? "!": "^"; }

        public string Digits { get => PatternStyle == PatternStyle.VisualBasic ? "#": "[0-9]"; }
    }

    public enum PatternStyle
    {
        VisualBasic = 0,

        TransactSql = 1
    }
}