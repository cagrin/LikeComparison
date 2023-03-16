#if NET48
namespace LikeComparison
{
    internal static class ArgumentNullException
    {
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