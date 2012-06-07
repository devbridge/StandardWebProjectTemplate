using System;

namespace DevBridge.Templates.WebProject.Services
{
    public class RegistrationException : ServiceException
    {
        public RegistrationException(string message) : base(message)
        {
        }

        public RegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
