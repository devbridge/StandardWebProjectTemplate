using System;
using System.Web.Mvc;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class AutocompleteAttribute : Attribute, IMetadataAware
	{
		public Type DataSourceType;
		public int MinChars { get; set; }
		public bool SetDataSourceAfterLoadViaOptions { get; set; }

		public AutocompleteAttribute()
		{
			MinChars = 2;
		}

		public AutocompleteAttribute(Type dataSourceType)
			: this()
		{
			DataSourceType = dataSourceType;
		}

		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.AdditionalValues[typeof(AutocompleteAttribute).Name] = this;
		}
	}
}