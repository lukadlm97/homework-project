using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Enigmatry.Shop.Application.Exceptions
{
    public class UnavailableAtLocalPersistenceStorageException : Exception
    {
        public UnavailableAtLocalPersistenceStorageException(string message) : base(message) { }
        public UnavailableAtLocalPersistenceStorageException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
