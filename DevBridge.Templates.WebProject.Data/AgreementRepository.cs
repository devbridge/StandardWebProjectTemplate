using System.Linq;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Dto;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.DataEntities.Enums;

namespace DevBridge.Templates.WebProject.Data
{
    public class AgreementRepository : RepositoryBase<Agreement>, IAgreementRepository
    {
        public AgreementRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<Agreement> GetAgreementsByCustomerType(CustomerType customerType)
        {
            return AsQueryable().Where(f => f.Customer.Type == customerType);
        }

        public IQueryable<AgreementInformation> GetAgreementInformation(CustomerType customerType)
        {
            return AsQueryable()
                .Where(f => f.Customer.Type == customerType)
                .Select(f => new AgreementInformation
                                 {
                                     Id = f.Id,
                                     Code = f.Number,
                                     CustomerCode = f.Customer.Code,
                                     CustomerName = f.Customer.Name
                                 });           
        }       
    }
}
