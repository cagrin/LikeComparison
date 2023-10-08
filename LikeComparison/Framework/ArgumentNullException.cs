#if NETSTANDARD2_0
namespace LikeComparison
{
    [System.Serializable]
    public sealed class ArgumentNullException : System.ArgumentException
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

        private ArgumentNullException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
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