using System;

namespace LikeComparison
{
    public class LikeOptions
    {
        public LikeOptions()
        {
        }

        public PatternStyle PatternStyle { get; init; } = PatternStyle.VisualBasic;

        public StringComparison StringComparison { get; init; } = StringComparison.OrdinalIgnoreCase;

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