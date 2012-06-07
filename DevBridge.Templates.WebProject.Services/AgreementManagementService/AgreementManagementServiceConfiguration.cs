using System.Configuration;

namespace DevBridge.Templates.WebProject.Services
{
    public class AgreementManagementServiceConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("agreementCodePrefix", IsRequired = false, DefaultValue = "A")]
        public string AgreementCodePrefix
        {
            get { return (string)this["agreementCodePrefix"]; }
            set { this["agreementCodePrefix"] = value; }
        }

        public AgreementManagementServiceConfiguration()
        {
        }
    }
}
