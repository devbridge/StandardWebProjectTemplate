using System;

namespace DevBridge.Templates.WebProject.Services
{
    public class AgreementManagementException : ServiceException
    {
        public AgreementManagementException(string message) : base(message)
        {
        }

        public AgreementManagementException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
