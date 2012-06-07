using System.Collections.Generic;
using DevBridge.Templates.WebProject.DataEntities.Entities;

namespace DevBridge.Templates.WebProject.ServiceContracts
{
    public interface IAgreementManagementService
    {
        string GenerateAgreementNumber();

        Agreement ExtendAgreement(int customerId);

        IList<Agreement> GetAgreements();       
    }
}
