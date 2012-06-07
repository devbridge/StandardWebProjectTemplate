using System;

namespace DevBridge.Templates.WebProject.DataContracts.Exceptions
{
    public abstract class KnownException : Exception
    {
        protected KnownException(string message) : base(message)
        {
        }

        protected KnownException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
