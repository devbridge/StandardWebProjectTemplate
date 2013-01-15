using System.Security.Principal;

using DevBridge.Templates.WebProject.DataContracts;

namespace DevBridge.Templates.WebProject.Tests.TestHelpers
{
    public class FakePrincipalAccessor : IPrincipalAccessor
    {
        public IPrincipal GetCurrentPrincipal()
        {
            return new GenericPrincipal(new GenericIdentity("UnitTest"), new[] { "admin" });
        }
    }
}
