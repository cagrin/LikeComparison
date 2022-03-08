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

        public StringComparison StringComparison => this.CaseSensitivity == CaseSensitivity.CaseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

        public string Wildcard => this.PatternStyle == PatternStyle.VisualBasic ? "*" : "%";

        public string Single => this.PatternStyle == PatternStyle.VisualBasic ? "?" : "_";

        public string Invert => this.PatternStyle == PatternStyle.VisualBasic ? "!" : "^";

        public string Digits => this.PatternStyle == PatternStyle.VisualBasic ? "#" : "[0-9]";
    }
}