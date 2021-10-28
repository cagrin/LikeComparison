namespace LikeComparison
{
    using System;
    
    internal enum PatternStyle
    {
        VisualBasic = 0,

        TransactSql = 1
    }

    internal class LikeOptions
    {
        public LikeOptions()
        {
        }

        public PatternStyle PatternStyle { get; set; } = PatternStyle.VisualBasic;

        public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

        public string Wildcard { get => this.PatternStyle == PatternStyle.VisualBasic ? "*" : "%"; }

        public string Single { get => this.PatternStyle == PatternStyle.VisualBasic ? "?" : "_"; }

        public string Invert { get => this.PatternStyle == PatternStyle.VisualBasic ? "!" : "^"; }

        public string Digits { get => this.PatternStyle == PatternStyle.VisualBasic ? "#" : "[0-9]"; }
    }
}