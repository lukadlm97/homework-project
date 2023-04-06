

namespace Homework.Enigmatry.Shop.Application.Exceptions
{
    public class UnclearOperationsResultException : Exception
    {
        public UnclearOperationsResultException(string message) : base(message) { }
        public UnclearOperationsResultException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
