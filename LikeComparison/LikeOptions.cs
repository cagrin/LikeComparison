namespace LikeComparison
{
    using System;

    internal enum CaseSensitivity
    {
        CaseInsensitive = 0,

        CaseSensitive = 1,
    }

    internal enum PatternStyle
    {
        VisualBasic = 0,

        TransactSql = 1,
    }

    internal class LikeOptions
    {
        public LikeOptions()
        {
        }

        public CaseSensitivity CaseSensitivity { get; set; } = CaseSensitivity.CaseInsensitive;

        public PatternStyle PatternStyle { get; set; } = PatternStyle.VisualBasic;

        public string Escape { get; set; } = string.Empty;

        public StringComparison StringComparison { get => this.CaseSensitivity == CaseSensitivity.CaseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal; }

        public string Wildcard { get => this.PatternStyle == PatternStyle.VisualBasic ? "*" : "%"; }

        public string Single { get => this.PatternStyle == PatternStyle.VisualBasic ? "?" : "_"; }

        public string Invert { get => this.PatternStyle == PatternStyle.VisualBasic ? "!" : "^"; }

        public string Digits { get => this.PatternStyle == PatternStyle.VisualBasic ? "#" : "[0-9]"; }
    }
}