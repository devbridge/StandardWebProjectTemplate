using System;
using DevBridge.Templates.WebProject.DataContracts.Exceptions;

namespace DevBridge.Templates.WebProject.Services
{
    public class ServiceException : KnownException
    {
        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
