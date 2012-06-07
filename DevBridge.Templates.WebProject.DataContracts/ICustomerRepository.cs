using DevBridge.Templates.WebProject.DataEntities.Entities;

namespace DevBridge.Templates.WebProject.DataContracts
{
    public interface ICustomerRepository : IRepository<Customer>, IUnitOfWorkRepository
    {
    }
}
