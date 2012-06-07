using System.Collections.Generic;
using System.Linq;
using DevBridge.Templates.WebProject.DataEntities.Dto;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.DataEntities.Enums;

namespace DevBridge.Templates.WebProject.DataContracts
{
    public interface IAgreementRepository : IRepository<Agreement>, IUnitOfWorkRepository
    {
        IQueryable<Agreement> GetAgreementsByCustomerType(CustomerType customerType);

        IQueryable<AgreementInformation> GetAgreementInformation(CustomerType customerType);
    }
}
