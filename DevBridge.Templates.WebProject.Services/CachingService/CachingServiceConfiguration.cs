using System;
using System.Configuration;

namespace DevBridge.Templates.WebProject.Services
{
    public class CachingServiceConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("defaultTimeout", IsRequired = false, DefaultValue = "00:10:00")]
        public TimeSpan DefaultTimeout
        {
            get { return (TimeSpan)this["defaultTimeout"]; }
            set { this["defaultTimeout"] = value; }
        }  
    }
}
