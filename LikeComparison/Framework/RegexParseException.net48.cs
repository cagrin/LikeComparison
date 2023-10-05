#if NET48
namespace LikeComparison
{
    internal sealed class RegexParseException : System.ArgumentException
    {
        public RegexParseException()
        {
        }

        public RegexParseException(string message)
        : base(message)
        {
        }

        public RegexParseException(string message, System.Exception innerException)
        : base(message, innerException)
        {
        }

        public static RegexParseException That => new RegexParseException();
    }
}
#endif