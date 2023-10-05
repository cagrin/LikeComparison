#if NET48
namespace LikeComparison
{
    internal sealed class ArgumentNullException : System.ArgumentException
    {
        public ArgumentNullException()
        {
        }

        public ArgumentNullException(string message)
        : base(message)
        {
        }

        public ArgumentNullException(string message, System.Exception innerException)
        : base(message, innerException)
        {
        }

        public static ArgumentNullException That => new ArgumentNullException();

        public static void ThrowIfNull(object? argument)
        {
            if (argument == null)
            {
                throw new System.ArgumentNullException(nameof(argument), "Value cannot be null.");
            }
        }
    }
}
#endif