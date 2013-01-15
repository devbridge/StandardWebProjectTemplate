using System.Security.Principal;

namespace DevBridge.Templates.WebProject.DataContracts
{
    public interface IPrincipalAccessor
    {
        /// <summary>
        /// Gets the current principal.
        /// </summary>
        /// <returns></returns>
        IPrincipal GetCurrentPrincipal();       
    }
}
