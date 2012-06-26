using System;
using System.Web.Mvc;

namespace DevBridge.Kernel.Web.Mvc.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class AutocompleteAttribute : Attribute, IMetadataAware
	{
		public Type DataSourceType;
		public int MinLength { get; set; }

		public AutocompleteAttribute(Type dataSourceType)
		{
			this.DataSourceType = dataSourceType;
			MinLength = 2;
		}

		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.AdditionalValues[typeof(AutocompleteAttribute).Name] = this;
		}
	}
}