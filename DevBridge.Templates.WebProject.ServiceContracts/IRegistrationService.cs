using DevBridge.Templates.WebProject.DataEntities.Enums;

namespace DevBridge.Templates.WebProject.ServiceContracts
{
    public interface IRegistrationService
    {
        void RegisterCustomer(string name);
        void RegisterCustomer(string name, CustomerType customerType);
        void UnregisterCustomer(int id);        
    }
}
