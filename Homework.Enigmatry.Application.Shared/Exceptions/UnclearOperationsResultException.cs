

namespace Homework.Enigmatry.Application.Shared.Exceptions
{
    public class UnclearOperationsResultException : Exception
    {
        public UnclearOperationsResultException(string message) : base(message) { }
        public UnclearOperationsResultException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
