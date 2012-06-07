using System;
using System.Configuration;
using System.Web.Configuration;
using DevBridge.Templates.WebProject.ServiceContracts;

namespace DevBridge.Templates.WebProject.Services
{
    public class ConfigurationLoaderService : IConfigurationLoaderService
    {
        private T TraverseConfigSections<T>(ConfigurationSectionGroup group) where T : ConfigurationSection
        {
            foreach (ConfigurationSection section in group.Sections)
            {
                if (Type.GetType(section.SectionInformation.Type, false) == typeof(T))
                    return (T)section;
            }

            foreach (ConfigurationSectionGroup g in group.SectionGroups)
            {
                var section = TraverseConfigSections<T>(g);
                if (section != null)
                    return section;
            }

            return null;
        }

        private T GetConfig<T>() where T : ConfigurationSection
        {
            T section = null;
            Configuration config = null;

            try
            {
                config = WebConfigurationManager.OpenWebConfiguration("~/Web.config");
            }
            catch { }

            if (config == null)
            {
                try
                {
                    config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                }
                catch { }
            }

            if (config != null && config.RootSectionGroup != null)
            {
                try
                {
                    section = TraverseConfigSections<T>(config.RootSectionGroup);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            if (section == null)
            {
                //checking if there are any required properties
                foreach (var member in typeof(T).GetMembers())
                {
                    var cpAtt = System.Attribute.GetCustomAttribute(member, typeof(ConfigurationPropertyAttribute)) as ConfigurationPropertyAttribute;
                    if ((cpAtt != null) && (cpAtt.IsRequired))
                        throw new ConfigurationErrorsException(String.Format("Required configuration not found: {0}", typeof(T)));
                }

                return Activator.CreateInstance<T>();
            }

            return section;
        }

        public T LoadConfig<T>() where T : ConfigurationSection
        {
            return GetConfig<T>();
        }
    }
}
