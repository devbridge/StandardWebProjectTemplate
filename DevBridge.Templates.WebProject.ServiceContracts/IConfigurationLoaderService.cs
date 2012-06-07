using System.Configuration;

namespace DevBridge.Templates.WebProject.ServiceContracts
{
    public interface IConfigurationLoaderService
    {
        T LoadConfig<T>() where T : ConfigurationSection;
    }
}
